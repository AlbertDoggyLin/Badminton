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
    }
    public override void EntityDispose()
    {
    }
    void Update()
    {
        if (!scored)
        {
            if(Rigidbody.velocity!=Vector3.zero)transform.rotation = Quaternion.LookRotation(Rigidbody.velocity.normalized);
            if (falling && Rigidbody.velocity.magnitude > 4.6f) Rigidbody.velocity = Rigidbody.velocity.normalized * 4.6f;
            else if (Rigidbody.velocity.magnitude > 4.6f)
            {
                timer += Time.deltaTime;
                Rigidbody.velocity = Rigidbody.velocity.normalized / (0.22f * timer + 1 / tempSpeed);
                if (Rigidbody.velocity.magnitude < 4.6f) falling = true;
            }
            if (transform.position.y <= 1.8 && transform.position.z >= 0.4)
            {
                Rigidbody.velocity = Rigidbody.velocity.normalized * -8;
                if (Rigidbody.velocity.z > -0.3f) Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, Rigidbody.velocity.y, -2);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.name != "floor" &&
            collision.collider.name != "wall" &&
            collision.collider.name != "net" &&
            collision.collider.name != "player"
            )
        {
            falling = false;
            tempSpeed = Rigidbody.velocity.magnitude;
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
