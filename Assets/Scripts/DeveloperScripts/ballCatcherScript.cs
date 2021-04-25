using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCatcherScript : MonoBehaviour
{
    private Rigidbody m_rigid;
    private void Awake()
    {
        m_rigid = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().racket;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody?.gameObject == GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball)
        {
            float targetV = Mathf.Abs(Vector3.Dot(m_rigid.velocity.normalized, other.attachedRigidbody.velocity))
                +Vector3.Cross(m_rigid.angularVelocity,transform.position-m_rigid.transform.position).magnitude;
            Debug.Log(targetV);
            other.attachedRigidbody.velocity = (m_rigid.velocity.magnitude+targetV)*m_rigid.velocity.normalized;
        }
    }
}
