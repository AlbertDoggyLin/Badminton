using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : GameEntityBase
{
    public GameStatus currentStatus;
    public ServingPosition currentPosition;
    public team scoredTeam;
    public team enableToHitBall;
    public team lastTeamHitBall;
    public bool scored;
    private bool ColideWithNet;
    private Rigidbody Rigidbody;
    private Vector3 LastPosition;
    public const float c = 0.2f;
    private new void Awake()
    {
        base.Awake();
        ColideWithNet = false;
        currentPosition = ServingPosition.Right;
        currentStatus = GameStatus.Stop;
        scored = false;
        Rigidbody = GetComponent<Rigidbody>();
        if (Rigidbody == null) Debug.LogError("ball can't find rigidbody");
        LastPosition = transform.position;
    }
    public override void EntityDispose()
    {
    }
    void FixedUpdate()
    {
        if (!ColideWithNet)
        {
            Vector3 v = Rigidbody.velocity;
            Rigidbody.velocity -= c * v.magnitude * v * Time.deltaTime;
        }
        if (LastPosition.z * transform.position.z <= 0)
        {
            if (transform.position.z > 0 || LastPosition.z < 0) enableToHitBall = team.Blue;
            else if (transform.position.z < 0 || LastPosition.z > 0) enableToHitBall = team.Red;
        }
        if(Rigidbody.velocity.normalized != Vector3.zero)
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Rigidbody.velocity.normalized), Rigidbody.velocity.magnitude * 3);
        LastPosition = transform.position;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.name == "net") ColideWithNet = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (scored) return;
        if (collision.collider.name == "net") {
            ColideWithNet = true;
            return;
        }
        scored = true;
        if (lastTeamHitBall == team.Blue)
        {
            RedSectionScript court = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().singleCourt.GetComponent<courtEntityBase>().section1;
            if (court.isOut())
            {
                scoredTeam = team.Red;
                print("blue out");
            }
            else
            {
                scoredTeam = team.Blue;
                print("blue in");
            }
        }
        else if (lastTeamHitBall == team.Red)
        {
            BlueSectionScript court = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().singleCourt.GetComponent<courtEntityBase>().section2;
            if (court.isOut())
            {
                scoredTeam = team.Blue;
                print("red out");
            }
            else
            {
                scoredTeam = team.Red;
                print("red in");
            }
        }
    }
}
