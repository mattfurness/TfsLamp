using System.Collections.Generic;
using TfsLamp.Infrastructure.ChangeInfo;

namespace TfsLamp.HtmlRendering.ViewModel
{
    public class ChangesViewModel
    {
        public ChangesViewModel()
        {
            var workItemViewModelComparer = new GenericComparer<WorkItemViewModel>((x,y) => x.WorkItemThatWasCommittedAgainst.Id.CompareTo(y.WorkItemThatWasCommittedAgainst.Id));
            var changesetDescriptionComparer = new GenericComparer<ChangesetDescription>((x, y) => x.Id.CompareTo(y.Id));

            AllWorkItemViewModels = new SortedSet<WorkItemViewModel>(workItemViewModelComparer);
            RootWorkItemViewModels = new SortedSet<WorkItemViewModel>(workItemViewModelComparer);
            OrphanChangesets = new SortedSet<ChangesetDescription>(changesetDescriptionComparer);
            AllChangesets = new SortedSet<ChangesetDescription>(changesetDescriptionComparer);
        }

        public string Title { get; set; }
        public ISet<WorkItemViewModel> AllWorkItemViewModels { get; private set; }
        public ISet<WorkItemViewModel> RootWorkItemViewModels { get; private set; }
        public ISet<ChangesetDescription> OrphanChangesets { get; private set; }
        public ISet<ChangesetDescription> AllChangesets { get; private set; }
    }
}