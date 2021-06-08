using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class courtEntityBase : GameEntityBase
{
    public RedSectionScript section1 { get; private set; }
    public BlueSectionScript section2 { get; private set; }
    private new void Awake()
    {
        base.Awake();
        section1 = transform.Find("section1")?.GetComponent<RedSectionScript>();
        if (section1 == null) Debug.LogError("courtEntityBase can't find section1");
        section2 = transform.Find("section2")?.GetComponent<BlueSectionScript>();
        if (section2 == null) Debug.LogError("courtEntityBase can't find section2");
    }
    public void setUntouch()
    {
        section1.setUntouch();
        section2.setUntouch();
    }
    public override void EntityDispose()
    {

    }
}
