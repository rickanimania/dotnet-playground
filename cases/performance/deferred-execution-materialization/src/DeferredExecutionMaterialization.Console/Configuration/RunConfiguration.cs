using DeferredExecutionMaterialization.Core.Models;
namespace DeferredExecutionMaterialization.Console.Configuration;

public static class RunConfiguration
{
    public static (int totalRecords, int inMemoryDelayMs) GetConfig(RunMode mode)
    {
        return mode switch
        {
            RunMode.Demo => (totalRecords: 500, inMemoryDelayMs: 2),
            RunMode.Stress => (totalRecords: 10_000, inMemoryDelayMs: 0),
            _ => (totalRecords: 500, inMemoryDelayMs: 2)
        };
    }
}