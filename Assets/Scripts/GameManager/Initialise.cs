using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialise : MonoBehaviour {

    public GameObject wandRed;
    public GameObject wandGreen;
    public GameObject wandPurple;

    // Use this for initialization
    void Start()
    {
        //deactivate all the scripts that should be run when weapons are used

        wandRed.GetComponent<UseRedWand>().enabled = false;
        wandGreen.GetComponent<UseGreenWand>().enabled = false;
        wandPurple.GetComponent<UsePurpleWand>().enabled = false;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
