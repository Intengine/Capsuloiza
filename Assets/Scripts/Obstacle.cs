using UnityEngine;

public class Obstacle
{
    public Vector2[] Corners { get; private set; }

    public Obstacle(Vector2[] corners)
    {
        Corners = corners;
    }
}