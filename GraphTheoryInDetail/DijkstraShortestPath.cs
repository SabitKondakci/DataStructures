using System;
using Priority_Queue;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheoryInDetail
{
    // shortest path for directed graphs , non negative edge weights
    // O(E*log(V)) time complexity
    class DijkstraShortestPath
    {
        private List<KeyValuePair<int, double>>[] myGraph;
        public List<KeyValuePair<int, double>>[] GetGraph
        {
            get { return myGraph; }
        }
        public DijkstraShortestPath(int numOfVertices)
        {
            myGraph = new List<KeyValuePair<int, double>>[numOfVertices];

            for (int i = 0; i < numOfVertices; i++)
            {
                myGraph[i] = new List<KeyValuePair<int, double>>();
            }
        }
        public void CreateDirectedEdges(int fromVer, int toVer, double weight)
        {
            myGraph[fromVer].Add(new KeyValuePair<int, double>(toVer, weight));
        }
        public double LazyDijkstraShortPath(List<KeyValuePair<int, double>>[] graphAdjList,
            int startNode, int endNode, out List<int> path)
        {
            int numOfVertices = graphAdjList.Length;

            if (endNode < 0 || endNode >=numOfVertices ) throw new ArgumentOutOfRangeException("Invalid node index");
            if (startNode < 0 || startNode >= numOfVertices) throw new ArgumentOutOfRangeException("Invalid node index");
            
            
            bool[] visited = new bool[numOfVertices];
            double[] distance = new double[numOfVertices];
            List<int> resultPath = new List<int>();//path you visit
            int[] prevNode = new int[numOfVertices];

            for (int i = 0; i < numOfVertices; i++)
            {
                distance[i] = double.PositiveInfinity;
            }

            distance[startNode] = 0;

            SimplePriorityQueue<int, double> priorityQueue =
                new SimplePriorityQueue<int, double>();

            priorityQueue.Enqueue(startNode, 0.0);

            while (priorityQueue.Count != 0)
            {
                int indexofNode = priorityQueue.Dequeue();
                visited[indexofNode] = true;

                foreach (var keyValuePair in graphAdjList[indexofNode])
                {
                    double minValue = keyValuePair.Value;

                    if (distance[keyValuePair.Key] < minValue)
                        continue;
                    
                    if (visited[keyValuePair.Key])
                        continue;

                    double distNew = distance[indexofNode] + keyValuePair.Value;

                    if (distNew < distance[keyValuePair.Key])
                    {
                        prevNode[keyValuePair.Key] = indexofNode;
                        distance[keyValuePair.Key] = distNew;
                        priorityQueue.Enqueue(keyValuePair.Key, distNew);
                    }
                }

                if (indexofNode == endNode)
                {
                    for (int at = endNode; at != startNode; at = prevNode[at]) 
                        resultPath.Add(at);

                    if(!resultPath.Contains(startNode))
                        resultPath.Add(startNode);
                    if(!resultPath.Contains(endNode))
                        resultPath.Add(endNode);

                    resultPath.Reverse();

                    path = resultPath;
                    return distance[endNode];
                }

            }

            path = resultPath;
            return double.PositiveInfinity;
        }
        public double EagerDijkstraShortPath(List<KeyValuePair<int, double>>[] graphAdjList,
            int startNode, int endNode, out List<int> path)
        {
            int numOfVertices = graphAdjList.Length;

            if (endNode < 0 || endNode >= numOfVertices) throw new ArgumentOutOfRangeException("Invalid node index");
            if (startNode < 0 || startNode >= numOfVertices) throw new ArgumentOutOfRangeException("Invalid node index");

            bool[] visited = new bool[numOfVertices];
            double[] distance = new double[numOfVertices];
            List<int> resultPath = new List<int>();//path you visit
            int[] prevNode = new int[numOfVertices];

            for (int i = 0; i < numOfVertices; i++)
            {
                distance[i] = double.PositiveInfinity;
            }

            distance[startNode] = 0;

            SimplePriorityQueue<int, double> priorityQueue =
                new SimplePriorityQueue<int, double>();

            priorityQueue.Enqueue(startNode, 0.0);

            while (priorityQueue.Count != 0)
            {
                int indexofNode = priorityQueue.Dequeue();
                visited[indexofNode] = true;

                foreach (var keyValuePair in graphAdjList[indexofNode])
                {
                    double minValue = keyValuePair.Value;

                    if (distance[keyValuePair.Key] < minValue)
                        continue;

                    if (visited[keyValuePair.Key])
                        continue;

                    double distNew = distance[indexofNode] + keyValuePair.Value;

                    if (distNew < distance[keyValuePair.Key])
                    {
                        prevNode[keyValuePair.Key] = indexofNode;
                        distance[keyValuePair.Key] = distNew;
                        if (!priorityQueue.EnqueueWithoutDuplicates(keyValuePair.Key, distNew))
                            priorityQueue.UpdatePriority(keyValuePair.Key, distNew);
                        
                    }
                }

                if (indexofNode == endNode)
                {
                    for (int at = endNode; at != startNode; at = prevNode[at])
                        resultPath.Add(at);

                    if (!resultPath.Contains(startNode))
                        resultPath.Add(startNode);
                    if (!resultPath.Contains(endNode))
                        resultPath.Add(endNode);

                    resultPath.Reverse();

                    path = resultPath;
                    return distance[endNode];
                }

            }

            path = resultPath;
            return double.PositiveInfinity;
        }
    }
}
