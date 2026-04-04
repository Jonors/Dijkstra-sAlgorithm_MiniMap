namespace Djikstra_s_Algorithm_Minimap;

public class Node
{
    //fields for postion on the map
    public int xPos;
    public int yPos;
    //abstract string to later polymorph into either Name of a place or replace with "crossing" 
    abstract string Name;
};
