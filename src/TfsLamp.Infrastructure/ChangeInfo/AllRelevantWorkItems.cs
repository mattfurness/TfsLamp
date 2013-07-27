using System.Collections.Generic;
using System.Linq;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.Infrastructure.ChangeInfo
{
    public class AllRelevantWorkItems
    {
        protected readonly ISet<RelevantWorkItem> _rootWorkItems;
        protected readonly ISet<RelevantWorkItem> _allWorkItems;
        protected readonly ISet<TfsChangeset> _orphanChangesets;

        public AllRelevantWorkItems()
        {
            var workItemComparer = new GenericComparer<RelevantWorkItem>((x, y) => x.WorkItem.Id.CompareTo(y.WorkItem.Id));
            var changesetComparer = new GenericComparer<TfsChangeset>((x, y) => x.Id.CompareTo(y.Id));

            _rootWorkItems = new SortedSet<RelevantWorkItem>(workItemComparer);
            _allWorkItems = new SortedSet<RelevantWorkItem>(workItemComparer);
            _orphanChangesets = new SortedSet<TfsChangeset>(changesetComparer);
        }

        public virtual IEnumerable<RelevantWorkItem> RootWorkItems
        {
            get { return _rootWorkItems; }
        }

        public virtual IEnumerable<TfsChangeset> OrphanChangesets
        {
            get { return _orphanChangesets; }
        }

        public virtual IEnumerable<RelevantWorkItem> AllWorkItems
        {
            get { return _allWorkItems; }
        }

        public virtual RelevantWorkItem GetWorkItem(int workItemId)
        {
            return _allWorkItems.First(wi => wi.WorkItem.Id == workItemId);
        }

        public virtual void AddChangeset(TfsChangeset changeset, ITfsOperations workItemFinder)
        {
            if (!changeset.WorkItems.Any())
            {
                _orphanChangesets.Add(changeset);
                return;
            }

            foreach (var workItem in changeset.WorkItems)
            {
                AddWorkItem(workItem, changeset, workItemFinder);
            }
        }

        protected virtual void AddWorkItem(TfsWorkItem workItem, TfsChangeset changeset, ITfsOperations workItemFinder)
        {
            RelevantWorkItem existingRelevantWorkItem;
            foreach (var rootWorkItemInMerge in _rootWorkItems)
            {
                existingRelevantWorkItem = rootWorkItemInMerge.FindInSelfOrChild(workItem.Id);
                if (existingRelevantWorkItem != null)
                {
                    existingRelevantWorkItem.AddAssociatedChangeset(changeset);
                    return;
                }
            }

            var newWorkItem = AddNewWorkItem(workItem, workItemFinder);
            newWorkItem.AddAssociatedChangeset(changeset);
        }

        protected virtual RelevantWorkItem AddNewWorkItem(TfsWorkItem workItem, ITfsOperations workItemFinder)
        {
            var parentLink = workItem.Links.FirstOrDefault(wil => wil.Name == "Parent");
            if (parentLink == null)
            {
                var newRootWorkItem = new RelevantWorkItem(workItem, null);
                _rootWorkItems.Add(newRootWorkItem);
                _allWorkItems.Add(newRootWorkItem);
                return newRootWorkItem;
            }

            RelevantWorkItem parentThatHasAlreadyBeenAdded = null;
            foreach (var rootWorkItemInMerge in _rootWorkItems)
            {
                parentThatHasAlreadyBeenAdded = rootWorkItemInMerge.FindInSelfOrChild(parentLink.LinkedToWorkItemId);
            }

            if (parentThatHasAlreadyBeenAdded == null)
            {
                var parentWorkItem = workItemFinder.GetWorkItem(parentLink.LinkedToWorkItemId);
                parentThatHasAlreadyBeenAdded = AddNewWorkItem(parentWorkItem, workItemFinder);
            }

            var childOfNewPArent = parentThatHasAlreadyBeenAdded.AddRelevantChildItem(workItem);
            _allWorkItems.Add(childOfNewPArent);
            return childOfNewPArent;

        }
    }
}