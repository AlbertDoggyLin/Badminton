using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SceneEntity : GameSceneEntityRes
{
    public GameObject singleCourt;
    public GameObject doubleCourt;
    public GameObject ball;
    public Transform Player;
    public Transform lastFrameSimulator;
    public ballCatcherScript ballCatcherScript;
    public SteamVR_Behaviour_Pose racket_pose;
    public Transform racket_transform;
    public GameObject leftHand;
}
