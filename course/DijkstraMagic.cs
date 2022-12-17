using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace course
{
    public class Edge
    {
        public Vertex Vertex1 { get; set; }
        public Vertex Vertex2 { get; set; }
        public int Weight { get; set; }
    }

    public class Vertex
    {
        public double X { get; set; }
        public double Y { get; set; }
        public List<Vertex> Neighbours { get; set; } = new List<Vertex>();
    }

    public class Graph
    {
        public List<Vertex> Vertices { get; set; }
        public List<Edge> Edges { get; set; }

        public void addVertex(Vertex v)
        {
            Vertices.Add(v);
        }
        public void addEdge(Edge e)
        {
            Edges.Add(e);
        }
    }

    public static class DijkstraMagic
    {
        public static int Infinity = 9999999;

        public static Graph graph { get; set; }
        public static Dictionary<Vertex, int> Distances { get; set; } = new Dictionary<Vertex, int>();
        public static Dictionary<Vertex, Vertex> Previous { get; set; } = new Dictionary<Vertex, Vertex>();
        public static Dictionary<Vertex, bool> Visited { get; set; } = new Dictionary<Vertex, bool>();
        public static Vertex StartVertex { get; set; }
        public static Vertex EndVertex { get; set; }

        public static List<Vertex> FindShortestPath()
        {
            var (distances, previous) = RunAlgorithm();

            List<Vertex> path = new List<Vertex>();

            var currentVertex = EndVertex;

            while (currentVertex != StartVertex)
            {
                path.Insert(0, currentVertex);
                currentVertex = previous[currentVertex];
            }
            path.Insert(0, StartVertex);

            return path;
        }

        public static Vertex FindNearestVertex()
        {
            int minDistance = Infinity;
            Vertex nearetVertex = null;

            (new List<Vertex>(Distances.Keys)).ForEach(v =>
            {
                if (!Visited[v] && Distances[v] < minDistance)
                {
                    minDistance = Distances[v];
                    nearetVertex = v;
                }
            });

            return nearetVertex;
        }

        public static (Dictionary<Vertex, int>, Dictionary<Vertex, Vertex>) RunAlgorithm()
        {
            // по умолчанию все расстояния неизвестны (бесконечны)
            foreach (Vertex v in graph.Vertices)
            {
                Distances.Add(v, Infinity);
                Visited[v] = false;
            }

            // расстояние до стартовой вершины равно 0
            Distances[StartVertex] = 0;

            // ищем самую близкую вершину из необработанных
            Vertex activeVertex = FindNearestVertex();

            void handleVertex(Vertex vertex)
            {
                // расстояние до вершины
                int activeVertexDistance = Distances[vertex];

                // смежные вершины
                List<Vertex> neighbours =
                    graph.Vertices.Find(v => v == activeVertex).Neighbours;

                if (neighbours != null)
                {
                    // для всех смежных вершин пересчитать расстояния
                    neighbours.ForEach(neighbourVertex =>
                    {
                        // известное на данный момент расстояние
                        int currentNeighbourDistance = Distances[neighbourVertex];

                        // вычисленное расстояние
                        int newNeighbourDistance =
                          activeVertexDistance + graph.Edges
                                 .Find(e => e.Vertex1 == activeVertex && e.Vertex2 == neighbourVertex
                                 || e.Vertex2 == activeVertex && e.Vertex1 == neighbourVertex).Weight;

                        if (newNeighbourDistance < currentNeighbourDistance)
                        {
                            Distances[neighbourVertex] = newNeighbourDistance;
                            Previous[neighbourVertex] = vertex;
                        }

                    });
                }

                // пометить вершину как посещенную
                Visited[vertex] = true;
            }

            // продолжаем цикл, пока остаются необработанные вершины
            while (activeVertex != null)
            {
                handleVertex(activeVertex);
                activeVertex = FindNearestVertex();
            }

            return (Distances, Previous);
        }
    }
}
