using System;
using System.Collection.Generic;
using Djikstra_s_Algorithm_Minimap;
using Djikstra_s_Algorithm_Minimap.Graph;

namespace MapLibrary.Core
{
    public class MapManager
    {
        public Graph<MapVertexProperty, MapEdgeProperty> MapGraph { get; private set; }

        public MapManager()
        {
            MapGraph = new Graph<MapVertexProperty, MapEdgeProperty>();
        }

        //wrapper to add Place or Crossing
        public Vertex<MapVertexProperty> CreateMapNode(string name, float x, float y, string type)
        {
            //use methods from lectre to add vertex
            var vertex = MapGraph.AddVertex(name);
            //inject custom map data into new vertex
            vertex.Property.Setup(x, y, type);

            return vertex;
        }

        //Wrapper to connect two nodes and calculate the distance between the two
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
                    //calculate the distance so the Algorithm can later use it
                    edge.Property.CalculateDistance();
                }
                return edge;
            }
            return null;
        }
        //distance calculation using Djikstras algorithm
        public List<Vertex<MapVertexProperty>> FindShortestPath(string startName, string endName)
        {
            var startNode = MapGraph.HasVertex(startName);
            var endNode = MapGraph.HasVertex(endName);

            if (startNode == null || nodeEnd == null) return new List <Vertex<MapVertexProperty>>();

            //DJIKSTRAS ALGORITHM

            return new List<Vertex<MapVertexProperty>> ();
        }
    }
}
