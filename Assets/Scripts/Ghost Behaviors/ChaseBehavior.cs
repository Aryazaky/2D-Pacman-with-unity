using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavior : Behavior
{
    Vector2 target;
    List<Vector2> possibleDir = new List<Vector2>();
    List<Vector2> all4Dir = new List<Vector2>();
    int lowestDistanceID = -1;

    private void Start()
    {
        all4Dir.Add(Vector2.up);
        all4Dir.Add(Vector2.right);
        all4Dir.Add(Vector2.down);
        all4Dir.Add(Vector2.left);
    }

    public override void Move()
    {
        SetTarget();
        if (possibleDir.Count > 0)
        {
            Vector2 p = Vector2.MoveTowards(ghost.transform.position,
                        possibleDir[lowestDistanceID],
                        speed);
            ghost.GetComponent<Rigidbody2D>().MovePosition(p);

            // Animation
            Vector2 dir = possibleDir[lowestDistanceID] - (Vector2)ghost.transform.position;

            ghost.GetComponent<Animator>().SetFloat("DirX", dir.x);
            ghost.GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
    }

    public override void SetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (possibleDir.Count > 0) possibleDir.Clear();
        foreach(Vector2 dir in all4Dir)
        {
            if (IsValid(dir))
            {
                possibleDir.Add((Vector2)transform.position + dir);
            }
        }
        float lowestDistance = 1000;
        int curIndex = 0;
        foreach (Vector2 dir in possibleDir)
        {
            float distance = Vector2.Distance(target, dir);

            if(distance < lowestDistance)
            {
                lowestDistance = distance;
                lowestDistanceID = curIndex;
            }
            curIndex++;
        }
    }

    bool IsValid(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Linecast((Vector2)ghost.transform.position + dir, ghost.transform.position);
        return (hit.collider == ghost.GetComponent<Collider2D>());
    }
}
