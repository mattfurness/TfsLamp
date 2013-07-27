using System.Collections.Generic;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TfsLamp.Infrastructure.Connection
{
    public interface ITfsOperations
    {
        TfsWorkItem GetWorkItem(int id);
        IEnumerable<TfsChangeset> GetMergeCandidates(string sourceBranchName, string targetBranchName);
        IEnumerable<TfsChangeset> GetChangesets(string sourceBranchName, int firstId, int lastId);
    }
}