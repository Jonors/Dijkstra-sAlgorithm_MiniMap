namespace Djikstra_s_Algorithm_Minimap
{
    //create an Edge which will allows the algorithm to check for distances between nodes
    public class Edge
    {
        //an edge cant exist without a start or endpoint
        public GraphNode StartNode { get; }
        public GrahpNode EndNode { get; }
        public float Distance { get; }

        //constructor
        public Edge(GraphNode start, GraphNode end, float distance)
        {
            StartNode = start;
            EndNode = end;
            Distance = distance;
        }

        //Helper function to get the other side of the ede
        public GraphNode GetOppositeNode(GraphNode fromNode)
        {
            if (fromNode == StartNode)
                return EndNode;
            if (fromNode == EndNode)
                return StartNode;
            return null;
        }
    }
}
