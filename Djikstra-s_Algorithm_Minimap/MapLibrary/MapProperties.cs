using System;
using Djikstra_s_Algorithm_Minimap.Graph;

namespace MapLibrary.Core
{
    // Defining what a node (place or crossing) holds using the BasicVertexProperty
    public class MapVertexProperty : BasicVertexProperty
    {
        //adding x and y so we have a position for the MapVertex
        public float X { get; set; }
        public float Y { get; set; }

        //add a NodeType so we can lateron declare if the MapVeretx is either a place or a road crossing
        public string NodeType { get; set; }

        public MapVertexProperty() { }

        //constructor for the PLace/crossing vertecies
        public void Setup(float x, float y, string nodeType)
        {
            X = x;
            Y = y;
            NodeType = nodeType;
        }
    }

    // Defining what an edge (road) holds, inhertign from the basic EdgeProperty
    public class MapEdgeProperty : BasicEdgeProperty<Vertex<MapVertexProperty>>
    {
        public float Distance { get; set; }

        //empty constructor
        public MapEdgeProperty() { }

        //using the pythagorean theorem to calculate the distance between two points, giving us the distances needed for Djikstras Algorithm
        public void CalculateDistance()
        {
            if (Source != null && Target != null)
            {
                Distance = (float)
                    Math.Sqrt(
                        Math.Pow(Target.Property.X - Source.Property.X, 2)
                            + Math.Pow(Target.Property.Y - Source.Property.Y, 2)
                    );
            }
        }
    }
}
