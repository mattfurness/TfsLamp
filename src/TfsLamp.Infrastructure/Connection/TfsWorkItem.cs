using System;
using System.Collections.Generic;

namespace TfsLamp.Infrastructure.Connection
{
    public class TfsWorkItem
    {
        public TfsWorkItem()
        {
            Links = new List<TfsWorkItemLink>();
        }

        public int Id { get; set; }
        public IEnumerable<TfsWorkItemLink> Links { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri Url { get; set; }
    }
}