using System.Diagnostics;

namespace Restruct.IntegrationTests;

[SetUpFixture]
public class Init
{
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        Trace.Listeners.Add(new ConsoleTraceListener());
        DotNetEnv.Env.TraversePath().Load();
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
        // ...
    }
    
}

