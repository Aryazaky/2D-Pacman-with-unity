using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behavior : MonoBehaviour
{
    public GameObject ghost;
    public float speed = 0.2f;

    public abstract void Move();

    public abstract void SetTarget();
}
