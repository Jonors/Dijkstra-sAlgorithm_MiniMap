namespace Djikstra_s_Algorithm_Minimap.Graph;

public abstract class BasicEdgeProperty<TVertex>
{
    // Fields
    public uint Id;
    // references
    public TVertex? Source;
    public TVertex? Target;
}

public class Edge<TVertex, TEdgeProperty>
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