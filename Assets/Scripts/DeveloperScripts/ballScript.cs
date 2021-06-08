using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : GameEntityBase
{
    public GameStatus currentStatus;
    public ServingPosition currentPosition;
    public team scoredTeam;
    public team enableToHitBall;
    public bool scored;
    private Rigidbody Rigidbody;
    private Vector3 LastPosition;
    public const float c = 0.2f;
    private new void Awake()
    {
        base.Awake();
        currentPosition = ServingPosition.Right;
        currentStatus = GameStatus.Stop;
        scored = false;
        Rigidbody = GetComponent<Rigidbody>();
        if (Rigidbody == null) Debug.LogError("ball can't find rigidbody");
    }
    public override void EntityDispose()
    {
    }
    void FixedUpdate()
    {
        Vector3 v = Rigidbody.velocity;
        Rigidbody.velocity -= c * v.magnitude * v * Time.deltaTime;
        if (LastPosition.z * transform.position.z <= 0)
        {
            if (transform.position.z > 0 || LastPosition.z < 0) enableToHitBall = team.Blue;
            else if (transform.position.z < 0 || LastPosition.z > 0) enableToHitBall = team.Red;
        }
        if (Rigidbody.velocity.magnitude>0.1f)transform.rotation = Quaternion.LookRotation(Rigidbody.velocity.normalized);
    }
    private void OnCollisionEnter(Collision collision)
    {
        scored = true;
        if (enableToHitBall == team.Red)
        {
            RedSectionScript court = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().singleCourt.GetComponent<courtEntityBase>().section1;
            if (court.isOut) scoredTeam = team.Red;
            else scoredTeam = team.Blue;
        }
        else { 
            BlueSectionScript court = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().singleCourt.GetComponent<courtEntityBase>().section2;
            if (court.isOut) scoredTeam = team.Blue;
            else scoredTeam = team.Red;
        }
    }
}
