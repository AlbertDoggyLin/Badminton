using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSectionScript : MonoBehaviour
{
    public bool isOut { get; private set; }
    private servingLineScript servingLine { get; set; }
    private normalLineScript normalLine { get; set; }
    private ballScript ball;
    private void Awake()
    {
        servingLine = transform.Find("servingLine").GetComponent<servingLineScript>();
        if (servingLine == null) Debug.LogError("Section can't find serving line");
        normalLine = transform.Find("normalLine").GetComponent<normalLineScript>();
        if (normalLine == null) Debug.LogError("Section can't find normal line");
    }
    private void Start()
    {
        ball = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball.GetComponent<ballScript>();
    }
    private void Update()
    {
        isOut = false;
        if (ball.currentStatus == GameStatus.Serving)
        {
            if (ball.transform.position.z <= servingLine.front.transform.position.z) isOut = true;
            else if (ball.transform.position.z >= servingLine.front.transform.position.z) isOut = true;
            if (servingLine.front.Touch) isOut = false;
            if (servingLine.back.Touch) isOut = false;
            if (isOut) return;
            if(normalLine.left.transform.position.x<=ball.transform.position.x)isOut = true;
            else if (normalLine.right.transform.position.x >= ball.transform.position.x) isOut = true;
            if (normalLine.left.Touch) isOut = false;
            if (normalLine.right.Touch) isOut = false;
            if (isOut) return;
            if (ball.currentPosition == ServingPosition.Right && servingLine.straight.transform.position.x <= ball.transform.position.x) isOut = true;
            else if (servingLine.straight.transform.position.x >= ball.transform.position.x) isOut = true;
            if (servingLine.straight.Touch) isOut = false;
        }
        else if (ball.currentStatus == GameStatus.Normal)
        {
            if (ball.transform.position.z <= normalLine.back.transform.position.z) isOut = true;
            else if (ball.transform.position.z >= 0) isOut = true;
            if (normalLine.back.Touch) isOut = false;
            if (isOut) return;
            if(normalLine.left.transform.position.x<=ball.transform.position.x)isOut = true;
            else if (normalLine.right.transform.position.x >= ball.transform.position.x) isOut = true;
            if (normalLine.left.Touch) isOut = false;
            if (normalLine.right.Touch) isOut = false;
            if (isOut) return;
        }
    }
    public void setUntouch()
    {
        servingLine.setUntouch();
        normalLine.setUntouch();
    }
}
