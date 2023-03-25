using Restruct.Cli.Input;
using Restruct.Cli.Model;

namespace Restruct.IntegrationTests.Input;


    [TestFixture]
    public class FilesTests
    {
        [Test]
        public void FindDataFolder_ReturnsDataFolder()
        {
           var folder = Files.FindDataRootFolder();
           
           Assert.That(folder.Name, Is.EqualTo("data"));
           Assert.That(folder.Exists, Is.True);
        }
        
        // [Test]
        // [Pairwise]
        // public void GetSubmissionTextFiles_List(
        //     [Values(Instances.Feldlex_2021_3, Consultations.VernehmlassungBe20210331)] Consultations consultation,
        //     [Values(SubmissionContent.Statements, SubmissionContent.NoStatement)] SubmissionContent submissionContent)
        // {
        //     var textFiles = Files.GetSubmissionTextFiles(consultation, submissionContent);
        //     if(textFiles.Length == 0)
        //         return;
        //     var first = textFiles.First(); 
        //     Assert.That(first.Exists, Is.True );
        // }
    }
