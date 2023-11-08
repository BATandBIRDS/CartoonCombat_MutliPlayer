using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TurtleMover : NetworkBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] List<Transform> waypoints;

    int wayPointIndex = 0;
    void Start()
    {
        transform.position = waypoints[wayPointIndex].position;
    }

    void FixedUpdate()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (waypoints != null)
        {
            if (wayPointIndex < waypoints.Count)
            {
                Vector3 targetPos = waypoints[wayPointIndex].position;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);
                transform.LookAt(targetPos);

                if (transform.position == targetPos)
                {
                    wayPointIndex++;
                }
            }
            else
            {
                wayPointIndex = 0;
            }
        }
    }
}
