namespace Restruct.Cli.Model;

public class PromptInput
{
    public PromptInput(FileInfo submission)
    {
        _submission = new Lazy<string>(() => File.ReadAllText(submission.FullName));
    }
    
    private readonly Lazy<string> _submission;

    public String Submission => _submission.Value;
}