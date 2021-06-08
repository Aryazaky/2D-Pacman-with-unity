using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBehavior : Behavior
{
    public Transform[] waypoints;
    int cur = 0;

    override public void Move()
    {
        // Waypoint not reached yet? then move closer
        if (ghost.transform.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(ghost.transform.position,
                                            waypoints[cur].position,
                                            speed);
            ghost.GetComponent<Rigidbody2D>().MovePosition(p);
        }
        else SetTarget();

        // Animation
        Vector2 dir = waypoints[cur].position - ghost.transform.position;

        ghost.GetComponent<Animator>().SetFloat("DirX", dir.x);
        ghost.GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    public override void SetTarget()
    {
        // Waypoint reached, select next one
        cur = (cur + 1) % waypoints.Length;
    }
}