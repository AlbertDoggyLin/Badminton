using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainGameTask : TaskBase
{
    private GameDataManager.score Score;
    private GameObject ball;
    private ballScript ballScript;
    private leftHand leftHand;
    public static team winner=team.None;
    public override IEnumerator TaskInit()
    {
        Score = GameDataManager.Instance.Score;
        ball = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball;
        leftHand = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().leftHand.GetComponent<leftHand>();
        winner = team.None;
        Score.red = 0;
        Score.blue = 0;
        ballScript = ball.GetComponent<ballScript>();
        yield return null;
    }
    public override IEnumerator TaskStart(){
        while (true)
        {
            yield return new WaitUntil(() => ballScript.scored);
            if (ballScript.scoredTeam == team.Red) Score.red++;
            if (ballScript.scoredTeam == team.Blue) Score.blue++;
            if (Score.red == 30) { winner = team.Red; break; }
            if (Score.blue == 30) {winner = team.Blue; break;}
            if (Mathf.Abs(Score.red - Score.blue) >= 2)
            {
                if(Score.red>Score.blue&&Score.red>=21) { winner = team.Red; break; }
                if(Score.blue>Score.red&&Score.blue>=21) { winner = team.Blue; break; }
            }
            yield return new WaitForSeconds(2f);
            leftHand.holdingBall = true;
            ballScript.enableToHitBall = team.Red;
            yield return new WaitForSeconds(0.2f);
            ballScript.scored = false;
        }
        yield return null;
    }
    public override IEnumerator TaskStop()
    {
        yield return null;
    }
}
