namespace TfsLamp.Infrastructure.Configuration
{
    public interface ITfsChangesetRangeConfig
    {
        int FirstChangesetId { get; }
        int LastChangesetId { get; }
        string Branch { get; }
    }
}