using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public GameObject dotPrefab;
    //public GameObject powerupPrefab;
    public Transform pacmanSpawnPos;
    public Transform ghostSpawnPos;

    float dotOffset = 1f;
    Vector2 margin = new Vector2(1, 1);
    Vector2 size;

    private void Start()
    {
        size.x = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        size.y = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    public void GenerateDots()
    {
        if (dotPrefab != null)
        {
            Vector2 spawnerPos = margin;
            while(spawnerPos.y < size.y - margin.y)
            {
                spawnerPos.x = margin.x;
                while(spawnerPos.x < size.x - margin.x)
                {
                    spawnerPos.x += dotOffset;
                    RaycastHit2D hit = Physics2D.CircleCast(spawnerPos, 0.5f, Vector2.zero);
                    if(hit.collider == null)
                    {
                        Instantiate(dotPrefab, spawnerPos, Quaternion.identity);
                    }
                }
                spawnerPos.y += dotOffset;
            }
        }
        else
        {
            Debug.LogError("No DotPrefab attached!");
        }
    }
}
