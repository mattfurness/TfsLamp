namespace TfsLamp.Infrastructure.Configuration
{
    public interface ITfsMergeConfig
    {
        string FromBranch { get; }
        string ToBranch { get; }
    }
}