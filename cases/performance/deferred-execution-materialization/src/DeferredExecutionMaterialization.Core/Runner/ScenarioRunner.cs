using System.Diagnostics;
using DeferredExecutionMaterialization.Core.Abstractions;
using DeferredExecutionMaterialization.Core.Models;

namespace DeferredExecutionMaterialization.Core.Runner;

public sealed class ScenarioRunner
{
    private readonly IReadOnlyList<IScenario> _scenarios;

    public ScenarioRunner(IEnumerable<IScenario> scenarios)
    {
        _scenarios = scenarios?.ToList() ?? throw new ArgumentNullException(nameof(scenarios));
    }

    public async Task<IReadOnlyList<ScenarioResult>> RunAsync(RunMode mode, CancellationToken ct = default)
    {
        var options = CaseRunOptions.From(mode);

        var results = new List<ScenarioResult>(_scenarios.Count);

        foreach (var scenario in _scenarios)
        {
            ct.ThrowIfCancellationRequested();

            var sw = Stopwatch.StartNew();
            var result = await scenario.ExecuteAsync(options, ct);
            sw.Stop();

            // garante consistência (caso o cenário não preencha)
            var normalized = new ScenarioResult
            {
                ScenarioName = result.ScenarioName,
                Mode = result.Mode,
                Items = options.Items,
                DelayMs = options.DelayMs,
                ElapsedMilliseconds = result.ElapsedMilliseconds > 0
                    ? result.ElapsedMilliseconds
                    : sw.ElapsedMilliseconds
            };

            results.Add(normalized);
        }

        return results;
    }
}