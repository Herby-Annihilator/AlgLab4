using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLab4
{
    /// <summary>
    /// ребро в графе
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// вершина с одной стороны ребра
        /// </summary>
        public Vertex FirstVertex { get; set; }
        /// <summary>
        /// вершина с другой стророны ребра
        /// </summary>
        public Vertex SecondVertex { get; set; }
        /// <summary>
        /// принадлежность данного ребра конктретной компоненте связности (проще говоря, конкретному графу)
        /// </summary>
        public int ComponenetID { get; set; }

        public Edge(Vertex first, Vertex second, int compid)
        {
            SecondVertex = second;
            FirstVertex = first;
            ComponenetID = compid;
        }

        public Edge Clone()
        {
            return new Edge(FirstVertex.Clone(), SecondVertex.Clone(), ComponenetID);
        }
    }
}
