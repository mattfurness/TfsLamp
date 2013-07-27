using System.Linq;
using Autofac;
using Machine.Specifications;
using TfsLamp.HtmlRendering.Mapping;
using TfsLamp.HtmlRendering.Registration;
using TfsLamp.HtmlRendering.ViewModel;
using TfsLamp.Infrastructure.ChangeInfo;
using TfsLamp.Infrastructure.Connection;
using TfsLamp.Infrastructure.Tests.Connection;

namespace TfsLamp.HtmlRendering.Tests.Rendering
{
    public class when_all_work_items_are_mapped_to_view_models
    {
        private static IWorkItemPopulator populator;
        private static IChangesetRetriever retriever;
        private static IWorkItemAssociationsToViewModelMapper mapper;

        private static ChangesViewModel view_model;
        private static FakeTfsOperations fake_tfs_operations;

        private static WorkItemViewModel GetWorkItemViewModel(ChangesViewModel viewModel, int id)
        {
            return viewModel.AllWorkItemViewModels.First(wi => wi.WorkItemThatWasCommittedAgainst.Id == id);
        }

        private Establish context = () =>
            {
                var builder = new ContainerBuilder();
                builder.RegisterModule<Infrastructure.Tests.Registrar.Registrar>();
                builder.RegisterModule<HtmlRenderingRegistrar>();
                var container = builder.Build();
                populator = container.Resolve<IWorkItemPopulator>();
                retriever = container.Resolve<IChangesetRetriever>();
                mapper = container.Resolve<IWorkItemAssociationsToViewModelMapper>();
                fake_tfs_operations = container.Resolve<ITfsOperations>() as FakeTfsOperations;
            };

        private Because of = () => view_model = mapper.Map(populator.GetAssociatedWorkItemsForChangesets(retriever.GetChangesets()));

        private It should_have_an_orphaned_changeset = () =>
            {
                view_model.OrphanChangesets.Count().ShouldEqual(1);
                view_model.OrphanChangesets.ElementAt(0).Id.ShouldEqual(FakeTfsOperations.ChangsetOrphan);
            };

        private It should_have_2_root_items = () =>
            {
                view_model.RootWorkItemViewModels.Count().ShouldEqual(2);
                view_model.RootWorkItemViewModels.ElementAt(0).WorkItemThatWasCommittedAgainst.Id.ShouldEqual(FakeTfsOperations.RootItem1);
                view_model.RootWorkItemViewModels.ElementAt(1).WorkItemThatWasCommittedAgainst.Id.ShouldEqual(FakeTfsOperations.RootItem2);
            };

        private It should_have_2_tier_2_items = () =>
            {
                var relevantWorkItem = GetWorkItemViewModel(view_model, FakeTfsOperations.RootItem1);
                relevantWorkItem.Children.Count().ShouldEqual(2);
                relevantWorkItem.Children.ElementAt(0).WorkItemThatWasCommittedAgainst.Id.ShouldEqual(FakeTfsOperations.Item11);
                relevantWorkItem.Children.ElementAt(1).WorkItemThatWasCommittedAgainst.Id.ShouldEqual(FakeTfsOperations.Item12);
            };

        private It should_have_2_tier_3_items = () =>
            {
                var relevantWorkItem = GetWorkItemViewModel(view_model, FakeTfsOperations.Item11);
                relevantWorkItem.Children.Count().ShouldEqual(2);
                relevantWorkItem.Children.ElementAt(0).WorkItemThatWasCommittedAgainst.Id.ShouldEqual(FakeTfsOperations.Item111);
                relevantWorkItem.Children.ElementAt(1).WorkItemThatWasCommittedAgainst.Id.ShouldEqual(FakeTfsOperations.Item112);
            };

        private It should_have_2_changesets_for_item21 = () =>
            {
                var relevantWorkItem = GetWorkItemViewModel(view_model, FakeTfsOperations.Item21);
                relevantWorkItem.DirectlyAssociatedChangesets.Count().ShouldEqual(2);
                relevantWorkItem.DirectlyAssociatedChangesets.ElementAt(0).Id.ShouldEqual(FakeTfsOperations.ChangsetItem21);
                relevantWorkItem.DirectlyAssociatedChangesets.ElementAt(1).Id.ShouldEqual(FakeTfsOperations.Changset2Item21);
            };

        private It should_have_1_changesets_for_all_items_except_item21 = () =>
            {
                var relevantWorkItem = view_model.AllWorkItemViewModels.Where(i => i.WorkItemThatWasCommittedAgainst.Id != FakeTfsOperations.Item21);
                foreach (var workItem in relevantWorkItem)
                {
                    var changesets = workItem.DirectlyAssociatedChangesets;
                    changesets.Count().ShouldEqual(1);
                    changesets.ElementAt(0).Id.ShouldEqual(workItem.WorkItemThatWasCommittedAgainst.Id);

                }
            };

        private It should_have_all_changesets = () =>
            {
                view_model.AllChangesets.Count().ShouldEqual(fake_tfs_operations.GetChangesetCount());
            };
    }
}
