using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TfsLamp.Infrastructure.Configuration;

namespace TfsLamp.Infrastructure.Connection
{
    public class TfsOperations : ITfsOperations
    {
        private readonly ITfsConnectionConfig _config;
        private TfsTeamProjectCollection _projectCollection;
        private VersionControlServer _versionControl;
        private WorkItemStore _workItemStore;

        public TfsOperations(ITfsConnectionConfig config)
        {
            _config = config;
        }

        protected virtual TfsTeamProjectCollection ProjectCollection
        {
            get
            {
                if (_projectCollection == null)
                {
                    _projectCollection = new TfsTeamProjectCollection(new Uri(_config.Server), new System.Net.NetworkCredential(_config.Username, _config.Password));
                    _projectCollection.EnsureAuthenticated();
                }
                return _projectCollection;
            }
        }

        protected virtual VersionControlServer VersionControl
        {
            get { return _versionControl ?? (_versionControl = ProjectCollection.GetService<VersionControlServer>()); }
        }

        protected virtual WorkItemStore WorkItemStore
        {
            get { return _workItemStore ?? (_workItemStore = ProjectCollection.GetService<WorkItemStore>()); }
        }

        public TfsWorkItem GetWorkItem(int id)
        {
            return MapWorkItem(WorkItemStore.GetWorkItem(id));
        }

        public IEnumerable<TfsChangeset> GetMergeCandidates(string sourceBranchName, string targetBranchName)
        {
            return VersionControl.GetMergeCandidates(sourceBranchName, targetBranchName, RecursionType.Full)
                .Select(mc => MapChangeset(mc.Changeset));
        }

        public IEnumerable<TfsChangeset> GetChangesets(string sourceBranchName, int firstId, int lastId)
        {
            return VersionControl.QueryHistory(sourceBranchName, VersionSpec.Latest, 0, RecursionType.Full, null,
                                               new ChangesetVersionSpec(firstId), 
                                               new ChangesetVersionSpec(lastId), 
                                               Int32.MaxValue,
                                               false,
                                               false).Cast<Changeset>().Select(MapChangeset);
        }

        public IEnumerable<TfsChangeset> GetChangesetsInMergedChangeset(string sourceBranchName, string targetBranchName, int mergeChangesetId)
        {
            var merges = VersionControl.TrackMerges(new[] { mergeChangesetId }, new ItemIdentifier(targetBranchName), new[] { new ItemIdentifier(sourceBranchName) }, null);
            return merges.Select(m => VersionControl.GetChangeset(m.SourceChangeset.ChangesetId)).Select(MapChangeset);
        }

        protected virtual TfsChangeset MapChangeset(Changeset changeset)
        {
            return new TfsChangeset
                {
                    Id = changeset.ChangesetId,
                    Date = changeset.CreationDate,
                    Author = changeset.Owner,
                    Comment = changeset.Comment,
                    Url = GenerateUrl(changeset.ArtifactUri),
                    WorkItems = changeset.WorkItems.Select(MapWorkItem).ToList(),
                };
        }

        protected virtual TfsWorkItem MapWorkItem(WorkItem workItem)
        {
            return new TfsWorkItem
                {
                    Id = workItem.Id,
                    Title = workItem.Title,
                    Type = workItem.Type.Name,
                    Description = workItem.Description,
                    Url = GenerateUrl(workItem.Uri),
                    Links = workItem.WorkItemLinks.Cast<WorkItemLink>().Select(MapWorkItemLink),
                };
        }

        protected virtual TfsWorkItemLink MapWorkItemLink(WorkItemLink workItemLink)
        {
            return new TfsWorkItemLink
                {
                    Name = workItemLink.LinkTypeEnd.Name,
                    LinkedToWorkItemId = workItemLink.TargetId,
                };
        }

        protected virtual Uri GenerateUrl(Uri tfsGeneratedUri)
        {
            return new Uri(string.Format("{0}?url={1}", tfsGeneratedUri, _config.Server));
        }
    }
}