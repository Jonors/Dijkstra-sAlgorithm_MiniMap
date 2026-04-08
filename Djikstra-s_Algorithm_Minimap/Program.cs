using System;
using MapLibrary.Core;

namespace Djikstra_s_Algorithm_Minimap
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate the MapManager
            MapManager mapManager = new MapManager();

            // Giving them specific X/Y coordinates so the Pythagorean math has real numbers to calculate
            // Places/Destinations
            mapManager.CreateMapNode("Home", 10, 10, "Place");
            mapManager.CreateMapNode("Tavern", 30, 50, "Place");
            mapManager.CreateMapNode("Market", 50, 20, "Place");
            mapManager.CreateMapNode("Blacksmith", 80, 80, "Place");
            mapManager.CreateMapNode("Castle", 100, 50, "Place");
            // Crossings
            mapManager.CreateMapNode("Crossing_A", 30, 20, "Crossing");
            mapManager.CreateMapNode("Crossing_B", 50, 50, "Crossing");
            mapManager.CreateMapNode("Crossing_C", 80, 20, "Crossing");

            Console.WriteLine("\nconnecting nodes ...");

            // We use a helper method below to keep the Main method clean
            ConnectAndPrint(mapManager, "Home", "Crossing_A");
            ConnectAndPrint(mapManager, "Crossing_A", "Tavern");
            ConnectAndPrint(mapManager, "Crossing_A", "Market");

            ConnectAndPrint(mapManager, "Tavern", "Crossing_B");
            ConnectAndPrint(mapManager, "Market", "Crossing_B");

            ConnectAndPrint(mapManager, "Market", "Crossing_C");
            ConnectAndPrint(mapManager, "Crossing_C", "Blacksmith");
            ConnectAndPrint(mapManager, "Crossing_B", "Blacksmith");

            ConnectAndPrint(mapManager, "Blacksmith", "Castle");

            Console.WriteLine("\nMap generation complete.");

            Console.WriteLine("\nLooking for shorest Path");
            NavigateAndPrint(mapManager, "Home", "Blacksmith");

            //Console.ReadLine(); // Keeps the console window open
        }

        // A helper method to connect nodes and format the console output
        static void ConnectAndPrint(MapManager manager, string source, string target)
        {
            //reuse MapManager so we have access to the created map
            var edge = manager.ConnectNodes(source, target);

            //prints each edge so we can see the connections between the places
            if (edge != null && edge.Property.Source != null && edge.Property.Target != null)
            {
                string sourceName = edge.Property.Source.Property.Name;
                string targetName = edge.Property.Target.Property.Name;

                // :F2 formats the float to exactly 2 decimal places so it looks clean when printed
                float distance = edge.Property.Distance;
                Console.WriteLine(
                    $"'{sourceName}' is connected to '{targetName}' with a distance of {distance:F2}"
                );
            }
            else
            {
                //mandatory error message in case something goes wrong
                Console.WriteLine($"Failed to connect '{source}' and '{target}'.");
            }
        }

        static void NavigateAndPrint(MapManager manager, string start, string end)
        {
            ///reuse MapManager so we have access to the created map
            var path = manager.FindShortestPath(start, end);
            string pathString = "";

            // Loop through our path output and pull out just the names
            for (int i = 0; i < path.Count; i++)
            {
                pathString += path[i].Property.Name;

                // Add an arrow between places, but not after the very last place
                if (i < path.Count - 1)
                {
                    pathString += " -> ";
                }
            }

            Console.WriteLine(
                $"\n[DIJKSTRA SUCCESS] The shortest path from '{start}' to '{end}' is: {pathString}"
            );
        }
    }
}
