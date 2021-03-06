using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DataSync;
using GameData;
using LabData;
using Newtonsoft.Json;
using UnityEngine;


public class GameDataManager : MonoSingleton<GameDataManager>, IGameManager
{
    /// <summary>
    /// LabData
    /// </summary>
    public class score
    {
        public score() { red = 0;blue = 0; }
        public int red { get; set; }
        public int blue { get; set; }
    }
    public score Score;
    public static ILabDataManager LabDataManager { get; set; }

    /// <summary>
    /// 游戏数据
    /// </summary>
    public static GameFlowData FlowData { get; set; }

    int IGameManager.Weight => GobalData.GameDataManagerWeight;

    void IGameManager.ManagerInit()
    {
        LabDataManager = new LabDataManager();
        Score = new score();
    }

    IEnumerator IGameManager.ManagerDispose()
    {
        LabDataManager.LabDataDispose();
        yield return null;
    }
}
