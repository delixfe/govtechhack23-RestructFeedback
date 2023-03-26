using System.Text.Json;

namespace Restruct.Cli.Model;

public interface ILabelReader
{
    public string Name { get; }
}

public interface ILabel
{
    string Name { get; init; }

    void WriteToJsonWriter(Utf8JsonWriter writer);
}

public interface ILabel<T> : ILabel
{
    T Value { get; init; }
}

public readonly record struct BinaryClassificationLabel : ILabel<bool>
{
    public string Name { get; init; }
    public bool Value { get; init; }

    public void WriteToJsonWriter(Utf8JsonWriter writer) { writer.WriteBoolean(Name, Value); }
}
