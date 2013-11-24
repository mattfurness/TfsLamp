namespace TfsLamp.Infrastructure.Configuration
{
    public interface ITfsMergeCandidatesConfig
    {
        string FromBranch { get; }
        string ToBranch { get; }
    }
}