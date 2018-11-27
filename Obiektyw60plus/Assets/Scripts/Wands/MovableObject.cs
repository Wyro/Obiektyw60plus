﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Attach the script to the object, that you want to move
//Requires also HighlightSelected script attached.
public class MovableObject : MonoBehaviour {

    public GameObject selectedObject;
    HighlightSelected highlightSelected;
    bool lookingAtObject = false;
    bool coroutineStarted = false;
    bool startedMoving = false;
    Rigidbody rigidbody;
    public int cnt = 0;
    bool move = true;
    GameObject wand;
    WandOfMoveFurniture wandOfMove;

    bool IsPushing = true; //TODO remove this later, it only temporary replaces WandOfMoveFurniture.IsPushing
	// Use this for initialization
	void Start () {

        wand = GameObject.Find("WandOfMoveFurniture");
        if (!wand)
        {
            print("can't find wand of move furniture");
        }
        else
        {
            wandOfMove = wand.GetComponent<WandOfMoveFurniture>();
        }

        highlightSelected = GetComponent<HighlightSelected>();
        if (!highlightSelected)
        {
            print("no HighlightSelected script attached");
        }
        rigidbody = GetComponent<Rigidbody>();
        if (!rigidbody)
        {
            print("no Rigidbody attached");
        }
	}
	
	// Update is called once per frame
	void Update () {

        //TODO remove this later
        if (Input.GetKeyDown("p"))
        {
            IsPushing = !IsPushing;
        }


        if (highlightSelected.rayHit && !coroutineStarted)
        {
            selectedObject = GameObject.Find(CastingToObject.selectedObject);
            lookingAtObject = true;

            if (!startedMoving)
            {
                StartCoroutine(MoveObject());
                coroutineStarted = true;
            }
        }
        else if (!highlightSelected.rayHit && coroutineStarted)
        {
            lookingAtObject = false;
            StopCoroutine(MoveObject());
            coroutineStarted = false;
        }
    }

    IEnumerator MoveObject()
    {
        while (lookingAtObject)
        {
            Vector3 wandDirection;
            if (IsPushing)
            {
                wandDirection = wand.transform.forward;
            }
            else
            {
                wandDirection = wand.transform.forward * -1;
            }
            yield return new WaitForSeconds(0.05f);

            print("moving moving ...");
            if (move)
            {   
                if(cnt <= 0)
                {
                    move = false;
                }
                else
                {

                    rigidbody.AddForce(transform.up * 100f);
                    //TODO add transform of wand in AddForce
                    //rigidbody.AddForce(wandDirection * 100f);
                    cnt--;
                }

            }
            else
            {
                if (cnt >= 100)
                {
                    move = true;
                }
                else
                {
                    rigidbody.AddForce(transform.up * -100f);
                    //TODO add transform of wand in AddForce
                    //rigidbody.AddForce(wandDirection * 100f);
                    cnt++;
                }

            }


        }
    }
}