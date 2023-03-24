// See https://aka.ms/new-console-template for more information


using System.Text.Encodings.Web;
using System.Text.Json;
using Restruct.Cli.Extensions;
using Restruct.Cli.Input;
using Restruct.Cli.OpenAI;

var consultation = Consultations.VernehmlassungBe20210331;
var submissionContent = SubmissionContent.Statements;

var jsonWriterOptions = new JsonWriterOptions
{
    Indented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
};
var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
var metrics = new Metrics();

var outputFolder = Files.GetOutputFolder(consultation, submissionContent);
var targetJsonOutputPath = Path.Combine(outputFolder.FullName, "_output.json");
var textFiles = Files.GetSubmissionTextFiles(consultation, submissionContent);

try
{
    Console.WriteLine("Output directory:");
    Console.WriteLine(outputFolder);
    Console.WriteLine();

    await using var targetStream = File.Create(targetJsonOutputPath);
    await using var writer = new Utf8JsonWriter(targetStream, jsonWriterOptions);

    // Console.WriteLine($"Start processing {textFiles.Length} to extract {Chat.Prompts.Length} attributes");
    foreach (var file in textFiles)
    {
        foreach (var prompt in Chat.Prompts)
        {
            var response = await Chat.Prompt(file, prompt);

            metrics.Add(response.Usage);

            Console.WriteLine(response.ToJson());
        }
    }
}

catch (Exception e)
{
    Console.WriteLine("That did not went well");
    Console.WriteLine(e);
}
finally
{
    JsonSerializer.Serialize(metrics, jsonSerializerOptions);
}