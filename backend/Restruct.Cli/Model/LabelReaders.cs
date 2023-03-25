namespace Restruct.Cli.Model;

public class SubfolderNameBasedBinaryClassLabelReader : IFileInfoLabelReader<BinaryClassificationLabel, bool>
{
    public string Name { get; }

    private readonly string _subfolder;
    readonly Func<bool, bool> _resultMappingFunc;
    

    private readonly BinaryClassificationLabel _trueLabel;
    private readonly BinaryClassificationLabel _falseLabel;
    
    public SubfolderNameBasedBinaryClassLabelReader(string name, string subfolder, Func<bool, bool> resultMappingFunc)
    {
        Name = name;
        _resultMappingFunc = resultMappingFunc;
        _subfolder = subfolder;
        
        _trueLabel = new BinaryClassificationLabel() { Name = name, Value = true };
        _falseLabel = new BinaryClassificationLabel() { Name = name, Value = false };
    }

    public BinaryClassificationLabel ReadLabel(FileInfo file)
    {
        var fileDirectoryName = file.DirectoryName!;

        var match = string.Equals(_subfolder, fileDirectoryName);
        var result = _resultMappingFunc(match);
        return result ? _trueLabel : _falseLabel;
    }

}