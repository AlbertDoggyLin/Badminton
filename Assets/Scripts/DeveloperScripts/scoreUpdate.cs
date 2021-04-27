using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreUpdate : MonoBehaviour
{
    public Text redTeamScore;
    public Text blueTeamScore;
    private void Update()
    {
        redTeamScore.text = GameDataManager.Instance.Score.red.ToString();
        blueTeamScore.text = GameDataManager.Instance.Score.blue.ToString();

    }
}
