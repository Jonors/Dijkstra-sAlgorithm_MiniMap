using System;
using Djikstra_s_Algorithm_Minimap.NodeLibrary.Graph;

namespace MapLibrary.Core
{
    public class MapVertexProperty : BasicVertexProperty
    {
        //define what a node(place or crossing) holds: position, Type, and depending on type a visual icon
        public float X { get; set; }
        public float Y { get; set; }

        public string NodeType { get; set; }

        //if it is a place, it has an icon displaying a house
        public string IconPath { get; set; }

        public MapVertexProperty() { }

        public void Setup(float x, float y, string nodeType, string iconPath = "")
        {
            X = x;
            Y = y;
            NodeType = nodeType;
            IconPath = iconPath;
        }
    }

    //defining what an edge(road) holds: distance, connected to

    public class MapEdgeProperty : BasicEdgeProperty<Vertex<MapEdgeProperty>>
    {
        public float Distance { get; set; }

        public MapEdgeProperty() { }

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
