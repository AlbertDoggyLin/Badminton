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
            Vector3 Bposition = transform.position - m_racket_transform.position;
            Vector3 swingDir = Vector3.Cross(m_racket.angularVelocity, Bposition).normalized;
            float swingSpeed = Bposition.magnitude * m_racket.angularVelocity.magnitude;
            float reflectionSpeed = Vector3.Dot(ball.velocity, swingDir);
            Debug.Log(ball.velocity.magnitude);
            Debug.Log(reflectionSpeed);
            if (reflectionSpeed > 0)
            {
                ball.velocity = ball.velocity + (2 * reflectionSpeed + swingSpeed) * swingDir;
            }
            else
            {
                float deltaV = swingSpeed - Vector3.Dot(swingDir, ball.velocity);
                if (deltaV > 0) ball.velocity += deltaV * swingDir;
            }
        }
    }
}
