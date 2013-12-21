using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDTrees
{
    public struct Pair<T>
    {
        public Pair(T a, T b)
            : this()
        {
            this.A = a;
            this.B = b;
        }

        public T A { get; set; }
        public T B { get; set; }
    }
}
