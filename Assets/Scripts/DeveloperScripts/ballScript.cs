using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : GameEntityBase
{
    private Rigidbody Rigidbody;
    private bool falling;
    private float timer;
    private float tempSpeed;
    public bool scored;
    public GameDataManager.team scoredTeam;
    private new void Awake()
    {
        base.Awake();
        scored = false;
        Rigidbody = GetComponent<Rigidbody>();
        if (Rigidbody == null) Debug.LogError("ball can't find rigidbody");
        falling = false;
        tempSpeed = -1;
    }
    public override void EntityDispose()
    {
    }
    void Update()
    {
        if (!scored)
        {
            if (falling && Rigidbody.velocity.magnitude > 4.6f)
                Rigidbody.velocity = Rigidbody.velocity.normalized * 4.6f;
            else if (Rigidbody.velocity.magnitude > 4.6f)
            {
                if (tempSpeed == -1 || Rigidbody.velocity.magnitude > tempSpeed)
                {
                    tempSpeed = Rigidbody.velocity.magnitude;
                    Debug.Log(tempSpeed);
                }
                timer += Time.deltaTime;
                Rigidbody.velocity = Rigidbody.velocity.normalized / (0.22f * timer + 1) * tempSpeed;
                Debug.Log(Rigidbody.velocity.magnitude);
                if (Rigidbody.velocity.magnitude < 4.6f)
                {
                    tempSpeed = -1;
                    falling = true;
                }
            }
            if (Rigidbody.velocity.magnitude>0.1f)transform.rotation = Quaternion.LookRotation(Rigidbody.velocity.normalized);
        }
        else
        {
            tempSpeed = -1;
            falling = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name != "floor" &&
            collision.collider.name != "wall" &&
            collision.collider.name != "net" &&
            collision.collider.name != "player"
            )
        {
            falling = false;
            timer = 0f;
        }
        else
        {
            scored = true;
            if (transform.position.z > 0) scoredTeam = GameDataManager.team.red;
            else scoredTeam = GameDataManager.team.blue;
        }
    }
}
