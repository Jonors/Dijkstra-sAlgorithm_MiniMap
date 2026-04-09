# Dijkstra-s_Algorithm_MiniMap
## About The Project
This Application was built as the Final Project for my C# class at USTP. Each student was assigned an algorithm which they had to implement into an application of their choice. Because the 4th semester revolves around building games in Unity, I decided to create a Map/Minimap similar to the one in GTA V. Allowing users to click on one place and marking it as their starting point and clicking on a second one to show the shortes path between them. 

## Built with
* C# 
* .NET
* [Raylib-cs](https://github.com/ChrisDill/Raylib-cs) (for 2D rendering)

## Getting Started 

### Prerequisites
To run this project .NET SDK has to be installed on the machine. It can be download from the official Microsoft website:
* [.NET SDK Download](https://dotnet.microsoft.com/download)
### Installation
1. Clone the repository to your local machine:
   ```Bash
   git clone [https://github.com/your_username_/Project-Name.git](https://github.com/your_username_/Project-Name.git)
   ```

2. Navigate into the project directory:
```Bash
cd Dijkstra-s_Algorithm_MiniMap
```

3. Install the required Raylib-cs package:
```Bash
dotnet add package Raylib-cs
```

4. Run the application:
```Bash
dotnet run
```

### Usage
When the application launches, a 2D map window will open displaying towns (blue) and crossings (dark gray) connected by roads.

Click 1: Selects your Starting Point (Highlight: Green).

Click 2: Selects your Destination (Highlight: Red). The algorithm will instantly draw a thick green line showing the fastest route.

Click 3: Resets the map and sets a new Starting Point.

Places and crossings can be added in the Program.cs via the setupMap(). Allowing to build a map for game or real place for example.

## Roadmap 
* Currently Implemented:
* Custom Graph Data Structure (Generics, Inheritance, Polymorphism)
* Dijkstra's Algorithm implementation
* Interactive 2D graphical interface using Raylib


## Contributing 
If there are any features for which you may see fit feel free to fork this project. 
Any contributions you make are greatly appreciated.

## License 
None

## Contact

jonazeichen@icloud.com
cc241032@ustp-students.at

## Acknowledgments
* Dijkstra's Algorithm - Computerphile https://youtu.be/GazC3A4OQTE?si=osmIP8jp8wqYkn2b
* How Dijkstra's Algorithm Works https://youtu.be/EFg3u_E6eHU?si=vVSGYnr_ZnOMqpyn
* Introduction to A* algorithm and pathfinding https://www.redblobgames.com/pathfinding/a-star/introduction.html
* Wikipedia https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
* Microsoft Docs https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.linkedlist-1?view=net-10.0
* Microsoft Docs https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics
* Microsoft Docs https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.priorityqueue-2?view=net-10.0
* RayLib Documentation and Git https://github.com/ChrisDill/Raylib-cs