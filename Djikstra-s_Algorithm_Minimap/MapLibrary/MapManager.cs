using System;
using System.Collections.Generic;
using Djikstra_s_Algorithm_Minimap.Graph;

namespace MapLibrary.Core
{
    //this manager acts as a middle man between the DataStrcuture Library and the UI, so that things dont get mixed up.
    public class MapManager
    {
        //creating a Graph called MapGraph which will be the map the vertecies will be placed on and used. Defining it to have Specefic MapVertexs and MapEdges which are inheriting from BasicVertex and Edge Properties
        public Graph<MapVertexProperty, MapEdgeProperty> MapGraph { get; private set; }

        //constructor
        public MapManager()
        {
            MapGraph = new Graph<MapVertexProperty, MapEdgeProperty>();
        }

        // Wrapper to make adding a place or crossing to the graph easier and more efficient
        public Vertex<MapVertexProperty> CreateMapNode(string name, float x, float y, string type)
        {
            // Tell the DataStructureLibrary/Graph to create a new blank Vertex with a name
            var vertex = MapGraph.AddVertex(name);

            // Inject custom map data into the properties of the new vertex
            vertex.Property.Setup(x, y, type);

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
                return edge;
            }
            return null;
        }

        // Distance calculation using Dijkstra's algorithm
        public List<Vertex<MapVertexProperty>> FindShortestPath(string startName, string endName)
        {
            var startNode = MapGraph.HasVertex(startName);
            var endNode = MapGraph.HasVertex(endName);

            if (startNode == null || endNode == null)
                return new List<Vertex<MapVertexProperty>>();

            // TODO: DIJKSTRA'S ALGORITHM

            return new List<Vertex<MapVertexProperty>>();
        }
    }
}
