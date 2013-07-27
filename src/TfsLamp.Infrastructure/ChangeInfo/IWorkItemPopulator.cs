using System.Collections.Generic;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.Infrastructure.ChangeInfo
{
    public interface IWorkItemPopulator
    {
        AllRelevantWorkItems GetAssociatedWorkItemsForChangesets(IEnumerable<TfsChangeset> changesets);
    }
}