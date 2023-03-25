namespace Restruct.Cli.Model;

// static for now -> configurable later
public static class Instances
{
    public static Consultation Feldlex_2021_3 => new Consultation()
    {
        FedlexId = "2021:3",
        FedlexUri = new Uri("https://fedlex.data.admin.ch/eli/dl/proj/6021/3/cons_1", UriKind.Absolute),
        Sender = "Das Staatssekretariat für Bildung, Forschung und Innovation",
        SenderUrl = new Uri(
            "https://www.sbfi.admin.ch/sbfi/de/home/bildung/bildungsraum-schweiz/bildungszusammenarbeit-bund-kantone/gemeinsame-grundlagen/vernehmlassung.html"),
        Title = "Totalrevision des Bundesgesetzes über Beiträge für die kantonale französischsprachige Schule in Bern",
        Date = new DateOnly(2021, 01, 20),
        FolderName = "2021__3",
        LabelReaders = new ILabelReader[]{ new SubfolderNameBasedBinaryClassLabelReader("no_statement", "no_statement", found => found)},
    };
    
    public static Consultation Feldlex_2022_19 => new Consultation()
    {
        FedlexId = "2022:19",
        FedlexUri = new Uri("https://fedlex.data.admin.ch/eli/dl/proj/2022/19/cons_1", UriKind.Absolute),
        Sender = "Bundesamt für Justiz",
        SenderUrl = new Uri(
            "https://www.bj.admin.ch/bj/de/home/staat/gesetzgebung/staatliche-e-id.html"),
        Title = "Bundesgesetz über den elektronischen Identitätsnachweis und andere elektronische Nachweise",
        Date = new DateOnly(2022, 06, 29),
        FolderName = "2022__19",
        LabelReaders = Array.Empty<ILabelReader>(),
    };
}