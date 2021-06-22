using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ballCatcherScript : MonoBehaviour
{
    private SteamVR_Behaviour_Pose m_racket;
    private Transform m_racket_transform;
    private leftHand leftHand;
    private Transform lastFrameRacketSimulate;
    private Vector3 lastHeadPosition;
    private Quaternion lastRotation;
    private Vector3 lastAngularVelocity;
    private Vector3 lastVelocity;
    private Vector3 lastFoward;
    private Vector3 lastBasePosition;
    public bool hited;
    public Vector3 ballHitedPosition;
    public Vector3 ballHitedSpeed;
    private void Start()
    {
        lastFrameRacketSimulate = transform.parent.GetChild(2);
        m_racket_transform = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().racket_transform;
        m_racket = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().racket_pose;
        leftHand = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().leftHand.GetComponent<leftHand>();
        lastHeadPosition = transform.position;
        lastRotation = m_racket_transform.rotation;
    }
    private void FixedUpdate()
    {
        //Adiust LastFramSimulater
        lastFrameRacketSimulate.rotation = lastRotation;
        lastFrameRacketSimulate.position = lastHeadPosition;
        Vector3 swingDir = Vector3.Cross(m_racket.GetAngularVelocity(), m_racket.transform.position - m_racket_transform.position).normalized;
        Vector3 currentV = (m_racket.transform.position - m_racket_transform.position).magnitude * m_racket.GetAngularVelocity().magnitude * swingDir + m_racket.GetVelocity();
        Vector3 localV = Quaternion.Inverse(transform.rotation) * currentV;
        int c = 12;
        transform.localScale = new Vector3(1.1f, 3.3f +c *Mathf.Abs(localV.y), 1.1f);
        lastFrameRacketSimulate.localScale = transform.localScale;
        //if Hit Deal with hitting ball
        if (hited)
        {
            hited = false;
            if (leftHand.holdingBall == true) leftHand.holdingBall = false;
            float min = 10;float index = -10;
            for(float i = -5; i <= 1.5; i += 0.1f)
            {
                Vector3 testUp = Quaternion.Lerp(lastRotation, transform.rotation, i) * new Vector3(0,-1,0);
                float k = Vector3.Dot(testUp, Vector3.Lerp(lastBasePosition, m_racket_transform.position, i));
                Vector3 testBP = ballHitedPosition - i * ballHitedSpeed * Time.deltaTime;
                if (Mathf.Abs((Vector3.Dot(testUp, testBP) - k) / testUp.magnitude) < min) index = i;
            }
            if (index == -10) Debug.LogError("Count Ball fault");
            else
            {

                Vector3 stickDir = ballHitedPosition - Vector3.Lerp(lastBasePosition, m_racket_transform.position, index);
                Vector3 racketFoword = Quaternion.Lerp(lastRotation, transform.rotation, index) * new Vector3(0, -1, 0);
                Vector3 lerpedAVvector = Quaternion.Lerp(Quaternion.LookRotation(lastAngularVelocity.normalized),
                    Quaternion.LookRotation(m_racket.GetAngularVelocity().normalized), index).eulerAngles;
                lerpedAVvector *= Mathf.Lerp(lastAngularVelocity.magnitude, m_racket.GetAngularVelocity().magnitude, index);
                Vector3 SwingDir = Vector3.Cross(lerpedAVvector, stickDir).normalized;
                Vector3 lerpedVelocity = Vector3.Lerp(lastVelocity, m_racket.GetVelocity(), index);
                float HitBallSpeed = Vector3.Dot(stickDir.magnitude * lerpedAVvector.magnitude * SwingDir + lerpedVelocity, racketFoword);
                float ballReflectSpeedDelta = (HitBallSpeed - Vector3.Dot(racketFoword, ballHitedSpeed)) * (1.6f);
                Vector3 testV = ballHitedSpeed + racketFoword * ballReflectSpeedDelta;
                if (Vector3.Dot(testV, ballHitedSpeed) < 0 || testV.magnitude > ballHitedSpeed.magnitude)
                {
                    print(testV.magnitude);
                    GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball.GetComponent<Rigidbody>().velocity = testV;
                    GameInputController.Instance.RightHandShake(testV.magnitude);
                }
            }
        }
        //Record imformation for last Frame
        lastRotation = m_racket_transform.rotation;
        lastHeadPosition = transform.position;
        lastAngularVelocity = m_racket.GetAngularVelocity();
        lastVelocity = m_racket.GetVelocity();
        lastFoward = -transform.up;
        lastBasePosition = m_racket_transform.position;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody?.gameObject == GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball)
        {
            Invoke("setBallEnable", 0.5f);
            hited = true;
            ballHitedPosition = other.transform.position;
            ballHitedSpeed = other.attachedRigidbody.velocity;
        }
    }
    private void setBallEnable()
    {
        GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball.GetComponent<ballScript>().enableToHitBall = team.None;
        hited = false;
    }
}
