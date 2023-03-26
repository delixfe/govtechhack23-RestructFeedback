using Restruct.Cli.Input;

namespace Restruct.IntegrationTests.Input;

[TestFixture]
public class FilesTests {
    [Test]
    public void FindDataFolder_ReturnsDataFolder() {
        var folder = Files.FindDataRootFolder();

        Assert.That(folder.Name, Is.EqualTo("data"));
        Assert.That(folder.Exists, Is.True);
    }
}
