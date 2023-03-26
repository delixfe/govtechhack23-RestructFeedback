using Restruct.Cli.Model;

namespace Restruct.Cli.Input;

public static class Files {
    public static DirectoryInfo FindDataRootFolder() {
        // find the folder named 'data' in the file hierarchy above me
        // find the folder in the file hierarchy above me
        var folder = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (true) {
            folder = folder.Parent;
            if (folder is null) { break; }

            if (folder.Name == "data") { break; }

            var brother = new DirectoryInfo(Path.Join(folder.FullName, "data"));
            if (brother.Exists) {
                folder = brother;
                break;
            }
        }

        if (folder is null) {
            throw new IOException("Could not find the folder named 'data' in the file hierarchy above me");
        }

        return folder;
    }

    public static DirectoryInfo GetConsultationSubmissionFolder(
        DirectoryInfo dataRootFolder,
        Consultation consultation) {
        var path = Path.Combine(dataRootFolder.FullName, consultation.FolderName, "submissions");
        if (!Directory.Exists(path)) { throw new IOException($"Submissions folder expected at {path} does not exist"); }

        return new DirectoryInfo(Path.GetRelativePath(dataRootFolder.FullName, path));
    }

    public static IEnumerable<FileInfo> EnumerateFilesWithSubdirectories(DirectoryInfo submissionRoot, string pattern) {
        var enumerationOptions = new EnumerationOptions { RecurseSubdirectories = true, };
        return submissionRoot.GetFiles(pattern, enumerationOptions);
    }

    public static DirectoryInfo CreateOutputRootFolder(DirectoryInfo dataFolder) {
        var dirInfo = new DirectoryInfo(Path.Combine(dataFolder.FullName, "output"));
        if (!dirInfo.Exists) { dirInfo.Create(); }

        return new DirectoryInfo(Path.GetRelativePath(dataFolder.FullName, dirInfo.FullName));
    }

    public static DirectoryInfo CreateSessionOutputFolder(DirectoryInfo outputRoot, Consultation consultation) {
        var dateTime = DateTimeOffset.Now.ToString("yyyyMMdd-HHmmss");
        var path = Path.Combine(outputRoot.FullName, consultation.FolderName, dateTime);

        Directory.CreateDirectory(path);

        return new DirectoryInfo(path);
    }
}
