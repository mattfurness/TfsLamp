using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TfsLamp.Infrastructure.Connection;
using System.Linq;

namespace TfsLamp.Infrastructure.Tests.Connection
{
    public class FakeTfsOperations : ITfsOperations
    {
        private readonly IDictionary<int, TfsChangeset> _allChangesets;
        private readonly IDictionary<int, TfsWorkItem> _allWorkItems;

        public const int RootItem1 = 1;
        public const int Item11 = 2;
        public const int Item12 = 3;
        public const int Item111 = 4;
        public const int Item112 = 5;
        public const int RootItem2 = 6;
        public const int Item21 = 7;

        public const int ChangsetItem1 = 1;
        public const int ChangsetItem11 = 2;
        public const int ChangsetItem12 = 3;
        public const int ChangsetItem111 = 4;
        public const int ChangsetItem112 = 5;
        public const int ChangsetItem2 = 6;
        public const int ChangsetItem21 = 7;
        public const int Changset2Item21 = 8;
        public const int ChangsetOrphan = 9;

        public FakeTfsOperations()
        {
            _allChangesets = new Dictionary<int, TfsChangeset>();
            _allWorkItems = new Dictionary<int, TfsWorkItem>();

            CreateChangeset(1, CreateWorkItem(1, null));
            CreateChangeset(2, CreateWorkItem(2, 1));
            CreateChangeset(3, CreateWorkItem(3, 1));
            CreateChangeset(4, CreateWorkItem(4, 2));
            CreateChangeset(5, CreateWorkItem(5, 2));
            
            CreateChangeset(6, CreateWorkItem(6, null));

            var tfsWorkItem7 = CreateWorkItem(7, 6);
            CreateChangeset(7, tfsWorkItem7);
            CreateChangeset(8, tfsWorkItem7);
            
            CreateChangeset(9, null);
        }

        public int GetChangesetCount()
        {
            return _allChangesets.Count();
        }

        protected virtual TfsWorkItem CreateWorkItem(int id, int? parentId)
        {
            var tfsWorkItem = new TfsWorkItem
                {
                    Id = id,
                    Description = id.ToString(),
                    Title = id.ToString(),
                    Url = new Uri("http://" + id.ToString()),
                };
            if (parentId != null)
                tfsWorkItem.Links = new []{new TfsWorkItemLink{LinkedToWorkItemId = parentId.Value, Name = "Parent"} };
            _allWorkItems.Add(id, tfsWorkItem);
            return tfsWorkItem;
        }

        protected virtual void CreateChangeset(int id, TfsWorkItem workItem)
        {
            var tfsChangeset = new TfsChangeset
                {
                    Id = id,
                    Comment = id.ToString(),
                    Author = id.ToString(),
                    Url = new Uri("http://" + id.ToString()),
                };

            if (workItem != null)
                tfsChangeset.WorkItems = new []{ workItem };
            _allChangesets.Add(id, tfsChangeset);
        }

        public TfsWorkItem GetWorkItem(int id)
        {
            return _allWorkItems[id];
        }

        public IEnumerable<TfsChangeset> GetMergeCandidates(string sourceBranchName, string targetBranchName)
        {
            return _allChangesets.Values;
        }

        public IEnumerable<TfsChangeset> GetChangesets(string sourceBranchName, int firstId, int lastId)
        {
            return _allChangesets.Values;
        }

        public IEnumerable<TfsChangeset> GetChangesetsInMergedChangeset(string sourceBranchName, string targetBranchName, int mergeChangesetId)
        {
            return _allChangesets.Values;
        }
    }
}