namespace Djikstra_s_Algorithm_Minimap.Graph;

public abstract class BasicVertexProperty
{
    // Fields
    public uint Id;
    public string Name = "UnknownName";
}


public class Vertex<TVertexProperty> where TVertexProperty : BasicVertexProperty, new()
{
    public TVertexProperty Property;
    private static uint _idCounter = 0;

    // Constructors
    public Vertex(string name)
    {
        Property = new TVertexProperty();
        Property.Id = _idCounter++;
        Property.Name = name;
    }

    public override string ToString()
    {
        return $"V({Property.Id}) = {Property.Name}";
    }
}