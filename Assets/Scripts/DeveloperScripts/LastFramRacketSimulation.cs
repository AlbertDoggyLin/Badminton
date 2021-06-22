using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LastFrameRacketSimulation : MonoBehaviour
{
    private ballCatcherScript ballCatcherScript;
    private void Awake()
    {
        ballCatcherScript = transform.parent.GetChild(0).GetComponent<ballCatcherScript>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody?.gameObject == GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball)
        {
            ballCatcherScript.hited = true;
            ballCatcherScript.ballHitedPosition = other.transform.position;
            ballCatcherScript.ballHitedSpeed = other.attachedRigidbody.velocity;
        }
    }
}
