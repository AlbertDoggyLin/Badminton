﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainGameTask : TaskBase
{
    private GameDataManager.score Score;
    private GameObject ball;
    private ballScript ballScript;
    private GameDataManager.team winner;
    private leftHand leftHand;
    public override IEnumerator TaskInit()
    {
        Score = GameDataManager.Instance.Score;
        ball = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().ball;
        leftHand = GameEntityManager.Instance.GetCurrentSceneRes<SceneEntity>().leftHand.GetComponent<leftHand>();
        winner = GameDataManager.team.none;
        Score.red = 0;
        Score.blue = 0;
        ballScript = ball.GetComponent<ballScript>();
        yield return null;
    }
    public override IEnumerator TaskStart(){
        while (true)
        {
            yield return new WaitUntil(() => ballScript.scored);
            if (ballScript.scoredTeam == GameDataManager.team.red) Score.red++;
            if (ballScript.scoredTeam == GameDataManager.team.blue) Score.blue++;
            if (Score.red == 30) { winner = GameDataManager.team.red; break; }
            if (Score.blue == 30) {winner = GameDataManager.team.blue; break;}
            if (Mathf.Abs(Score.red - Score.blue) >= 2)
            {
                if(Score.red>Score.blue&&Score.red>=21) { winner = GameDataManager.team.red; break; }
                if(Score.blue>Score.red&&Score.blue>=21) { winner = GameDataManager.team.blue; break; }
            }
            leftHand.holdingBall = true;
            ballScript.scored = false;
        }
        yield return null;
    }
    public override IEnumerator TaskStop()
    {
        yield return null;
    }
}