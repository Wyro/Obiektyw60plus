﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsePurpleWand : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.One))//A key
        {
            print("using purple wand");
        }
    }
}
