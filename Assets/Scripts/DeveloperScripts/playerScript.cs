using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerScript : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject ball;
    private ballScript ballScript;
    private bool chasingBall;
    private Rigidbody ballRigid;
    private void Start()
    {
        ball = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball;
        ballRigid = ball.GetComponent<Rigidbody>();
        ballScript = ball.GetComponent<ballScript>();
        chasingBall = false;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if(chasingBall){
            Vector3 predictedPosition;
            predictedPosition.y = transform.position.y;
            float t = (-ballRigid.velocity.y + Mathf.Sqrt(ballRigid.velocity.y * ballRigid.velocity.y + 40 * ball.transform.position.y)) / 20;
            agent.SetDestination(new Vector3(ball.transform.position.x+ballRigid.velocity.x*t, transform.position.y, Mathf.Min(0, ball.transform.position.z + ballRigid.velocity.z * t))-0.2f*transform.forward);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.rigidbody?.name=="ball")chasingBall = false;
    }
}
