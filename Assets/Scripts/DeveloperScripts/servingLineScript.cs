using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class servingLineScript : MonoBehaviour
{
    public lineDetecter straight { get; private set; }
    public lineDetecter front { get; private set; }
    public lineDetecter back { get; private set; }
    private void Awake()
    {
        straight = transform.Find("StraightServingLine")?.GetComponent<lineDetecter>();
        if (straight == null) Debug.LogError("servingLineScript can't find StraightServingLine");
        front = transform.Find("FrontServingLine")?.GetComponent<lineDetecter>();
        if (front == null) Debug.LogError("servingLineScript can't find ColumnServingLine");
        back = transform.Find("BackServingLine")?.GetComponent<lineDetecter>();
        if (front == null) Debug.LogError("servingLineScript can't find BackServingLine");
    }
    public void setUntouch()
    {
        straight.Touch = false;
        front.Touch = false;
        back.Touch = false;
    }
}
