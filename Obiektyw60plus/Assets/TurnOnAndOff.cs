using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnAndOff : MonoBehaviour {

    public Light PointLight;
    private bool lightEnabled = false;

	// Use this for initialization
	void Start ()
    {
        PointLight = this.GetComponentInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void SwitchLight()
    {
        PointLight.enabled = !PointLight.enabled;
    }
}
