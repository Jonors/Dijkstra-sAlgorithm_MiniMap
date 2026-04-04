using System;
using Djikstra_s_Algorithm_Minimap.Graph;

namespace MapLibrary.Core
{
    // Defining what a node (place or crossing) holds using the BasicerexProperty
    public class MapVertexProperty : BasicVertexProperty
    {
        //adding x and y so we have a position for the MapVertex
        public float X { get; set; }
        public float Y { get; set; }

        //add a NodeType so we can lateron declare if the MapVeretx is either a clickable place or a road crossing
        public string NodeType { get; set; }

        //Depending on the type the Nodes will be marked with a house or nothing
        public string IconPath { get; set; }

        public MapVertexProperty() { }

        public void Setup(float x, float y, string nodeType, string iconPath = "")
        {
            X = x;
            Y = y;
            NodeType = nodeType;
            // If it is a place, it has an icon displaying a house
            IconPath =
                nodeType == "Place" && string.IsNullOrEmpty(iconPath)
                    ? "../assets/house.png"
                    : iconPath;
        }
    }

    // Defining what an edge (road) holds, inhertign from the basic EdgeProperty
    public class MapEdgeProperty : BasicEdgeProperty<Vertex<MapVertexProperty>>
    {
        public float Distance { get; set; }

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
