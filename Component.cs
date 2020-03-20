using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLab4
{
    /// <summary>
    /// компонента связности
    /// </summary>
    public class Component
    {
        public int EdgeCount { get { return Edges.Count; } }
        public int VertexCount { get { return Vertices.Count; } }

        public List<Vertex> Vertices { get; private set; }

        public List<Edge> Edges { get; private set; }

        public int ID { get; private set; }

        public Component(int id, List<Vertex> vertices, List<Edge> edges = null)
        {
            if (vertices == null)
            {
                Vertices = new List<Vertex>();
                Vertices.Add(new Vertex(0, 0, -1, -1));
            }
            else
            {
                Vertices = vertices;
            }
            ID = id;
        }
        /// <summary>
        /// Производит слияние двух компонент связности. В каждой компоненте должна быть хотябы одна вершина
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Component MergeComponents(Component first, Component second)
        {
            List<VertexPair> shortestPathVertexs = new List<VertexPair>();
            shortestPathVertexs.Add(new VertexPair(first.Vertices[0], second.Vertices[0]));
            
            // для каждой вершины из первой компоненты связности
            for (int i = 0; i < first.VertexCount; i++)
            {
                // для каждой вершины из второй компоненты связности
                for (int j = 0; j < second.VertexCount; j++)
                {
                    // если есть пара с меньшим расстоянием
                    if (first.Vertices[i].LenghtFromVertex(second.Vertices[j]) < shortestPathVertexs[0].First.LenghtFromVertex(shortestPathVertexs[0].Second))
                    {
                        // внести ее в список
                        shortestPathVertexs.Clear();
                        shortestPathVertexs.Add(new VertexPair(first.Vertices[i], second.Vertices[j]));
                    }
                    // если есть пара с таким же расстоянием
                    else if (first.Vertices[i].LenghtFromVertex(second.Vertices[j]) == shortestPathVertexs[0].First.LenghtFromVertex(shortestPathVertexs[0].Second))
                    {
                        // добавить ее в список
                        shortestPathVertexs.Add(new VertexPair(first.Vertices[i], second.Vertices[j]));
                    }
                }
            }
            // если есть в shortestPathVertexs более одной пары вершин, то выбрать пару с наименьшим ID у вершины
            VertexPair workingPair;
            if (shortestPathVertexs.Count > 1)
            {
                workingPair = shortestPathVertexs[0];
                for (int i = 1; i < shortestPathVertexs.Count; i++)
                {
                    if (workingPair.SmallestVertexID() < shortestPathVertexs[i].SmallestVertexID())
                    {
                        workingPair = shortestPathVertexs[i];
                    }
                }
            }
            else
            {
                workingPair = shortestPathVertexs[0];
            }
            // соединить вршины из пары

            Edge edge = new Edge(workingPair.First, workingPair.Second, first.ID);

            workingPair.First.Edges.Add(edge);
            workingPair.Second.Edges.Add(edge);
            first.Edges.Add(edge);
            first.Vertices.AddRange(second.Vertices);
            first.Edges.AddRange(second.Edges);
            return first;
        }

        public Component Clone()
        {
            List<Vertex> verties = new List<Vertex>();
            List<Edge> edges = new List<Edge>();
            int id = ID;

            for (int i = 0; i < VertexCount; i++)
            {
                verties.Add(Vertices[i]);
            }

            return new Component(id, verties, edges);
        }

        public static double SmallestPathBetweenComponents(Component first, Component second)
        {
            if (first.Vertices.Count == 0 || second.Vertices.Count == 0)
            {
                return 0;
            }

            //VertexPair smallestPathBetweenVertex = new VertexPair(first.Vertices[0], second.Vertices[0]);
            double smallestPath = first.Vertices[0].LenghtFromVertex(second.Vertices[0]);

            for (int i = 0; i < first.Vertices.Count; i++)
            {
                for (int j = 0; j < second.Vertices.Count; j++)
                {
                    double path = first.Vertices[i].LenghtFromVertex(second.Vertices[j]);
                    if (path < smallestPath)
                    {
                        smallestPath = path;
                        //smallestPathBetweenVertex = new VertexPair(first.Vertices[i], second.Vertices[j]);
                    }
                }
            }
            return smallestPath;
        }
    }
}
