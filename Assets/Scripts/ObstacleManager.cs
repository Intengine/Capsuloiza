using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public List<Obstacle> Obstacles { get; private set; } = new List<Obstacle>();

    void Start()
    {
        // Znajdź wszystkie obiekty typu Cube w scenie
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject cube in cubes)
        {
            // Przykładowo używamy 4 wierzchołków na podstawie pozycji i rozmiaru sześcianu
            Vector2[] corners = GetCubeCorners(cube);
            Obstacles.Add(new Obstacle(corners));
        }
    }

    Vector2[] GetCubeCorners(GameObject cube)
    {
        Vector3 center = cube.transform.position;
        Vector3 size = cube.transform.localScale;
        Vector2[] corners = new Vector2[4];

        corners[0] = new Vector2(center.x - size.x / 2, center.z - size.z / 2);
        corners[1] = new Vector2(center.x + size.x / 2, center.z - size.z / 2);
        corners[2] = new Vector2(center.x + size.x / 2, center.z + size.z / 2);
        corners[3] = new Vector2(center.x - size.x / 2, center.z + size.z / 2);

        return corners;
    }
}