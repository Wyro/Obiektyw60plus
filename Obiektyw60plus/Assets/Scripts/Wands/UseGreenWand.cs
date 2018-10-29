using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseGreenWand : MonoBehaviour {

	// Use this for initialization
	void Start () {
        OVRDebugConsole.Log("green wand");
    }
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.One))//A key
        {
            OVRDebugConsole.Log("using green wand");
        }
    }
}
