using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalLineScript : MonoBehaviour
{
    public GameObject right { get; private set; }
    public GameObject left { get; private set; }
    public GameObject back { get; private set; }
    private void Awake()
    {
        right = transform.Find("right")?.gameObject;
        if (right == null) Debug.LogError("servingLineScript can't find right");
        left = transform.Find("left")?.gameObject;
        if (left == null) Debug.LogError("servingLineScript can't find left");
        back = transform.Find("back")?.gameObject;
        if (back == null) Debug.LogError("servingLineScript can't find back");
    }
}
