using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrackingUI : MonoBehaviour
{
    public OVRHand hand;
    public OVRInputModule inputModule;
    // Start is called before the first frame update
    void Start()
    {
        inputModule.rayTransform = hand.PointerPose;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
