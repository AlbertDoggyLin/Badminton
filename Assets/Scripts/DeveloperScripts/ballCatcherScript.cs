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
            float targetV = Mathf.Abs(Vector3.Dot(m_racket.velocity, other.attachedRigidbody.velocity))
                +Vector3.Cross(m_racket.angularVelocity,transform.position- m_racket_transform.position).magnitude;
            other.attachedRigidbody.velocity = (m_racket.velocity.magnitude+targetV)* 
                Vector3.Cross(m_racket.angularVelocity, transform.position - m_racket_transform.position).normalized;
        }
    }
}
