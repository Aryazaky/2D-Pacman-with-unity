using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score = 0;
    public int lives = 3;
    int cooldown = 120;

    private void FixedUpdate()
    {
        if (cooldown > 0)
        {
            cooldown--;
        }
    }

    public void ReduceLives()
    {
        if (cooldown != 0)
        {
            return;
        }
        lives--;
        cooldown = 120;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().RespawnPlayer();
        if (lives == 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().PrintGameOver();
        }
    }
    public void AddScore()
    {
        score++;
        if (score % 50 == 0)
        {
            score++;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SetGhostScared();
        }
    }
}
