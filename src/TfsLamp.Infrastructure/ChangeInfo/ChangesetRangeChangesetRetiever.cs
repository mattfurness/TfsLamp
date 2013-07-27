using System.Collections.Generic;
using TfsLamp.Infrastructure.Configuration;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.Infrastructure.ChangeInfo
{
    public class ChangesetRangeChangesetRetiever : IChangesetRetriever
    {
        protected readonly ITfsOperations _operations;
        protected readonly ITfsChangesetRangeConfig _config;

        public ChangesetRangeChangesetRetiever(ITfsOperations operations, ITfsChangesetRangeConfig config)
        {
            _operations = operations;
            _config = config;
        }

        public virtual IEnumerable<TfsChangeset> GetChangesets()
        {
            return _operations.GetChangesets(_config.Branch, _config.FirstChangesetId, _config.LastChangesetId);
        }
    }
}