using System.Collections.Generic;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.Infrastructure.ChangeInfo
{
    public interface IChangesetRetriever
    {
        IEnumerable<TfsChangeset> GetChangesets();
    }
}