using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class gameEndTask : TaskBase
{
    public override IEnumerator TaskInit()
    {
        yield return new WaitUntil(() => mainGameTask.winner != team.None);
    }

    public override IEnumerator TaskStart()
    {
        yield return null;
    }

    public override IEnumerator TaskStop()
    {
        yield return null;
    }
}