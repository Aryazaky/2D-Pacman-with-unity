using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public Behavior scatterBehavior;
    public Behavior chaseBehavior;
    public Behavior scaredBehavior;

    public enum GhostState
    {
        Scatter,
        Chase,
        Scared,
        Eaten
    }
    public GhostState ghostState = GhostState.Scatter;

    int chaseCooldown;
    int chaseCooldownMax = 3600;
    int chaseDuration;
    int chaseDurationMax = 3600;
    int scaredDuration;
    int scaredDurationMax = 600;

    private void FixedUpdate()
    {
        switch (ghostState)
        {
            case (GhostState.Scatter):
                scatterBehavior.Move();
                break;
            case (GhostState.Chase):
                chaseBehavior.Move();
                break;
            case (GhostState.Scared):
                scaredBehavior.Move();
                break;
            case (GhostState.Eaten):
                Vector2 spawnPos = GameObject.FindGameObjectWithTag("Obstacle").GetComponent<Maze>().ghostSpawnPos.position;
                Vector2 p = Vector2.MoveTowards(transform.position,
                    spawnPos,
                    0.4f);
                GetComponent<Rigidbody2D>().MovePosition(p);
                if (Vector2.Distance(transform.position, spawnPos) < 1)
                {
                    ghostState = GhostState.Scatter;
                }
                break;
            default:
                break;
        }

        if (chaseCooldown > 0 && ghostState == GhostState.Scatter)
        {
            chaseCooldown--;
        }
        else
        {
            chaseCooldown = chaseCooldownMax;
            chaseDuration = chaseDurationMax;
            ghostState = GhostState.Chase;
        }
        if (chaseDuration > 0 && ghostState == GhostState.Chase)
        {
            chaseDuration--;
        }
        else
        {
            ghostState = GhostState.Scatter;
        }
        if (scaredDuration > 0 && ghostState == GhostState.Scared)
        {
            scaredDuration--;
        }
        else
        {
            chaseCooldown = chaseCooldownMax;
            chaseDuration = chaseDurationMax;
            ghostState = GhostState.Scatter;
        }
    }

    public void SetScared()
    {
        ghostState = GhostState.Scared;
        scaredDuration = scaredDurationMax;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ghostState == GhostState.Scared)
            {
                ghostState = GhostState.Eaten;
            }
            else collision.gameObject.GetComponent<Score>().ReduceLives();
        }
    }
}
