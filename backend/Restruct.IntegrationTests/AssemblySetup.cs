namespace Restruct.IntegrationTests;

[SetUpFixture]
public class AssemblySetup
{
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        DotNetEnv.Env.TraversePath().Load();
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
        // ...
    }
    
}

