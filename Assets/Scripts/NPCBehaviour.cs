using System.Collections;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public Transform[] Waypoints;
    private int currentWaypointIndex = 0;
    public float speed = 2.0f;
    public float waitTime = 2.0f;

    void Start()
    {
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            Vector3 target = Waypoints[currentWaypointIndex].position;
            while (Vector3.Distance(transform.position, target) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);
            currentWaypointIndex = (currentWaypointIndex + 1) % Waypoints.Length;
        }
    }
}