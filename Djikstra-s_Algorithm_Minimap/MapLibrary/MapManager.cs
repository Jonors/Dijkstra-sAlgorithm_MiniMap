using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Djikstra_s_Algorithm_Minimap.Graph;
using Microsoft.VisualBasic;

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

            // using 2 diffrent Dictionary<> to keep track of:
            // the distances between the nodes we travelled across
            var distances = new Dictionary<Vertex<MapVertexProperty>, float>();

            // remember which nodes are the ones with the shortest paths between eachother
            var previousNodes =
                new Dictionary<Vertex<MapVertexProperty>, Vertex<MapVertexProperty>>();

            // Using a PriorityQueue<> to keep track of unvisited nodes. The first item is the Node. The second item is the Priority (which will be its distance). The queue automatically sorts itself so the smallest distance is always pulled out first.
            var unvisitedQueue = new PriorityQueue<Vertex<MapVertexProperty>, float>();

            // We set the distance of every node in the graph to Infinity because we dont know how far away they are until we have visited them, except the start node which is 0 because we are already on it.
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
                // STEP A: Dequeue the node with the lowest distance. Let's call it 'currentNode'.
                // Syntax hint: var currentNode = unvisitedQueue.Dequeue();

                // STEP B: Early Exit Check.
                // If currentNode == targetNode, you found the destination! 'break' out of the while loop.

                // STEP C: Loop through all the edges connected to the currentNode.
                // (You will need to loop through MapGraph._edges and find the ones where Source or Target is the currentNode)

                // STEP D: Inside the edge loop, find the 'neighbor' node on the other side of the edge.
                // Calculate the tentative distance: distances[currentNode] + edge.Property.Distance.

                // STEP E: If this tentative distance is LESS than the neighbor's current recorded distance in distances[neighbor]:
                //   1. Update distances[neighbor] with the new smaller tentative distance.
                //   2. Update previousNodes[neighbor] to be the currentNode.
                //   3. Enqueue the neighbor into the priority queue with its new tentative distance.
            }

            // 5. RECONSTRUCT THE PATH
            // Once the loop breaks, you create a new List<Vertex>.
            // Start at targetNode, and use the previousNodes dictionary to walk backward to startNode, adding each to the list.
            // Finally, Reverse() the list so it goes from Start -> End, and return it.

            return new List<Vertex<MapVertexProperty>>(); // Replace this with your reconstructed path
        }
    };
}
