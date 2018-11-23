using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelected : MonoBehaviour {

    public bool rayCasted;

    public GameObject selectedObject;

    public int redCol;
    public int greenCol;
    public int blueCol;
    public bool lookingAtObject = false;
    public bool flashingIn = true;
    public bool startedFlashing = false;

    private bool coroutineStarted = false;
    private Color32 initialColor;

	// Use this for initialization
	void Start () {
        initialColor = selectedObject.transform.gameObject.GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {

        if (lookingAtObject)
        {
            selectedObject.transform.gameObject.GetComponent<Renderer>().material.color = new Color32((byte)redCol, (byte)greenCol, (byte)blueCol, 255);
        }

        if (rayCasted && !coroutineStarted)
        {
            selectedObject = GameObject.Find(CastingToObject.selectedObject);
            lookingAtObject = true;

            if (!startedFlashing)
            {
                startedFlashing = true;
                print("started coroutine");
                StartCoroutine(FlashObject());
                coroutineStarted = true;
            }
        }
        else if(!rayCasted && coroutineStarted)
        {
            lookingAtObject = false;
            startedFlashing = false;
            StopCoroutine(FlashObject());
            coroutineStarted = false;
            print("ended coroutine");
            selectedObject.transform.gameObject.GetComponent<Renderer>().material.color = initialColor;
            print(initialColor);
        }


	}

   IEnumerator FlashObject()
    {
        while (lookingAtObject)
        {

            yield return new WaitForSeconds(0.05f);
            if (flashingIn)
            {
                if (blueCol <= 30)
                {
                    flashingIn = false;
                }
                else
                {
                    blueCol -= 25;
                    greenCol -= 1;
                }

            }

            if (!flashingIn)
                {
                    if (blueCol >= 250)
                    {
                        flashingIn = true;
                    }
                    else
                    {
                        blueCol += 25;
                        greenCol += 1;
                    }
                }

        }
    }





}
