using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArchitectMenu : MonoBehaviour {

    public Toggle wheelchairToggle;
    public GameObject wheelchairMesh;
    public GameObject eyesCamera;

	// Use this for initialization
	void Start () {
        wheelchairToggle.onValueChanged.AddListener(delegate {
            wheelchairToggleChanged(wheelchairToggle);
        });

    }

    private void wheelchairToggleChanged(Toggle wheelchairToggle)
    {
        if (wheelchairToggle.isOn)
        {
            wheelchairMesh.SetActive(true);
        }
        else
        {
            wheelchairMesh.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
