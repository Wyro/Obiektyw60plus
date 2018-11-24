using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelected : MonoBehaviour {

    public bool rayHit;
    public GameObject selectedObject;

    private int redCol;
    private int greenCol;
    private int blueCol;
    private int alpha = 80;
    private bool lookingAtObject = false;
    private bool flashingIn = true;
    private bool startedFlashing = false;
    private bool coroutineStarted = false;
    private Color32 initialColor;

    // Update is called once per frame
    void Update ()
    {

        changeColor();

        if (rayHit && !coroutineStarted)
        {
            selectedObject = GameObject.Find(CastingToObject.selectedObject);
            lookingAtObject = true;

            if (!startedFlashing)
            {
                startedFlashing = true;
                initialColor = GetComponent<Renderer>().material.color;
                StartCoroutine(FlashObject());
                coroutineStarted = true;
            }
        }
        else if (!rayHit && coroutineStarted)
        {
            lookingAtObject = false;
            startedFlashing = false;
            StopCoroutine(FlashObject());
            coroutineStarted = false;
            selectedObject.transform.gameObject.GetComponent<Renderer>().material.color = initialColor;
        }


    }

    private void changeColor()
    {
        if (lookingAtObject)
        {
            //selectedObject.transform.gameObject.GetComponent<Renderer>().material.color = new Color32((byte)redCol, (byte)greenCol, (byte)blueCol, 255);
            selectedObject.transform.gameObject.GetComponent<Renderer>().material.color = new Color32(initialColor.r, initialColor.g, initialColor.b, (byte)alpha);
        }
    }

    IEnumerator FlashObject()
    {
        while (lookingAtObject)
        {

            yield return new WaitForSeconds(0.05f);
            //if (flashingIn)
            //{
            //    if (blueCol <= 30)
            //    {
            //        flashingIn = false;
            //    }
            //    else
            //    {
            //        blueCol -= 10;
            //        greenCol -= 5;
            //    }

            //}

            //if (!flashingIn)
            //    {
            //        if (blueCol >= 250)
            //        {
            //            flashingIn = true;
            //        }
            //        else
            //        {
            //            blueCol += 10;
            //            greenCol += 5;

            //        }
            //    }

            if (flashingIn)
            {
                if (alpha <= 80)
                {
                    flashingIn = false;
                }
                else
                {
                    alpha -= 5;
                }

            }

            if (!flashingIn)
            {
                if (alpha >= 160)
                {
                    flashingIn = true;
                }
                else
                {
                    alpha += 5;

                }
            }
        }
    }





}
