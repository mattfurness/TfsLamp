using System.Collections.Generic;
using TfsLamp.Infrastructure.Configuration;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.Infrastructure.ChangeInfo
{
    public class MergeChangesetRetriever : IChangesetRetriever
    {
        private readonly ITfsOperations _operations;
        private readonly ITfsMergeCandidatesConfig _mergeCandidatesConfig;

        public MergeChangesetRetriever(ITfsOperations operations, ITfsMergeCandidatesConfig mergeCandidatesConfig)
        {
            _operations = operations;
            _mergeCandidatesConfig = mergeCandidatesConfig;
        }

        public IEnumerable<TfsChangeset> GetChangesets()
        {
            return _operations.GetMergeCandidates(_mergeCandidatesConfig.FromBranch, _mergeCandidatesConfig.ToBranch);
        }
    }
}