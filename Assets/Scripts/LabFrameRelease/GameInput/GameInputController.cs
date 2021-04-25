using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;
using Valve.VR;
public class GameInputController : MonoSingleton<GameInputController>
{
    public bool leftHandTouch { get; set; }
    public bool rightHandTouch { get; set; }
    private SteamVR_Action_Boolean leftHandGrib;
    private SteamVR_Action_Boolean rightHandGrib;
    public void Awake()
    {
        leftHandGrib = SteamVR_Actions.default_Teleport;
        rightHandGrib = SteamVR_Actions.default_Teleport;
    }
    public void Update()
    {
        leftHandTouch = leftHandGrib.GetState(SteamVR_Input_Sources.LeftHand);
        rightHandTouch = rightHandGrib.GetState(SteamVR_Input_Sources.RightHand);
    }
}
