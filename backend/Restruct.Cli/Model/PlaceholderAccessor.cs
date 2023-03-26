namespace Restruct.Cli.Model;

public class PlaceholderAccessor {
    readonly Lazy<string> _submission;

    public PlaceholderAccessor(FileInfo submission) {
        _submission = new Lazy<string>(() => File.ReadAllText(submission.FullName));
    }

    public string Submission => _submission.Value;
}
