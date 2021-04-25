using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sectionScript : MonoBehaviour
{
    public servingLineScript servingLine{get;private set;}
    public normalLineScript normalLine{get;private set; }
    private void Awake()
    {
        servingLine = transform.Find("servingLine").GetComponent<servingLineScript>();
        if (servingLine == null) Debug.LogError("Section can't find serving line");
        normalLine = transform.Find("normalLine").GetComponent<normalLineScript>();
        if (normalLine == null) Debug.LogError("Section can't find normal line");
    }
}
