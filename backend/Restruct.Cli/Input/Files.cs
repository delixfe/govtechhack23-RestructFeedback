namespace Restruct.Cli.Input;


public enum Consultations
{
    None,
    EID,
    VernehmlassungBe20210331,
}


public enum SubmissionContent
{
    Statements,
    NoStatement,
}

public static class Files
{

    public static string GetTitle(Consultations consultation) => consultation switch
    {
        Consultations.VernehmlassungBe20210331 =>
            "Totalrevision des Bundesgesetzes über Beiträge für die kantonale französischsprachige Schule in Bern",
        Consultations.EID =>
            "Bundesgesetz über den elektronischen Identitätsnachweis und andere elektronische Nachweise (E-ID-Gesetz, BGEID)",
        _ => throw new ArgumentOutOfRangeException(nameof(consultation), consultation, null)
    };
    
    public static DirectoryInfo FindDataFolder()
    {
        // find the folder named 'data' in the file hierarchy above me
        // find the folder in the file hierarchy above me
        var folder = new DirectoryInfo("./");
        while (true)
        {
            folder = folder.Parent;
            if (folder is null)
                break;
            if (folder.Name == "data")
                break;

            var brother = new DirectoryInfo(Path.Combine(folder.FullName, "data"));
            if (brother.Exists)
            {
                folder = brother;
                break;
            }
        }

        if (folder is null)
            throw new IOException("Could not find the folder named 'data' in the file hierarchy above me");
        
        return folder;
    }
    
    public static string  GetConsultationPathSegment (Consultations consultation, SubmissionContent submissionContent)
    {
        
        var consultationSegment = consultation switch
        {
            Consultations.EID => "2022__19",
            Consultations.VernehmlassungBe20210331 => "2021__3",
            _ => throw new ArgumentOutOfRangeException(nameof(consultation), consultation, null)
        };
        
        var submissionContentSegment = submissionContent switch
        {
            
            SubmissionContent.NoStatement =>  "no_statement",
            _ => string.Empty
        };
        
        return Path.Combine(consultationSegment,"submissions", submissionContentSegment);
    }
    
    public static FileInfo[] GetSubmissionTextFilesFor(DirectoryInfo folder)
    {
        return folder.EnumerateFiles("*.txt").ToArray();
    }

    public static FileInfo[] GetSubmissionTextFiles(Consultations consultation, SubmissionContent submissionContent)
    {
        var folder = FindDataFolder();
        var consultationFolder =  Path.Combine(folder.FullName, GetConsultationPathSegment(consultation, submissionContent));
        var folderInfo = new DirectoryInfo(consultationFolder);
        if(!folderInfo.Exists) return Array.Empty<FileInfo>();
        return GetSubmissionTextFilesFor(folderInfo);
    }

    public static DirectoryInfo GetOutputFolder(Consultations consultation, SubmissionContent submissionContent)
    {
        var folder = FindDataFolder().Parent!;
        var dateTime = DateTimeOffset.Now.ToString("yyMMdd-hhmm");
        var consultationSegment =  GetConsultationPathSegment(consultation, submissionContent);
        var path = Path.Combine(folder.FullName, "output", dateTime, consultationSegment);
        Directory.CreateDirectory(path);
        
        return  new DirectoryInfo(path);
    }
}