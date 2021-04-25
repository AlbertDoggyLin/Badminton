using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector3 AIPosition => GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().Player.position;
    void Update()
    {
        transform.position = new Vector3(AIPosition.x, transform.position.y, AIPosition.z);       
    }
}
