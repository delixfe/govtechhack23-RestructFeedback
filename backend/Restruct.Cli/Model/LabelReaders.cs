namespace Restruct.Cli.Model;

public class SubfolderNameBasedBinaryClassLabelReader : IFileInfoLabelReader<BinaryClassificationLabel, bool>
{
    private readonly BinaryClassificationLabel _falseLabel;
    private readonly Func<bool, bool> _resultMappingFunc;

    private readonly string _subfolder;

    private readonly BinaryClassificationLabel _trueLabel;

    public SubfolderNameBasedBinaryClassLabelReader(string name, string subfolder, Func<bool, bool> resultMappingFunc)
    {
        Name = name;
        _resultMappingFunc = resultMappingFunc;
        _subfolder = subfolder;

        _trueLabel = new BinaryClassificationLabel { Name = name, Value = true };
        _falseLabel = new BinaryClassificationLabel { Name = name, Value = false };
    }

    public string Name { get; }

    public BinaryClassificationLabel ReadLabel(FileInfo file)
    {
        var fileDirectoryName = file.DirectoryName!;

        var match = string.Equals(_subfolder, fileDirectoryName);
        var result = _resultMappingFunc(match);
        return result ? _trueLabel : _falseLabel;
    }
}
