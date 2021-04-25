using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetest : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Rigidbody Rigidbody;
    private bool up=true;

    private void Awake()
    {
        startPosition = transform.position;
        targetPosition = startPosition + 0.5f * Vector3.up;
        Rigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (up) Rigidbody.velocity = 2 * Vector3.up;
        else Rigidbody.velocity = -2 * Vector3.up;
        if (transform.position.y > targetPosition.y) up = false;
        if (transform.position.y < startPosition.y)
        {
            Rigidbody.velocity = Vector3.zero;
               up = true;
        }
    }
}
