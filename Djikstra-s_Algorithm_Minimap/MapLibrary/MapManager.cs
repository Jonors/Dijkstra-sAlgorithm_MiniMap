using System;
using System.Collections.Generic;
using System.Dynamic;
using Djikstra_s_Algorithm_Minimap.Graph;

namespace MapLibrary.Core
{
    //this manager acts as a middle man between the DataStrcuture Library and the UI, so that things dont get mixed up.
    public class MapManager
    {
        //creating a Graph called MapGraph which will be the map the vertecies will be placed on and used. Defining it to have Specefic MapVertexs and MapEdges which are inheriting from BasicVertex and Edge Properties
        public Graph<MapVertexProperty, MapEdgeProperty> MapGraph { get; private set; }

        //create two lists containing copies od the created vertexes so the are accessible and modifyable from the manager wihtout ruining the MapGraph itself
        public List<Vertex<MapVertexProperty>> AllNodes { get; private set; }
        public List<Edge<Vertex<MapVertexProperty>, MapEdgeProperty>> AllEdges { get; private set; }

        //constructor
        public MapManager()
        {
            // initialising the lists so we can use them
            MapGraph = new Graph<MapVertexProperty, MapEdgeProperty>();
            AllNodes = new List<Vertex<MapVertexProperty>>();
            AllEdges = new List<Edge<Vertex<MapVertexProperty>, MapEdgeProperty>>();
        }

        // Wrapper to make adding a place or crossing to the graph easier and more efficient
        public Vertex<MapVertexProperty> CreateMapNode(string name, float x, float y, string type)
        {
            if (x < 0 || y < 0)
            {
                Console.WriteLine("MapNodes cannot have a negative position value");
                return null;
            }

            // Tell the DataStructureLibrary/Graph to create a new blank Vertex with a name
            var vertex = MapGraph.AddVertex(name);

            // Inject custom map data into the properties of the new vertex
            vertex.Property.Setup(x, y, type);

            AllNodes.Add(vertex);

            return vertex;
        }

        // Wrapper to connect two nodes and calculate the distance
        public Edge<Vertex<MapVertexProperty>, MapEdgeProperty> ConnectNodes(
            string sourceName,
            string targetName
        )
        {
            var source = MapGraph.HasVertex(sourceName);
            var target = MapGraph.HasVertex(targetName);

            if (source != null && target != null)
            {
                var edge = MapGraph.AddEdge(source, target);
                if (edge != null)
                {
                    // Calculate the distance so the Algorithm can later use it
                    edge.Property.CalculateDistance();
                }

                AllEdges.Add(edge);
                return edge;
            }
            return null;
        }

        // Distance calculation using Dijkstra's algorithm
        public List<Vertex<MapVertexProperty>> FindShortestPath(string startName, string endName)
        {
            //initialising start and end nodes
            var startNode = MapGraph.HasVertex(startName);
            var targetNode = MapGraph.HasVertex(endName);

            if (startNode == null || targetNode == null)
                return new List<Vertex<MapVertexProperty>>();

            //Use two dictionaries tracks the absolute shortest known distance from the startNode to every other node on the map.
            //One tracks the absolute shortest total distance from the startNode to this specific node,
            var distances = new Dictionary<Vertex<MapVertexProperty>, float>();

            //and one remembers the nodes which we traveled from so we can later retrace the procedure.
            var previousNodes =
                new Dictionary<Vertex<MapVertexProperty>, Vertex<MapVertexProperty>>();

            // Using a PriorityQueue<> to keep track of unvisited nodes. The first item is the Node. The second item is the Priority (which will be its distance). The queue automatically sorts itself so the smallest distance is always pulled out first.
            var unvisitedQueue = new PriorityQueue<Vertex<MapVertexProperty>, float>();

            // We set the distance of every node in the dictionary to Infinity because we dont know how far away they are until we have visited them, except the start node which is 0 because we are already on it.
            foreach (var node in AllNodes)
            {
                distances[node] = float.MaxValue;
            }
            distances[startNode] = 0;

            // Put the start node into the queue with a priority of 0
            unvisitedQueue.Enqueue(startNode, 0);

            // Djikstras Algorithm loop
            while (unvisitedQueue.Count > 0)
            {
                //Dequeue the node with the lowest distance. .
                var currentNode = unvisitedQueue.Dequeue();
                //If the currentNode is the same as the targetNode we are trying to get too, we break out of the while loop and end the process
                if (currentNode == targetNode)
                {
                    break;
                }

                // Looping through all the edges so we can find what nodes are connected to our current node.
                foreach (var edge in AllEdges)
                {
                    if (edge.Property.Source == currentNode || edge.Property.Target == currentNode)
                    {
                        //Creating a temporary box to hold the information of the node at the other end of the edge we are currently looking at.
                        Vertex<MapVertexProperty> neighbour;
                        //Check if the currentNode is the starting or end position of the current edge
                        if (edge.Property.Source == currentNode)
                        {
                            neighbour = edge.Property.Target;
                        }
                        else
                        {
                            neighbour = edge.Property.Source;
                        }

                        // Calculate the potentially new(tentative) distance.
                        float tentativeDistance = distances[currentNode] + edge.Property.Distance;

                        // Check the ledger if the route is faster than the previously saved one
                        if (tentativeDistance < distances[neighbour])
                        {
                            // Overwrite the ledger with our new, faster time.
                            distances[neighbour] = tentativeDistance;

                            //we put our current node inside previous neighbour dictonary so we know where we traveled from and can later recreate our path
                            previousNodes[neighbour] = currentNode;

                            // Replaceing the current time it takes to get to this neighbour and putting it back in the queue. Because we found a faster way to get to this neighbor, any nodes connected to it might now have a faster route too.
                            unvisitedQueue.Enqueue(neighbour, tentativeDistance);
                        }
                    }
                }
            }

            // Create the final list to hold our path
            List<Vertex<MapVertexProperty>> finalPath = new List<Vertex<MapVertexProperty>>();

            //Create a variable to track where we currently are, starting at the targetNode/endpoint.
            Vertex<MapVertexProperty>? step = targetNode;

            // Start going backward. Keep looping until our "step" becomes null .
            while (step != null)
            {
                // Add the town we are currently standing in to our path list.
                finalPath.Add(step);

                // Pull information form dictionary on where we came form to reach this postion
                if (previousNodes.ContainsKey(step))
                {
                    // If yes, move to it
                    step = previousNodes[step];
                }
                else
                {
                    // If no, it means we have reached the startNode (which has no previous node).
                    step = null;
                }
            }

            // .Reverse() is a built-in C# method that instantly flips the list,because right now the logic has the path saved back to front
            finalPath.Reverse();

            return finalPath;
        }
    };
}
