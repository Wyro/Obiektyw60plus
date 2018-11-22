using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayOrNight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            this.GetComponent<Light>().enabled = false;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            this.GetComponent<Light>().enabled = true;
        
    }
}
