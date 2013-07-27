using System.Collections.Generic;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.Infrastructure.ChangeInfo
{
    public class RelevantWorkItem
    {
        private readonly RelevantWorkItem _parent;
        private readonly ISet<TfsChangeset> _associatedChangesets;
        private readonly TfsWorkItem _workItem;
        private readonly ISet<RelevantWorkItem> _orderedAssociates;

        public RelevantWorkItem(TfsWorkItem workItem, RelevantWorkItem parent)
        {
            var workItemComparer = new GenericComparer<RelevantWorkItem>((x, y) => x.WorkItem.Id.CompareTo(y.WorkItem.Id));
            var changesetComparer = new GenericComparer<TfsChangeset>((x, y) => x.Id.CompareTo(y.Id));

            _parent = parent;
            _associatedChangesets = new SortedSet<TfsChangeset>(changesetComparer);
            _workItem = workItem;
            _orderedAssociates = new SortedSet<RelevantWorkItem>(workItemComparer);
        }

        public IEnumerable<TfsChangeset> DirectlyAssociatedChangesets
        {
            get { return _associatedChangesets; }
        }

        public void AddAssociatedChangeset(TfsChangeset changeset)
        {
            _associatedChangesets.Add(changeset);
        }

        public RelevantWorkItem Parent
        {
            get { return _parent; }
        }

        public TfsWorkItem WorkItem
        {
            get { return _workItem; }
        }

        public IEnumerable<RelevantWorkItem> RelevantChildren
        {
            get { return _orderedAssociates; }
        }

        public RelevantWorkItem AddRelevantChildItem(TfsWorkItem child)
        {
            var relevantWorkItem = new RelevantWorkItem(child, this);
            _orderedAssociates.Add(relevantWorkItem);
            return relevantWorkItem;
        }

        public RelevantWorkItem FindInSelfOrChild(int workItemId)
        {
            if (workItemId == this.WorkItem.Id)
                return this;

            foreach (var workItemInMerge in RelevantChildren)
            {
                var matchingChild = workItemInMerge.FindInSelfOrChild(workItemId);
                if (matchingChild != null)
                    return matchingChild;
            }
            return null;
        }
    }
}