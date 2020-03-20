using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLab4
{
    public class VertexPair
    {
        public Vertex First { get; set; }

        public Vertex Second { get; set; }

        public VertexPair(Vertex first, Vertex second)
        {
            First = first;
            Second = second;
        }

        public int SmallestVertexID()
        {
            if (First.ID < Second.ID)
            {
                return First.ID;
            }
            else
            {
                return Second.ID;
            }
        }
    }
}
