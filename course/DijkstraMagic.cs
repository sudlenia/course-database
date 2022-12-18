using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace course
{
    public class Coord
    {
        public double X { get; set; }
        public double Y { get; set; }
    }


    public static class Coordinates
    {
        public static Coord IKIT { get; set; } = new Coord() { X = 55.994474805136846, Y = 92.79788544574697 };
        public static Coord Mira81 { get; set; } = new Coord() { X = 56.01142414341906, Y = 92.86212445763783 };
        public static Coord Mira124 { get; set; } = new Coord() { X = 56.01079310426668, Y = 92.8430333408457 };
        public static Coord Gazety5 { get; set; } = new Coord() { X = 55.990488225723084, Y = 92.94134369144753 };
        public static Coord Svobodnyi76 { get; set; } = new Coord() { X = 56.00619184444418, Y = 92.7708360287757 };
        public static Coord KarlaMarksa135 { get; set; } = new Coord() { X = 56.009657449935865, Y = 92.861770901567 };
        public static Coord AdyLebedevoi109 { get; set; } = new Coord() { X = 56.01507910034775, Y = 92.85036576453813 };
    }

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
        public static int Infinity = 99999999;

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

        public static string getAdress(double x, double y)
        {
            if (x == Coordinates.IKIT.X && y == Coordinates.IKIT.Y) return "Институт Космических и Информационных Технологий";
            if (x == Coordinates.Mira81.X && y == Coordinates.Mira81.Y) return "Мира 81";
            if (x == Coordinates.Mira124.X && y == Coordinates.Mira124.Y) return "Мира 124";
            if (x == Coordinates.Gazety5.X && y == Coordinates.Gazety5.Y) return "газеты Пионерской Правды 5";
            if (x == Coordinates.Svobodnyi76.X && y == Coordinates.Svobodnyi76.Y) return "Свободный 76";
            if (x == Coordinates.KarlaMarksa135.X && y == Coordinates.KarlaMarksa135.Y) return "Карла Маркса 135";
            if (x == Coordinates.AdyLebedevoi109.X && y == Coordinates.AdyLebedevoi109.Y) return "Ады Лебедевой 109";

            return "";
        }

        public static int GetDistance(double x1, double y1, double x2, double y2)
        {
            if ((x1 == Coordinates.IKIT.X && y1 == Coordinates.IKIT.Y 
                && x2 == Coordinates.Mira81.X && y2 == Coordinates.Mira81.Y) 
                || ( (x1 == Coordinates.Mira81.X && y1 == Coordinates.Mira81.Y 
                && x2 == Coordinates.IKIT.X && y2 == Coordinates.IKIT.Y)))
            {
                return 8200;
            }
            if ((x1 == Coordinates.IKIT.X && y1 == Coordinates.IKIT.Y 
                && x2 == Coordinates.Mira124.X && y2 == Coordinates.Mira124.Y) 
                || ( (x1 == Coordinates.Mira124.X && y1 == Coordinates.Mira124.Y 
                && x2 == Coordinates.IKIT.X && y2 == Coordinates.IKIT.Y)))
            {
                return 5200;
            }
            if ((x1 == Coordinates.IKIT.X && y1 == Coordinates.IKIT.Y 
                && x2 == Coordinates.Gazety5.X && y2 == Coordinates.Gazety5.Y) 
                || ( (x1 == Coordinates.Gazety5.X && y1 == Coordinates.Gazety5.Y 
                && x2 == Coordinates.IKIT.X && y2 == Coordinates.IKIT.Y)))
            {
                return 14100;
            }
            if ((x1 == Coordinates.IKIT.X && y1 == Coordinates.IKIT.Y 
                && x2 == Coordinates.Svobodnyi76.X && y2 == Coordinates.Svobodnyi76.Y) 
                || ( (x1 == Coordinates.Svobodnyi76.X && y1 == Coordinates.Svobodnyi76.Y 
                && x2 == Coordinates.IKIT.X && y2 == Coordinates.IKIT.Y)))
            {
                return 5300;
            }
            if ((x1 == Coordinates.IKIT.X && y1 == Coordinates.IKIT.Y 
                && x2 == Coordinates.KarlaMarksa135.X && y2 == Coordinates.KarlaMarksa135.Y) 
                || ( (x1 == Coordinates.KarlaMarksa135.X && y1 == Coordinates.KarlaMarksa135.Y 
                && x2 == Coordinates.IKIT.X && y2 == Coordinates.IKIT.Y)))
            {
                return 7900;
            }
            if ((x1 == Coordinates.IKIT.X && y1 == Coordinates.IKIT.Y 
                && x2 == Coordinates.AdyLebedevoi109.X && y2 == Coordinates.AdyLebedevoi109.Y) 
                || ( (x1 == Coordinates.AdyLebedevoi109.X && y1 == Coordinates.AdyLebedevoi109.Y 
                && x2 == Coordinates.IKIT.X && y2 == Coordinates.IKIT.Y)))
            {
                return 6300;
            }
            if ((x1 == Coordinates.Mira81.X && y1 == Coordinates.Mira81.Y 
                && x2 == Coordinates.Mira124.X && y2 == Coordinates.Mira124.Y) 
                || ( (x1 == Coordinates.Mira124.X && y1 == Coordinates.Mira124.Y 
                && x2 == Coordinates.Mira81.X && y2 == Coordinates.Mira81.Y)))
            {
                return 1100;
            }
            if ((x1 == Coordinates.Mira81.X && y1 == Coordinates.Mira81.Y 
                && x2 == Coordinates.Gazety5.X && y2 == Coordinates.Gazety5.Y) 
                || ( (x1 == Coordinates.Gazety5.X && y1 == Coordinates.Gazety5.Y 
                && x2 == Coordinates.Mira81.X && y2 == Coordinates.Mira81.Y)))
            {
                return 7900;
            }
            if ((x1 == Coordinates.Mira81.X && y1 == Coordinates.Mira81.Y 
                && x2 == Coordinates.Svobodnyi76.X && y2 == Coordinates.Svobodnyi76.Y) 
                || ( (x1 == Coordinates.Svobodnyi76.X && y1 == Coordinates.Svobodnyi76.Y 
                && x2 == Coordinates.Mira81.X && y2 == Coordinates.Mira81.Y)))
            {
                return 6900;
            }
            if ((x1 == Coordinates.Mira81.X && y1 == Coordinates.Mira81.Y 
                && x2 == Coordinates.KarlaMarksa135.X && y2 == Coordinates.KarlaMarksa135.Y) 
                || ( (x1 == Coordinates.KarlaMarksa135.X && y1 == Coordinates.KarlaMarksa135.Y 
                && x2 == Coordinates.Mira81.X && y2 == Coordinates.Mira81.Y)))
            {
                return 700;
            }
            if ((x1 == Coordinates.Mira81.X && y1 == Coordinates.Mira81.Y 
                && x2 == Coordinates.AdyLebedevoi109.X && y2 == Coordinates.AdyLebedevoi109.Y) 
                || ( (x1 == Coordinates.AdyLebedevoi109.X && y1 == Coordinates.AdyLebedevoi109.Y 
                && x2 == Coordinates.Mira81.X && y2 == Coordinates.Mira81.Y)))
            {
                return 1200;
            }
            if ((x1 == Coordinates.Mira124.X && y1 == Coordinates.Mira124.Y 
                && x2 == Coordinates.Gazety5.X && y2 == Coordinates.Gazety5.Y) 
                || ( (x1 == Coordinates.Gazety5.X && y1 == Coordinates.Gazety5.Y 
                && x2 == Coordinates.Mira124.X && y2 == Coordinates.Mira124.Y)))
            {
                return 9000;
            }
            if ((x1 == Coordinates.Mira124.X && y1 == Coordinates.Mira124.Y 
                && x2 == Coordinates.Svobodnyi76.X && y2 == Coordinates.Svobodnyi76.Y) 
                || ( (x1 == Coordinates.Svobodnyi76.X && y1 == Coordinates.Svobodnyi76.Y 
                && x2 == Coordinates.Mira124.X && y2 == Coordinates.Mira124.Y)))
            {
                return 5800;
            }
            if ((x1 == Coordinates.Mira124.X && y1 == Coordinates.Mira124.Y 
                && x2 == Coordinates.KarlaMarksa135.X && y2 == Coordinates.KarlaMarksa135.Y) 
                || ( (x1 == Coordinates.KarlaMarksa135.X && y1 == Coordinates.KarlaMarksa135.Y 
                && x2 == Coordinates.Mira124.X && y2 == Coordinates.Mira124.Y)))
            {
                return 1300;
            }
            if ((x1 == Coordinates.Mira124.X && y1 == Coordinates.Mira124.Y 
                && x2 == Coordinates.AdyLebedevoi109.X && y2 == Coordinates.AdyLebedevoi109.Y) 
                || ( (x1 == Coordinates.AdyLebedevoi109.X && y1 == Coordinates.AdyLebedevoi109.Y 
                && x2 == Coordinates.Mira124.X && y2 == Coordinates.Mira124.Y)))
            {
                return 1000;
            }
            if ((x1 == Coordinates.Gazety5.X && y1 == Coordinates.Gazety5.Y 
                && x2 == Coordinates.Svobodnyi76.X && y2 == Coordinates.Svobodnyi76.Y) 
                || ( (x1 == Coordinates.Svobodnyi76.X && y1 == Coordinates.Svobodnyi76.Y 
                && x2 == Coordinates.Gazety5.X && y2 == Coordinates.Gazety5.Y)))
            {
                return 15200;
            }
            if ((x1 == Coordinates.Gazety5.X && y1 == Coordinates.Gazety5.Y 
                && x2 == Coordinates.KarlaMarksa135.X && y2 == Coordinates.KarlaMarksa135.Y) 
                || ( (x1 == Coordinates.KarlaMarksa135.X && y1 == Coordinates.KarlaMarksa135.Y 
                && x2 == Coordinates.Gazety5.X && y2 == Coordinates.Gazety5.Y)))
            {
                return 9500;
            }
            if ((x1 == Coordinates.Gazety5.X && y1 == Coordinates.Gazety5.Y 
                && x2 == Coordinates.AdyLebedevoi109.X && y2 == Coordinates.AdyLebedevoi109.Y) 
                || ( (x1 == Coordinates.AdyLebedevoi109.X && y1 == Coordinates.AdyLebedevoi109.Y 
                && x2 == Coordinates.Gazety5.X && y2 == Coordinates.Gazety5.Y)))
            {
                return 9600;
            }
            if ((x1 == Coordinates.Svobodnyi76.X && y1 == Coordinates.Svobodnyi76.Y 
                && x2 == Coordinates.KarlaMarksa135.X && y2 == Coordinates.KarlaMarksa135.Y) 
                || ( (x1 == Coordinates.KarlaMarksa135.X && y1 == Coordinates.KarlaMarksa135.Y 
                && x2 == Coordinates.Svobodnyi76.X && y2 == Coordinates.Svobodnyi76.Y)))
            {
                return 7700;
            }
            if ((x1 == Coordinates.Svobodnyi76.X && y1 == Coordinates.Svobodnyi76.Y 
                && x2 == Coordinates.AdyLebedevoi109.X && y2 == Coordinates.AdyLebedevoi109.Y) 
                || ( (x1 == Coordinates.AdyLebedevoi109.X && y1 == Coordinates.AdyLebedevoi109.Y 
                && x2 == Coordinates.Svobodnyi76.X && y2 == Coordinates.Svobodnyi76.Y)))
            {
                return 7300;
            }
            if ((x1 == Coordinates.KarlaMarksa135.X && y1 == Coordinates.KarlaMarksa135.Y 
                && x2 == Coordinates.AdyLebedevoi109.X && y2 == Coordinates.AdyLebedevoi109.Y) 
                || ( (x1 == Coordinates.AdyLebedevoi109.X && y1 == Coordinates.AdyLebedevoi109.Y 
                && x2 == Coordinates.KarlaMarksa135.X && y2 == Coordinates.KarlaMarksa135.Y)))
            {
                return 1400;
            }
            return 0;
        }

    }
}
