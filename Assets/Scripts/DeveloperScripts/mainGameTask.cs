using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainGameTask : TaskBase
{
    private GameDataManager.score Score;
    private GameObject ball;
    private ballScript ballScript;
    private leftHand leftHand;
    private BlueSectionScript blueSection;
    private RedSectionScript redSection;
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
        blueSection = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().singleCourt.GetComponent<courtEntityBase>().section2;
        redSection = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().singleCourt.GetComponent<courtEntityBase>().section1;
        yield return null;
    }
    public override IEnumerator TaskStart(){
        yield return new WaitUntil(() => leftHand.transform.position.y>=0.5);
        Score.red = Score.blue = 0;
        ballScript.scored = false;
        leftHand.holdingBall = true;
        ballScript.enableToHitBall = team.Red;
        ballScript.currentStatus = GameStatus.Serving;
        ballScript.lastTeamHitBall = team.Red;
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
            yield return new WaitForSeconds(0.5f);
            leftHand.holdingBall = true;
            ballScript.enableToHitBall = team.Red;
            ballScript.scored = false;
            ballScript.currentStatus = GameStatus.Serving;
            blueSection.setUntouch();
            redSection.setUntouch();
        }
        yield return null;
    }
    public override IEnumerator TaskStop()
    {
        ballScript.currentStatus = GameStatus.Stop;
        yield return null;
    }
}
