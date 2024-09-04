using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public List<Vector2> Nodes { get; private set; }
    public List<Edge> Edges { get; private set; }
    private List<Obstacle> obstacles;

    public Graph(List<Obstacle> obstacles)
    {
        this.obstacles = obstacles;
        Nodes = new List<Vector2>();
        Edges = new List<Edge>();
        GenerateGraph();
    }

    private void GenerateGraph()
    {
        // Generowanie węzłów w przestrzeni
        // Przykład: węzły w regularnej siatce
        float step = 1.0f; // Odległość między węzłami
        for (float x = 0; x < 10; x += step)
        {
            for (float y = 0; y < 10; y += step)
            {
                Vector2 node = new Vector2(x, y);
                if (!IsNodeInsideObstacle(node))
                {
                    Nodes.Add(node);
                }
            }
        }

        // Generowanie krawędzi
        foreach (var node in Nodes)
        {
            foreach (var otherNode in Nodes)
            {
                if (node != otherNode && !IsObstacleBetween(node, otherNode))
                {
                    Edges.Add(new Edge(node, otherNode));
                }
            }
        }
    }

    private bool IsNodeInsideObstacle(Vector2 node)
    {
        foreach (var obstacle in obstacles)
        {
            if (IsPointInsidePolygon(node, obstacle.Corners))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsObstacleBetween(Vector2 start, Vector2 end)
    {
        foreach (var obstacle in obstacles)
        {
            if (IsLineIntersectingPolygon(start, end, obstacle.Corners))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsPointInsidePolygon(Vector2 point, Vector2[] polygon)
    {
        // Algorytm wnętrza wielokąta
        int n = polygon.Length;
        bool inside = false;

        for (int i = 0, j = n - 1; i < n; j = i++)
        {
            if (((polygon[i].y > point.y) != (polygon[j].y > point.y)) &&
                (point.x < (polygon[j].x - polygon[i].x) * (point.y - polygon[i].y) / (polygon[j].y - polygon[i].y) + polygon[i].x))
            {
                inside = !inside;
            }
        }

        return inside;
    }

    private bool IsLineIntersectingPolygon(Vector2 p1, Vector2 p2, Vector2[] polygon)
    {
        for (int i = 0; i < polygon.Length; i++)
        {
            Vector2 p3 = polygon[i];
            Vector2 p4 = polygon[(i + 1) % polygon.Length];
            if (LinesIntersect(p1, p2, p3, p4))
            {
                return true;
            }
        }
        return false;
    }

    private bool LinesIntersect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        float d = (p1.x - p2.x) * (p3.y - p4.y) - (p1.y - p2.y) * (p3.x - p4.x);
        if (d == 0)
            return false;

        float ua = ((p3.x - p4.x) * (p1.y - p3.y) - (p3.y - p4.y) * (p1.x - p3.x)) / d;
        float ub = ((p2.x - p1.x) * (p1.y - p3.y) - (p2.y - p1.y) * (p1.x - p3.x)) / d;

        return (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1);
    }
}

public class Edge
{
    public Vector2 Start { get; private set; }
    public Vector2 End { get; private set; }

    public Edge(Vector2 start, Vector2 end)
    {
        Start = start;
        End = end;
    }
}