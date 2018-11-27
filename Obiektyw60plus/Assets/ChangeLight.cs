using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLight : MonoBehaviour {

    public Light PointLight;

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

    public void SwitchLight(Color color)
    {
        PointLight.color = color;
    }
}
