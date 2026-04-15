
# Report

Course: C# Development SS2026 (4 ECTS, 3 SWS)

Student ID: CC241032

BCC Group: C

Name: Jona Jožef Zeichen

## Methodology: 
To solve the shortest path problem without changing the provided DataStructureLibrary, I created a MapManager and a MapProperties class to act as secure wrappers. The manager handles all interactions between the UI and the underlying Graph, ensuring data safety by storing safe references to the vertices and edges (AllNodes, AllEdges) as they are created. The MapProperties takes the BasicProperties of Vertecies and Edges and allows the MapManager to acces and use them as needed.

To implement Dijkstra's Algorithm, the logic was contained within a single FindShortestPath method to prevent passing heavy data structures between multiple functions. The algorithm utilizes 2 Dictionaries. The first Dictionary<Vertex, float> tracks the absolute shortest known distance from the starting node to every other node. And the second Dictionary<Vertex, Vertex> acts as a trail, recording the previous node used to reach a destination optimally. A PriorityQueue<Vertex, float> continually sorts unvisited nodes so that the algorithm always explores the node with the lowest current distance score first. As the algorithm evaluates neighbors, it calculates a tentative distance (current distance + edge distance). If this new distance is faster than the previously recorded distance, it overwrites the shortest known path (Dictionary<Vertex, float>), updates the trail(Dictionary<Vertex, Vertex>), and pushes the neighbor back into the queue to ripple the new shortcut across the map.

Reconstruction: Once the target is reached, a while loop traces the breadcrumb dictionary backward from the destination to the start, reverses the list, and returns the final path.

## Additional Features
While the core requirement was simply an application demonstrating the algorithm, I implemented a fully interactive 2D Graphical User Interface using Raylib-cs. Instead of a static console application, the project functions like a functioning video game minimap. Using a custom spatial check using the Pythagorean theorem (dx*dx + dy*dy) to detect if the user's mouse clicks within the radius of a drawn node, the UI dynamically tracks the user's clicks, allowing them to intuitively set a Start node (highlighted Green), set an End node (highlighted Red), and instantly visualize the generated shortest path (drawn as a thick green line overlapping the standard roads) without relying on text inputs.

## Discussion/Conclusion
The development process presented one distinct challenges. The struggle of how to access the nodes for the UI without altering the mandatory base Graph.cs file (which kept its lists private). Creating parallel lists risked data desynchronization so I solved this by intercepting the nodes exactly at the point of creation inside the MapManager wrapper and saving safe references via properties with private set accessors.

Reagarding the visualiyation the plan was to build the UI using Windows Forms (WinForms). However, because I develop 50/50 on a macOS and Windows environment using Visual Studio Code, WinForms was incompatible for one of them. I solved this by pivoting to Raylib-cs, a cross-platform graphics library. 

But all in all I learned a lot about algorithms and how to tackle them. Also about how classes and dictionaries, and what possibilities they offer about their stored data.

## Work with: 
Independant

## Reference: 
* Dijkstra's Algorithm - Computerphile https://youtu.be/GazC3A4OQTE?si=osmIP8jp8wqYkn2b
* How Dijkstra's Algorithm Works https://youtu.be/EFg3u_E6eHU?si=vVSGYnr_ZnOMqpyn
* Introduction to A* algorithm and pathfinding https://www.redblobgames.com/pathfinding/a-star/introduction.html
* Wikipedia https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
* Microsoft Docs https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.linkedlist-1?view=net-10.0
* Microsoft Docs https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics
* Microsoft Docs https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.priorityqueue-2?view=net-10.0
* RayLib Documentation and Git https://github.com/ChrisDill/Raylib-cs