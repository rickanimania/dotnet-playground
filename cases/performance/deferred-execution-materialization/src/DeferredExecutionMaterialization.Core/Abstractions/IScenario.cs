using DeferredExecutionMaterialization.Core.Models;

namespace DeferredExecutionMaterialization.Core.Abstractions;

public interface IScenario
{
    string Name { get; }

    Task<ScenarioResult> ExecuteAsync(CaseRunOptions options, CancellationToken ct);
}