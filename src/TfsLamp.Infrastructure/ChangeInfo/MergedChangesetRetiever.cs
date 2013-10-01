using System.Collections.Generic;
using TfsLamp.Infrastructure.Configuration;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.Infrastructure.ChangeInfo
{
    public class MergedChangesetRetiever : IChangesetRetriever
    {
        protected readonly ITfsOperations _operations;
        protected readonly ITfsMergedChangesetConfig _config;

        public MergedChangesetRetiever(ITfsOperations operations, ITfsMergedChangesetConfig config)
        {
            _operations = operations;
            _config = config;
        }

        public virtual IEnumerable<TfsChangeset> GetChangesets()
        {
            return _operations.GetChangesetsInMergedChangeset(_config.FromBranch, _config.ToBranch, _config.ChangesetId);
        }
    }
}