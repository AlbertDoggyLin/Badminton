using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class servingLineScript : MonoBehaviour
{
    public GameObject straight { get; private set; }
    public GameObject front { get; private set; }
    public GameObject back { get; private set; }
    private void Awake()
    {
        straight = transform.Find("StraightServingLine")?.gameObject;
        if (straight == null) Debug.LogError("servingLineScript can't find StraightServingLine");
        front = transform.Find("FrontServingLine")?.gameObject;
        if (front == null) Debug.LogError("servingLineScript can't find ColumnServingLine");
        back = transform.Find("BackServingLine")?.gameObject;
        if (front == null) Debug.LogError("servingLineScript can't find BackServingLine");
    }
}
