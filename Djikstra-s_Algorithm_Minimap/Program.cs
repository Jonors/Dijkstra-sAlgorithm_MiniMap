using System;
using System.Collections.Generic;
using System.Numerics; // Required for Raylib Vectors
using Djikstra_s_Algorithm_Minimap.Graph;
using MapLibrary.Core;
using Raylib_cs;

namespace Djikstra_s_Algorithm_Minimap
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate the MapManager
            MapManager mapManager = new MapManager();

            //setting up the map with access to the mapManager
            SetupMap(mapManager);

            // Using Raylib to create a window for the map to visualise the paths between the places
            Raylib.InitWindow(1200, 800, "Djikstra Minimap");
            Raylib.SetTargetFPS(60);

            // Variables to track user clicks
            Vertex<MapVertexProperty> selectedStart = null;
            Vertex<MapVertexProperty> selectedEnd = null;
            List<Vertex<MapVertexProperty>> currentPath = new List<Vertex<MapVertexProperty>>();

            //loop that displays the map and updates data on it
            while (!Raylib.WindowShouldClose())
            {
                //clicking logic
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    // check mouse position
                    Vector2 mousePos = Raylib.GetMousePosition();
                    Vertex<MapVertexProperty> clickedNode = null;

                    // check if a node is close to the mouse position
                    foreach (var node in mapManager.AllNodes)
                    {
                        // pythagorean theorem to find distance between mouse and node
                        float dx = mousePos.X - node.Property.X;
                        float dy = mousePos.Y - node.Property.Y;
                        float distanceToMouse = (float)Math.Sqrt(dx * dx + dy * dy);

                        // if the mouse is within 20 pixels of the node's center mark it as clicked
                        if (distanceToMouse < 20)
                        {
                            clickedNode = node;
                            break;
                        }
                    }

                    // if a node is clicked the selection needs to be ugraded. Depending on what click is made the selction updates differently so we have a simple click navigation.
                    if (clickedNode != null)
                    {
                        if (selectedStart == null)
                        {
                            // If no start point is selected we determin the first clicked node as the starting point
                            selectedStart = clickedNode;
                        }
                        else if (selectedEnd == null && clickedNode != selectedStart)
                        {
                            // if there is a starting point selected and a second node is clicked that is not the same as the starting point then deteremin it as the end point and claculate the shortest path
                            selectedEnd = clickedNode;
                            currentPath = mapManager.FindShortestPath(
                                selectedStart.Property.Name,
                                selectedEnd.Property.Name
                            );
                        }
                        else
                        {
                            //if both poiont are selected and a click is made on a new node we determine that as the new start point, remove the end point and clear the drawn path
                            selectedStart = clickedNode;
                            selectedEnd = null;
                            currentPath.Clear();
                        }
                    }
                }

                //drawing logic
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RayWhite);

                // drawing the default edges/roads first so they sit behind everything else so the later shortest path lies above them
                foreach (var edge in mapManager.AllEdges)
                {
                    Raylib.DrawLine(
                        (int)edge.Property.Source.Property.X,
                        (int)edge.Property.Source.Property.Y,
                        (int)edge.Property.Target.Property.X,
                        (int)edge.Property.Target.Property.Y,
                        Color.LightGray
                    );
                }

                // drawing the path Djikstras Algorihtm found over the esxisting edges
                if (currentPath.Count > 1)
                {
                    // Loop through the path list and stop at Count - 1 because we draw a from the current index to the next one. If we donw stop early the prgram will try to connect to a non exisitng node and crash
                    for (int i = 0; i < currentPath.Count - 1; i++)
                    {
                        Vector2 p1 = new Vector2(
                            currentPath[i].Property.X,
                            currentPath[i].Property.Y
                        );
                        Vector2 p2 = new Vector2(
                            currentPath[i + 1].Property.X,
                            currentPath[i + 1].Property.Y
                        );

                        // DrawLineEx makes the line thicker
                        Raylib.DrawLineEx(p1, p2, 5.0f, Color.Green);
                    }
                }

                // Draw the nodes (Places and Crossings)  on top of everything so it looks nice and clean
                foreach (var node in mapManager.AllNodes)
                {
                    // Default styling based on inheritance Type
                    Color nodeColor =
                        node.Property.NodeType == "Place" ? Color.Blue : Color.DarkGray;
                    int radius = node.Property.NodeType == "Place" ? 15 : 8;

                    // Override colors if they are currently selected by the user
                    if (node == selectedStart)
                        nodeColor = Color.Green;
                    if (node == selectedEnd)
                        nodeColor = Color.Red;

                    // Draw the physical circle
                    Raylib.DrawCircle(
                        (int)node.Property.X,
                        (int)node.Property.Y,
                        radius,
                        nodeColor
                    );

                    // Draw the name text hovering slightly above the node
                    Raylib.DrawText(
                        node.Property.Name,
                        (int)node.Property.X - 15,
                        (int)node.Property.Y - 30,
                        20,
                        Color.Black
                    );
                }

                // Draw Instructions at the top left
                Raylib.DrawText(
                    "Click a node to set Start (Green). Click another to set End (Red).",
                    10,
                    10,
                    20,
                    Color.DarkGray
                );

                Raylib.EndDrawing();
            }

            // Cleanup when window closes
            Raylib.CloseWindow();
        }

        static void SetupMap(MapManager manager)
        {
            // Giving them specific X/Y coordinates so Pythagorean math has numbers to go off
            // Places
            manager.CreateMapNode("Home", 100, 100, "Place");
            manager.CreateMapNode("Tavern", 250, 530, "Place");
            manager.CreateMapNode("Market", 500, 300, "Place");
            manager.CreateMapNode("Blacksmith", 800, 650, "Place");
            manager.CreateMapNode("Castle", 1000, 500, "Place");
            // Crossings
            manager.CreateMapNode("Crossing_A", 300, 200, "Crossing");
            manager.CreateMapNode("Crossing_B", 500, 500, "Crossing");
            manager.CreateMapNode("Crossing_C", 800, 200, "Crossing");

            Console.WriteLine("\nconnecting nodes ...");

            ConnectAndPrint(manager, "Home", "Crossing_A");
            ConnectAndPrint(manager, "Crossing_A", "Tavern");
            ConnectAndPrint(manager, "Crossing_A", "Market");

            ConnectAndPrint(manager, "Tavern", "Crossing_B");
            ConnectAndPrint(manager, "Market", "Crossing_B");

            ConnectAndPrint(manager, "Market", "Crossing_C");
            ConnectAndPrint(manager, "Crossing_C", "Blacksmith");
            ConnectAndPrint(manager, "Crossing_B", "Blacksmith");

            ConnectAndPrint(manager, "Blacksmith", "Castle");

            Console.WriteLine("\nMap generation complete.");
        }

        // A helper methods to connect nodes, check their distaces and format the console output
        static void ConnectAndPrint(MapManager manager, string source, string target)
        {
            var edge = manager.ConnectNodes(source, target);

            if (edge != null && edge.Property.Source != null && edge.Property.Target != null)
            {
                string sourceName = edge.Property.Source.Property.Name;
                string targetName = edge.Property.Target.Property.Name;
                float distance = edge.Property.Distance;
                Console.WriteLine(
                    $"'{sourceName}' is connected to '{targetName}' with a distance of {distance:F2}"
                );
            }
            else
            {
                Console.WriteLine($"Failed to connect '{source}' and '{target}'.");
            }
        }
    }
}
