using System.Diagnostics;

using DotNetEnv;

namespace Restruct.IntegrationTests;

[SetUpFixture]
public class Init
{
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        Trace.Listeners.Add(new ConsoleTraceListener());
        Env.TraversePath().Load();
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
        // ...
    }
}
