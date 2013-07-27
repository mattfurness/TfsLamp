using System.Collections.Generic;

namespace TfsLamp.HtmlRendering.ViewModel
{
    public class WorkItemViewModel
    {
        public WorkItemViewModel()
        {
            Children = new List<WorkItemViewModel>();
            DirectlyAssociatedChangesets = new List<ChangesetDescription>();
        }

        public WorkItemDescription WorkItemThatWasCommittedAgainst { get; set; }
        public IList<WorkItemViewModel> Children { get; private set; }
        public IList<ChangesetDescription> DirectlyAssociatedChangesets { get; private set; }
    }
}