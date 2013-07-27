using TfsLamp.HtmlRendering.ViewModel;
using TfsLamp.Infrastructure.ChangeInfo;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.HtmlRendering.Mapping
{
    public class WorkItemAssociationsToViewModelMapper : IWorkItemAssociationsToViewModelMapper
    {
        private readonly IWorkItemDescriptionGenerator _workItemDescriptionGenerator;
        private readonly IChangesetDescriptionGenerator _changesetDescriptionGenerator;

        public WorkItemAssociationsToViewModelMapper(IWorkItemDescriptionGenerator workItemDescriptionGenerator, IChangesetDescriptionGenerator changesetDescriptionGenerator)
        {
            _workItemDescriptionGenerator = workItemDescriptionGenerator;
            _changesetDescriptionGenerator = changesetDescriptionGenerator;
        }

        public virtual ChangesViewModel Map(AllRelevantWorkItems allRelevantAssociatedWorkItems)
        {
            var changesViewModel = new ChangesViewModel();

            foreach (var changeset in allRelevantAssociatedWorkItems.OrphanChangesets)
            {
                var changesetDescription = MapChangeset(changeset);
                changesViewModel.OrphanChangesets.Add(changesetDescription);
                changesViewModel.AllChangesets.Add(changesetDescription);
            }

            foreach (var workItemInMerge in allRelevantAssociatedWorkItems.RootWorkItems)
            {
                var viewModel = MapWorkItemAndAssociates(changesViewModel, workItemInMerge);
                changesViewModel.RootWorkItemViewModels.Add(viewModel);
                changesViewModel.AllWorkItemViewModels.Add(viewModel);
            }

            return changesViewModel;
        }

        protected virtual WorkItemViewModel MapWorkItemAndAssociates(ChangesViewModel root, RelevantWorkItem item)
        {
            var viewModel = new WorkItemViewModel();
            viewModel.WorkItemThatWasCommittedAgainst = MapWorkItem(item.WorkItem);
            foreach (var child in item.RelevantChildren)
            {
                var pbiViewModel = MapWorkItemAndAssociates(root, child);
                viewModel.Children.Add(pbiViewModel);
                root.AllWorkItemViewModels.Add(pbiViewModel);
            }
            foreach (var changeset in item.DirectlyAssociatedChangesets)
            {
                var changesetDescription = MapChangeset(changeset);
                viewModel.DirectlyAssociatedChangesets.Add(changesetDescription);
                root.AllChangesets.Add(changesetDescription);
            }
            return viewModel;
        }

        protected virtual ChangesetDescription MapChangeset(TfsChangeset changeset)
        {
            return _changesetDescriptionGenerator.GenerateDescription(changeset);
        }

        protected virtual WorkItemDescription MapWorkItem(TfsWorkItem workItem)
        {
            return _workItemDescriptionGenerator.GenerateDescription(workItem);
        }
    }
}