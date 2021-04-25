using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class courtEntityBase : GameEntityBase
{
    public sectionScript section1 { get; private set; }
    public sectionScript section2 { get; private set; }
    private new void Awake()
    {
        base.Awake();
        section1 = transform.Find("section1")?.GetComponent<sectionScript>();
        if (section1 == null) Debug.LogError("courtEntityBase can't find section1");
        section2 = transform.Find("section2")?.GetComponent<sectionScript>();
        if (section2 == null) Debug.LogError("courtEntityBase can't find section2");
    }
    public override void EntityDispose()
    {

    }
}
