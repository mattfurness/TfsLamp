using System.Collections.Generic;
using TfsLamp.Infrastructure.Configuration;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.Infrastructure.ChangeInfo
{
    public class MergeChangesetRetriever : IChangesetRetriever
    {
        private readonly ITfsOperations _operations;
        private readonly ITfsMergeConfig _mergeConfig;

        public MergeChangesetRetriever(ITfsOperations operations, ITfsMergeConfig mergeConfig)
        {
            _operations = operations;
            _mergeConfig = mergeConfig;
        }

        public IEnumerable<TfsChangeset> GetChangesets()
        {
            return _operations.GetMergeCandidates(_mergeConfig.FromBranch, _mergeConfig.ToBranch);
        }
    }
}