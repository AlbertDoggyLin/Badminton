using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineDetecter : MonoBehaviour
{
    public bool Touch = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball)
        {
            Touch = false;
        }
    }
}
