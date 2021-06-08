using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ballCatcherScript : MonoBehaviour
{
    private SteamVR_Behaviour_Pose m_racket;
    private Transform m_racket_transform;
    private void Start()
    {
        m_racket_transform = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().racket_transform;
        m_racket = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().racket_pose;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody?.gameObject == GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball)
        {
            Invoke("setBallEnable", 0.2f);
            Rigidbody ball = other.attachedRigidbody;
            Vector3 stickDir = transform.position - m_racket_transform.position;
            Vector3 racketFoword = -transform.up;
            Vector3 swingDir = Vector3.Cross(m_racket.GetAngularVelocity(), stickDir).normalized;
            float HitBallSpeed = Vector3.Dot(stickDir.magnitude * m_racket.GetAngularVelocity().magnitude*swingDir + m_racket.GetVelocity(),racketFoword);
            float ballReflectSpeedDelta = (HitBallSpeed-Vector3.Dot(racketFoword,ball.velocity))*(1.6f);
            Vector3 testV = ball.velocity + racketFoword * ballReflectSpeedDelta;
            if (Vector3.Dot(testV, ball.velocity) < 0 || testV.magnitude > ball.velocity.magnitude) ball.velocity = testV;
        }
    }
    private void setBallEnable()
    {
        GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball.GetComponent<ballScript>().enableToHitBall = team.None;
    }
}
