using Restruct.Cli.Input;

namespace Restruct.IntegrationTests.Input; 

[TestFixture]
public class LabelReaderTests {

    [Test]
    [TestCase("fixtures/2021__3/no_statement_false.txt", false)]
    [TestCase("fixtures/2021__3/no_statement/no_statement_true.txt", true)]
    public void SubfolderNameBasedBinaryClassLabelReader(string relativePath, bool expectedValue) {
        var fileInfo = new FileInfo(relativePath);
        if (!fileInfo.Exists) throw new IOException($"Test file {fileInfo.FullName} does not exist");

        var reader = new SubfolderNameBasedBinaryClassLabelReader("no_statement", "no_statement", found => found);
        var label = reader.ReadLabel(fileInfo);

        var actual = label.Value;
        
        Assert.That(actual, Is.EqualTo(expectedValue));
    }
}
