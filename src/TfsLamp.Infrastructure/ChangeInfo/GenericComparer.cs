using System;
using System.Collections.Generic;

namespace TfsLamp.Infrastructure.ChangeInfo
{
    public class GenericComparer<TCompare> : IComparer<TCompare>
    {
        protected readonly Func<TCompare, TCompare, int> Comparer;

        public GenericComparer(Func<TCompare, TCompare, int> comparer)
        {
            Comparer = comparer;
        }

        public virtual int Compare(TCompare x, TCompare y)
        {
            return Comparer(x, y);
        }
    }
}