using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalLineScript : MonoBehaviour
{
    public lineDetecter right { get; private set; }
    public lineDetecter left { get; private set; }
    public lineDetecter back { get; private set; }
    private void Awake()
    {
        right = transform.Find("right")?.GetChild(0).GetComponent<lineDetecter>();
        if (right == null) Debug.LogError("servingLineScript can't find right");
        left = transform.Find("left")?.GetChild(0).GetComponent<lineDetecter>();
        if (left == null) Debug.LogError("servingLineScript can't find left");
        back = transform.Find("back")?.GetChild(0).GetComponent<lineDetecter>();
        if (back == null) Debug.LogError("servingLineScript can't find back");
    }
    public void setUntouch()
    {
        right.Touch = false;
        left.Touch = false;
        back.Touch = false;
    }
}
