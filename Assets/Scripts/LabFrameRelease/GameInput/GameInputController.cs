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
    public void RightHandShake(float v)
    {
        SteamVR_Actions.default_Haptic.Execute(0, 0.1f, 1, 0.05f*v, SteamVR_Input_Sources.RightHand);
    }
}
