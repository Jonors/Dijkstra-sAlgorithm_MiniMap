namespace Djikstra_s_Algorithm_Minimap.Graph;

//Edge class need to contain Vertexes so it know where it begins and ends, <TVertex>
public abstract class BasicEdgeProperty<TVertex>
{
    // Fields
    public uint Id;

    // references
    public TVertex? Source;
    public TVertex? Target;
}

public class Edge<TVertex, TEdgeProperty>
//A TEdgeProperty needs to inherit from BasicEdgeProperty to be the specific Vertex we need. Also it always needs to have a constructor in case we want to create a new one.
    where TEdgeProperty : BasicEdgeProperty<TVertex>, new()
{
    public TEdgeProperty Property;
    private static uint _idCounter = 0;

    // Constructors
    public Edge(TVertex source, TVertex target)
    {
        Property = new TEdgeProperty();
        Property.Id = _idCounter++;
        Property.Source = source;
        Property.Target = target;
    }

    public override string ToString()
    {
        // check how to make it generic
        return $"E({Property.Id}): ({Property.Source}) --> ({Property.Target})";
    }
}
