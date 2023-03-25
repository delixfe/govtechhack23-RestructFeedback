// See https://aka.ms/new-console-template for more information


using System.Text.Encodings.Web;
using System.Text.Json;
using Restruct.Cli.Input;
using Restruct.Cli.Model;
using Restruct.Cli.OpenAI;

DotNetEnv.Env.TraversePath().Load();

var consultation = Instances.Feldlex_2021_3;

var jsonWriterOptions = new JsonWriterOptions
{
    Indented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
};
var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
var metrics = new Metrics();


try
{
    // TODO factor the following declarations into an config
    const int maxSubmissions = 1;
    const string inputPathPattern = "*.txt";
    var dataRootFolder = Files.FindDataRootFolder();
    Directory.SetCurrentDirectory(dataRootFolder.FullName);
    var submissionFolder = Files.GetConsultationSubmissionFolder(dataRootFolder, consultation);
    var outputRootFolder = Files.CreateOutputRootFolder(dataRootFolder);
    var outputFolder = Files.CreateSessionOutputFolder(outputRootFolder, consultation);
    var targetJsonOutputPath = Path.Combine(outputFolder.FullName, "summary.json");

    Console.WriteLine("Output directory:");
    Console.WriteLine(outputFolder);
    Console.WriteLine();

    var submissions = Files.EnumerateFilesWithSubdirectories(submissionFolder, inputPathPattern)
        .Take(maxSubmissions);


    await using var targetStream = File.Create(targetJsonOutputPath);
    await using var writer = new Utf8JsonWriter(targetStream, jsonWriterOptions);

    writer.WriteStartObject();
    if (consultation.FedlexId is not null)
        writer.WriteString("fedlex_id"u8, consultation.FedlexId);
    if (consultation.FedlexUri is not null)
        writer.WriteString("fedlex_uri"u8, $"{consultation.FedlexUri}");
    if (consultation.Title is not null)
        writer.WriteString("title"u8, consultation.Title);
    if (consultation.Date.HasValue)
        writer.WriteString("date"u8, $"{consultation.Date!:O}");
    if (consultation.Sender is not null)
        writer.WriteString("sender"u8, consultation.Sender);
    if (consultation.SenderUrl is not null)
        writer.WriteString("sender_url"u8, $"{consultation.SenderUrl}");

    writer.WriteStartArray("answers"u8);
    foreach (var file in submissions)
    {
        var metricsForFile = new Metrics();
        var promptInput = new PromptInput(file);
        var labels = consultation.LabelReaders.Select(reader => reader switch
        {
             SubfolderNameBasedBinaryClassLabelReader r => r.ReadLabel(file),
            _ => throw new InvalidOperationException(),
        }).ToArray();

        writer.WriteStartObject();
        writer.WriteString("_fileName"u8, file.Name);

        if (labels.Length > 0)
        {
            writer.WriteStartArray("labels"u8);
            foreach (var label in labels)
            {
                writer.WriteStartObject();
                label.WriteToJsonWriter(writer);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

        foreach (var prompt in Chat.Prompts)
        {
            try
            {
                var response = await Chat.Prompt(promptInput, prompt);
                writer.WriteString(prompt.Attribute, response.FirstChoice);
                metricsForFile.Add(response.Usage);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error requesting prompt {prompt.Attribute} for file {file.Name}");
                Console.Error.WriteLine(e);
                continue;
            }
        }
        metricsForFile.WriteToJsonTextWriter(writer);
        writer.WriteEndObject();
        metrics.Add(metricsForFile);
    }

   
    writer.WriteEndArray();
    metrics.WriteToJsonTextWriter(writer);

    writer.WriteEndObject();
}

catch (Exception e)
{
    Console.Error.WriteLine("That did not went well");
    Console.Error.WriteLine(e);
}
finally
{
    JsonSerializer.Serialize(metrics, jsonSerializerOptions);
}