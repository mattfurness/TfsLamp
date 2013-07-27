using System;
using System.Collections.Generic;

namespace TfsLamp.Infrastructure.Connection
{
    public class TfsChangeset
    {
        public TfsChangeset()
        {
            WorkItems = new TfsWorkItem[0];
        }

        public int Id { get; set; }
        public IEnumerable<TfsWorkItem> WorkItems { get; set; }

        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public Uri Url { get; set; }
    }
}