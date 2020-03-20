using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLab4
{
    public class Vertex
    {
        /// <summary>
        /// порядковый номер, нужный для построения матрицы смежности в дальнейшем
        /// </summary>
        public int ID { get; set; }

        public int X { get; private set; }

        public int Y { get; private set; }

        public Vertex(int x, int y, int id, int compid, List<Edge> edges = null)
        {
            X = x;
            Y = y;
            ID = id;
            Edges = edges;
            ComponentID = compid;
        }
        /// <summary>
        /// список ребер, инцидентных данной вершине
        /// </summary>
        public List<Edge> Edges { get; set; }

        /// <summary>
        /// принадлежность данной вершины конктретной компоненте связности
        /// </summary>
        public int ComponentID { get; set; }

        public double LenghtFromVertex(Vertex vertex)
        {
            int x = Math.Abs(this.X - vertex.X);
            int y = Math.Abs(this.Y - vertex.Y);

            x *= x;
            y *= y;

            return Math.Sqrt(x + y);
        }

        public Vertex Clone()
        {
            List<Edge> edges = new List<Edge>();

            for (int i = 0; i < Edges.Count; i++)
            {
                edges.Add(Edges[i].Clone());
            }

            return new Vertex(X, Y, ID, ComponentID, edges);
        }
    }
}
