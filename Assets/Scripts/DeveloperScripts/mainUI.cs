using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class mainUI : MonoBehaviour
{
    public Button startbut;
    private void Awake()
    {
        GameSceneManager.Instance.Change2MainScene();
        startbut.onClick.AddListener(() => { GameSceneManager.Instance.Change2MainScene(); });
    }
}
