using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pacmanPrefab;
    public GameObject mazePrefab;
    public List<GameObject> ghostPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        mazePrefab = GameObject.FindGameObjectWithTag("Obstacle");
        pacmanPrefab = GameObject.FindGameObjectWithTag("Player");
        mazePrefab.GetComponent<Maze>().GenerateDots();
    }
    //fungsi buat observer test? Mirip observer pattern lah
    public void PrintGameOver()
    {
        Debug.Log("Game Over!");
    }
    public void RespawnPlayer()
    {
        pacmanPrefab.transform.position = mazePrefab.GetComponent<Maze>().pacmanSpawnPos.position;
    }
    public void SetGhostScared()
    {
        foreach(GameObject g in ghostPrefabs)
        {
            g.GetComponent<GhostMove>().SetScared();
        }
    }
}
