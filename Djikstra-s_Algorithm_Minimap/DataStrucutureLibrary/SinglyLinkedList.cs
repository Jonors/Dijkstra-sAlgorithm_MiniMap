using System.IO.Compression;

namespace Djikstra_s_Algorithm_Minimap.SinglyLinkedList;

public class Node
{
    // fields
    // data
    public int Data;
    // referene
    public Node? Next;

    // constructor
    public Node(int d)
    {
        Data = d;
        Next = null;
    }
}

public class SinglyLinkedList
{
    // fields
    private Node? _head = null;

    // methods
    // add elements
    public void InsertFront(int newData)
    {
        Node newNode = new Node(newData);

        newNode.Next = _head;
        _head = newNode;
    }

    public void InsertLast(int newData)
    {
        Node newNode = new Node(newData);

        if (_head == null)
        {
            _head = newNode;
        }
        else
        {
            Node? lastNode = GetLastNode();
            lastNode!.Next = newNode;
        }
    }

    public Node? GetLastNode()
    {
        Node? temp = _head;

        if(temp != null)
        {
            while(temp.Next != null)
            {
                temp = temp.Next;
            }
        }

        return temp;
    }

    public void InsertAfter(Node? prev, int newData)
    {

    }

    // delete elements
    public void DeleteNodebyKey(int key)
    {

    }

    // visit elements
    public void PrintList()
    {
        Node? temp = _head;
        Console.Write("The singlyLinkedList: ");

        while (temp != null)
        {
            Console.Write(temp.Data + " ");
            temp = temp.Next;
        }

        Console.WriteLine("");
    }
}
