using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseRedWand : MonoBehaviour {

	// Use this for initialization
	void Start () {
       OVRDebugConsole.Log("red wand");
    }
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.One))//A key
        {
            OVRDebugConsole.Log("using red wand");
        }
    }
}
