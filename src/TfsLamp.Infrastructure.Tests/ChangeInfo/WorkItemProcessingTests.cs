using System.Linq;
using Autofac;
using Machine.Specifications;
using TfsLamp.Infrastructure.ChangeInfo;
using TfsLamp.Infrastructure.Tests.Connection;

namespace TfsLamp.Infrastructure.Tests.ChangeInfo
{
    public class when_all_work_items_are_retrieved
    {
        private static IWorkItemPopulator populator;
        private static IChangesetRetriever retriever;

        private static AllRelevantWorkItems items;

        private Establish context = () =>
            {
                var builder = new ContainerBuilder();
                builder.RegisterModule<Registrar.Registrar>();
                var container = builder.Build();
                populator = container.Resolve<IWorkItemPopulator>();
                retriever = container.Resolve<IChangesetRetriever>();
            };

        private Because of = () => items = populator.GetAssociatedWorkItemsForChangesets(retriever.GetChangesets());

        private It should_have_an_orphaned_changeset = () =>
            {
                items.OrphanChangesets.Count().ShouldEqual(1);
                items.OrphanChangesets.ElementAt(0).Id.ShouldEqual(FakeTfsOperations.ChangsetOrphan);
            };

        private It should_have_2_root_items = () =>
            {
                items.RootWorkItems.Count(wi => wi.Parent == null).ShouldEqual(2);
                items.RootWorkItems.ElementAt(0).WorkItem.Id.ShouldEqual(FakeTfsOperations.RootItem1);
                items.RootWorkItems.ElementAt(1).WorkItem.Id.ShouldEqual(FakeTfsOperations.RootItem2);
            };

        private It should_have_2_tier_2_items = () =>
            {
                var relevantWorkItem = items.GetWorkItem(FakeTfsOperations.RootItem1);
                relevantWorkItem.RelevantChildren.Count(wi => wi.Parent.WorkItem.Id == FakeTfsOperations.RootItem1).ShouldEqual(2);
                relevantWorkItem.RelevantChildren.ElementAt(0).WorkItem.Id.ShouldEqual(FakeTfsOperations.Item11);
                relevantWorkItem.RelevantChildren.ElementAt(1).WorkItem.Id.ShouldEqual(FakeTfsOperations.Item12);
            };

        private It should_have_2_tier_3_items = () =>
            {
                var relevantWorkItem = items.GetWorkItem(FakeTfsOperations.Item11);
                relevantWorkItem.RelevantChildren.Count(wi => wi.Parent.WorkItem.Id == FakeTfsOperations.Item11).ShouldEqual(2);
                relevantWorkItem.RelevantChildren.ElementAt(0).WorkItem.Id.ShouldEqual(FakeTfsOperations.Item111);
                relevantWorkItem.RelevantChildren.ElementAt(1).WorkItem.Id.ShouldEqual(FakeTfsOperations.Item112);
            };

        private It should_have_2_changesets_for_item21 = () =>
            {
                var relevantWorkItem = items.GetWorkItem(FakeTfsOperations.Item21);
                relevantWorkItem.DirectlyAssociatedChangesets.Count().ShouldEqual(2);
                relevantWorkItem.DirectlyAssociatedChangesets.ElementAt(0).Id.ShouldEqual(FakeTfsOperations.ChangsetItem21);
                relevantWorkItem.DirectlyAssociatedChangesets.ElementAt(1).Id.ShouldEqual(FakeTfsOperations.Changset2Item21);
            };

        private It should_have_1_changesets_for_all_items_except_item21 = () =>
            {
                var relevantWorkItem = items.AllWorkItems.Where(i => i.WorkItem.Id != FakeTfsOperations.Item21);
                foreach (var workItem in relevantWorkItem)
                {
                    var changesets = workItem.DirectlyAssociatedChangesets;
                    changesets.Count().ShouldEqual(1);
                    changesets.ElementAt(0).Id.ShouldEqual(workItem.WorkItem.Id);

                }
            };
    }
}
