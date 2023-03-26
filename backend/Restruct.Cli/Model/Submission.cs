namespace Restruct.Cli.Model;

public class PromptInput
{
    private readonly Lazy<string> _submission;

    public PromptInput(FileInfo submission)
    {
        _submission = new Lazy<string>(() => File.ReadAllText(submission.FullName));
    }

    public string Submission => _submission.Value;
}
