namespace NPlusOneRoundTrips.Core.Models;

public sealed class Record
{
    public int Id { get; init; }
    public string ExternalKey { get; init; } = string.Empty;
}