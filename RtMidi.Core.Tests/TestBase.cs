using Xunit.Abstractions;
using Serilog;
namespace RtMidi.Core.Tests
{
    public abstract class TestBase
    {
        protected TestBase(ITestOutputHelper output)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.TestOutput(output, Serilog.Events.LogEventLevel.Debug)
                .MinimumLevel.Debug()
                .CreateLogger();
        }
    }
}
