using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ballCatcherScript : MonoBehaviour
{
    private SteamVR_Behaviour_Pose m_racket;
    private Transform m_racket_transform;
    private leftHand leftHand;
    private void Start()
    {
        m_racket_transform = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().racket_transform;
        m_racket = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().racket_pose;
        leftHand = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().leftHand.GetComponent<leftHand>();
    }
    private void FixedUpdate()
    {
        Vector3 stickDir = m_racket.transform.position - m_racket_transform.position;
        Vector3 swingDir = Vector3.Cross(m_racket.GetAngularVelocity(), stickDir).normalized;
        Vector3 currentV = stickDir.magnitude * m_racket.GetAngularVelocity().magnitude * swingDir + m_racket.GetVelocity();
        transform.localScale = 1.1f * new Vector3(1, 3, 1) + 
            3 * (Mathf.Abs(currentV.x)*transform.right + Mathf.Abs(currentV.y) * 3 * transform.up + Mathf.Abs(currentV.z)*transform.forward);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody?.gameObject == GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball)
        {
            if (leftHand.holdingBall == true) leftHand.holdingBall = false;
            Invoke("setBallEnable", 0.5f);
            Rigidbody ball = other.attachedRigidbody;
            Vector3 stickDir = other.transform.position - m_racket_transform.position;
            Vector3 racketFoword = -transform.up;
            Vector3 swingDir = Vector3.Cross(m_racket.GetAngularVelocity(), stickDir).normalized;
            float HitBallSpeed = Vector3.Dot(stickDir.magnitude * m_racket.GetAngularVelocity().magnitude * swingDir + m_racket.GetVelocity(), racketFoword);
            float ballReflectSpeedDelta = (HitBallSpeed - Vector3.Dot(racketFoword, ball.velocity)) * (1.6f);
            Vector3 testV = ball.velocity + racketFoword * ballReflectSpeedDelta;
            if (Vector3.Dot(testV, ball.velocity) < 0 || testV.magnitude > ball.velocity.magnitude)
            {
                print(testV.magnitude);
                ball.velocity = testV;
            }
        }
    }
    private void setBallEnable()
    {
        GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball.GetComponent<ballScript>().enableToHitBall = team.None;
    }
}
