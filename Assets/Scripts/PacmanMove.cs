using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    public float speed = 0.3f;
    Vector2 dest = Vector2.zero;
    Vector2 lastDir = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        dest = transform.position;
    }

    void FixedUpdate()
    {
        // Move closer to Destination
        if (IsValid(dest - (Vector2)transform.position))
        {
            Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }

        // Check for Input if not moving
        if ((Vector2)transform.position == dest)
        {
            if (Input.GetKey(KeyCode.UpArrow) || lastDir == Vector2.up )
                if (IsValid(Vector2.up))
                {
                    dest = (Vector2)transform.position + Vector2.up;
                    lastDir = Vector2.up;
                }
            if (Input.GetKey(KeyCode.RightArrow) || lastDir == Vector2.right)
                if (IsValid(Vector2.right))
                {
                    dest = (Vector2)transform.position + Vector2.right;
                    lastDir = Vector2.right;
                }
            if (Input.GetKey(KeyCode.DownArrow) || lastDir == -Vector2.up)
                if (IsValid(-Vector2.up))
                {
                    dest = (Vector2)transform.position - Vector2.up;
                    lastDir = -Vector2.up;
                }
            if (Input.GetKey(KeyCode.LeftArrow) || lastDir == -Vector2.right)
                if (IsValid(-Vector2.right))
                {
                    dest = (Vector2)transform.position - Vector2.right;
                    lastDir = -Vector2.right;
                }
        }

        // Animation Parameters
        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    bool IsValid(Vector2 dir)
    {
        // Cast Line from 'next to Pac-Man' to 'Pac-Man'
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }
}
