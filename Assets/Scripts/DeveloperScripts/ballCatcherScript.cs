using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ballCatcherScript : MonoBehaviour
{
    private SteamVR_Action_Pose m_racket;
    private Transform m_racket_transform;
    private void Start()
    {
        m_racket_transform = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().racket_transform;
        m_racket = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().racket_pose.poseAction;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody?.gameObject == GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball)
        {
            Rigidbody ball = other.attachedRigidbody;
            Vector3 stickDir = transform.position - m_racket_transform.position;
            Vector3 racketFoword = transform.up;
            Vector3 swingDir = Vector3.Cross(m_racket.angularVelocity, stickDir).normalized;
            float HitBallSpeed = stickDir.magnitude * m_racket.angularVelocity.magnitude*Vector3.Dot(swingDir,racketFoword) + Vector3.Dot(m_racket.velocity,racketFoword);
            Vector3 BallRacketRelativeVelocity = ball.velocity - HitBallSpeed * racketFoword;
            float ballReflectSpeedDelta = Mathf.Abs(Vector3.Dot(BallRacketRelativeVelocity, racketFoword))*2;
            ball.velocity = ball.velocity + racketFoword * ballReflectSpeedDelta;
        }
    }
}
