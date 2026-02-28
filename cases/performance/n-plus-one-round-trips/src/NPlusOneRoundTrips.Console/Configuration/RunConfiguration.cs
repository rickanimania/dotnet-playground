namespace NPlusOneRoundTrips.Console.Configuration;

public enum RunMode
{
    Demo,
    Stress
}

public static class RunConfiguration
{
    public static (int TotalRecords, int InMemoryDelayMs) GetConfig(RunMode mode)
    {
        return mode switch
        {
            RunMode.Demo => (TotalRecords: 500, InMemoryDelayMs: 2),
            RunMode.Stress => (TotalRecords: 10_000, InMemoryDelayMs: 0),
            _ => (TotalRecords: 500, InMemoryDelayMs: 2)
        };
    }
}