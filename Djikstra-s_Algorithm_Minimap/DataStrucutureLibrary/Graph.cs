using System;

namespace Djikstra_s_Algorithm_Minimap.Graph;

public class Graph<TVertexProperty, TEdgeProperty>
    //custom vertex TVertexProperty passed to this graph class must inherit BasicVertexProperty from Vertex.cs and have an empty constructor in case a new one needs to be created
    where TVertexProperty : BasicVertexProperty, new()
    //custom edge TEdgeProperty must inherit BasicEdgeProperty which links two custom vertecies as Source and Target (defined in Edge.cs), so the edge knows where it starts and ends,  and have an empty constructor in case a new one needs to be created
    where TEdgeProperty : BasicEdgeProperty<Vertex<TVertexProperty>>, new()
{
    // Fields
    // In a field declaration, readonly means you can only assign the field during the declaration or in a constructor in the same class.
    private readonly LinkedList<Vertex<TVertexProperty>> _vertices;
    private readonly LinkedList<Edge<Vertex<TVertexProperty>, TEdgeProperty>> _edges;

    // Constructors
    public Graph()
    {
        // create new a new LinkedList for each of the 2 newly created variables where one is a list that holds all the vertecies/places we are about to create in the MapManager and one holds all the edge data inbetween those vertecies/places
        _vertices = new LinkedList<Vertex<TVertexProperty>>();
        _edges = new LinkedList<Edge<Vertex<TVertexProperty>, TEdgeProperty>>();
    }

    // Methods(CRUD)
    // Vertex
    public Vertex<TVertexProperty> AddVertex(string name)
    {
        // check if the vertex exist so we dont create duplicates
        Vertex<TVertexProperty>? v = HasVertex(name);

        if (v == null)
        {
            Vertex<TVertexProperty> newV = new Vertex<TVertexProperty>(name);
            _vertices.AddLast(newV);

            return newV;
        }

        return v;
    }

    public void RemoveVertex(string name)
    {
        // Check if the vertex exsits
        Vertex<TVertexProperty>? v = HasVertex(name);

        // If it exist,
        if (v != null)
        {
            for (int i = 0; i < _edges.Count; i++)
            {
                //also remove the edges between the removed vertex and connected ones to have no loose ends.
                Edge<Vertex<TVertexProperty>, TEdgeProperty> e = _edges.ElementAt(i);

                if (e.Property.Source == v || e.Property.Target == v)
                {
                    _edges.Remove(e);
                    i--;
                }
            }

            // remove the vertex
            _vertices.Remove(v);
        }
    }

//check if Vertex exists
    public Vertex<TVertexProperty>? HasVertex(string name)
    {
        foreach (Vertex<TVertexProperty> v in _vertices)
        {
            if (v.Property.Name == name)
                return v;
        }

        return null;
    }

    // Edge
    public Edge<Vertex<TVertexProperty>, TEdgeProperty>? AddEdge(
        Vertex<TVertexProperty>? source,
        Vertex<TVertexProperty>? target
    )
    {
        // Check if the source and target vertices exist
        if (source == null || target == null)
        {
            Console.WriteLine(
                "Source or Target Vertex<TVertexProperty>could not be found. Please add vertices first"
            );
            return null;
        }

        // Check if the edge between them exists
        Edge<Vertex<TVertexProperty>, TEdgeProperty>? e = HasEdge(source, target);

        // If not, add a new edge between the vertecies
        if (e == null)
        {
            Edge<Vertex<TVertexProperty>, TEdgeProperty> newE = new Edge<
                Vertex<TVertexProperty>,
                TEdgeProperty
            >(source, target);
            _edges.AddLast(newE);
            return newE;
        }

        return e;
    }

    public void RemoveEdge(Vertex<TVertexProperty>? source, Vertex<TVertexProperty>? target)
    {
        // Check if the edge exists
        Edge<Vertex<TVertexProperty>, TEdgeProperty>? e = HasEdge(source, target);

        // If yes, remove it
        if (e != null)
        {
            _edges.Remove(e);
        }
        else
        {
            Console.WriteLine("Edge<Vertex<TVertexProperty>, TEdgeProperty> could not be found");
        }
    }

//check if edge exists
    public Edge<Vertex<TVertexProperty>, TEdgeProperty>? HasEdge(
        Vertex<TVertexProperty>? source,
        Vertex<TVertexProperty>? target
    )
    {
        if (source == null || target == null)
            return null;

        foreach (Edge<Vertex<TVertexProperty>, TEdgeProperty> e in _edges)
        {
            if ((e.Property.Source == source) && (e.Property.Target == target))
                return e;
        }

        return null;
    }

    // Graph
    public void PrintGraph()
    {
        Console.WriteLine("Total Vertex Number: " + _vertices.Count);
        Console.WriteLine("Total Edge Number: " + _edges.Count);
        Console.WriteLine("======================");

        // vertex list
        foreach (Vertex<TVertexProperty> v in _vertices)
        {
            Console.WriteLine(v);
            // Console.WriteLine($"V({v.Id}) = {v.Name}");
        }
        Console.WriteLine("======================");

        // edge list
        foreach (Edge<Vertex<TVertexProperty>, TEdgeProperty> e in _edges)
        {
            Console.WriteLine(e);
            // Console.WriteLine($"E({e.Id}) = V({e.Source.Name}) -- V({e.Target.Name})");
        }
        Console.WriteLine("======================");
    }
}
