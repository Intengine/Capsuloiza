using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    public List<Obstacle> Obstacles = new List<Obstacle>();
    private Graph graph;

    void Start()
    {
        // Przekaż przeszkody do grafu
        graph = new Graph(Obstacles);
        Debug.Log("Graph initialized with " + graph.Nodes.Count + " nodes.");
    }
}