namespace Restruct.Cli.Model;

public interface IFileInfoLabelReader<out TLabel, out TValue> : ILabelReader where TLabel : ILabel<TValue> {
    TLabel ReadLabel(FileInfo file);
}

public readonly record struct Consultation {
    #region Metadata

    public string? FedlexId { get; init; }
    public Uri? FedlexUri { get; init; }
    public string? Title { get; init; }
    public DateOnly? Date { get; init; }
    public string? Sender { get; init; }
    public Uri? SenderUrl { get; init; }

    #endregion

    #region Execution

    public string FolderName { get; init; }

    public ILabelReader[] LabelReaders { get; init; }

    #endregion
}
