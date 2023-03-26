namespace Restruct.IntegrationTests;

public static class Fixtures
{
    private static readonly Lazy<string> _ecflVnBundesgesetzD;
    private static readonly Lazy<string> _vernehmlassungBe20210331;

    static Fixtures()
    {
        _ecflVnBundesgesetzD = new Lazy<string>(
            () =>
                File.ReadAllText(Path.Combine("Fixtures", "2021__3", "ecfl_vn_bundesgesetz_d.pdf.txt"))
        );
        _vernehmlassungBe20210331 = new Lazy<string>(
            () =>
                File.ReadAllText(Path.Combine("Fixtures", "2021__3", "Vernehmlassung_BE_20210331.txt"))
        );
    }

    public static string EcflVnBundesgesetzD => _ecflVnBundesgesetzD.Value;
    public static string VernehmlassungBe20210331 => _vernehmlassungBe20210331.Value;
}
