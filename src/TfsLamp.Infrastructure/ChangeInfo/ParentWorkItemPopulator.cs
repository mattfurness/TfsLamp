using System.Collections.Generic;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.Infrastructure.ChangeInfo
{
    public class ParentWorkItemPopulator : IWorkItemPopulator
    {
        private readonly ITfsOperations _operations;

        public ParentWorkItemPopulator(ITfsOperations operations)
        {
            _operations = operations;
        }

        public virtual AllRelevantWorkItems GetAssociatedWorkItemsForChangesets(IEnumerable<TfsChangeset> changesets)
        {
            var workItems = new AllRelevantWorkItems();
            foreach (var changeset in changesets)
            {
                workItems.AddChangeset(changeset, _operations);
            }
            return workItems;
        }
    }
}