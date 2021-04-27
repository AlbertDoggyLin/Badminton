using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftHand : GameEntityBase
{
    public override void EntityDispose()
    {
    }
    public bool holdingBall { get; set; }
    private GameObject ball;
    private void Start()
    {
        ball = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball;
        holdingBall = true;
    }
    private void Update()
    {
        if (holdingBall)
        {
            ball.transform.position = transform.position;
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.transform.rotation = transform.rotation;
        }
        if (GameInputController.Instance.leftHandTouch) holdingBall = false;
    }
}
