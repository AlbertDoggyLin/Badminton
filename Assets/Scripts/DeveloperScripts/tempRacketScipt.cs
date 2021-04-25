using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempRacketScipt : MonoBehaviour
{
    private GameObject ball;
    private Vector3 tempSpeed;
    private void Start()
    {
        ball=GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position -= tempSpeed *2f* Time.deltaTime;
        }
        else
        {
            tempSpeed = ball.GetComponent<Rigidbody>().velocity;
            transform.position = ball.transform.position + tempSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(-ball.GetComponent<Rigidbody>().velocity);
        }
    }
}
