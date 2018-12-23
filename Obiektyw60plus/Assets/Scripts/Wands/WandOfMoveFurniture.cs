using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandOfMoveFurniture : MonoBehaviour {

    public bool IsPushing = true;
    public bool move = false;
    public Vector3 PointToGo;

    DistanceGrabbable distanceGrabbable;
    CastingToObject castingToObject;
    DrawLaser drawLaser;
    MoveShelves moveShelves;
    MoveDoor moveDoor;
    CurtainController curtainController;
    GameObject KitchenShelf;
    GameObject GameManager;

    bool UsingItem = false;

    // Variables for holding user input
    private float MovementInput;

    // Use this for initialization
    void Start()
    {
        KitchenShelf = GameObject.Find("salonRegal");
        if (!KitchenShelf) Debug.Log("Can't find salonRegal");

        GameManager = GameObject.Find("GameManager");
        if (!GameManager) Debug.Log("Can't find GameManager");

        distanceGrabbable = GetComponent<DistanceGrabbable>();
        castingToObject = GetComponent<CastingToObject>();
        drawLaser = GetComponent<DrawLaser>();
        moveShelves = KitchenShelf.GetComponent<MoveShelves>();
        curtainController = GameManager.GetComponent<CurtainController>();

        if (!distanceGrabbable) Debug.Log("No DistanceGrabbable script attached");
        if (!castingToObject) Debug.Log("No CastingToObject script attached");
        if (!drawLaser) Debug.Log("No DrawLaser script attached");
        if (!moveShelves) Debug.Log("No MoveShelves script attached");
        if (!curtainController) Debug.Log("No CurtainController script attached to GameManager object");
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput = Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger");

        //Input test
        //Debug.Log("Lindex " +Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger")); //left index
        //Debug.Log("Rindex " +Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger")); //right index
        //Debug.Log("Primary hand "+Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger")); //left grab
        //Debug.Log("Secondary hand "+Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger")); //right grab
        //Debug.Log("Primary thumbstick " + Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstick"));
        //Debug.Log("Secondary thumbstick " + Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstick"));
        if (IsSelectedShelf() && MovementInput > 0 &&!moveShelves.IsShelfSelected) moveShelves.IsShelfSelected = true; //moving shelves
        if (IsSelectedDoor() && MovementInput > 0 && !moveDoor.Move) moveDoor.Move = true; //moving door
        if (IsSelectedCurtain() && MovementInput > 0 && !curtainController.Use) curtainController.Use = true; //using curtains

        if (MovementInput > 0) { move = true; }
        else { move = false; }

        FindIntersectionPoint();

        if (distanceGrabbable.isGrabbed)
        {
            //use item
            if (!UsingItem)
            {
                castingToObject.IsCasting = true; //activating script for detecting which object we hit
                drawLaser.IsShowingLaser = true; 
                UsingItem = true;
            }
        }
        if (UsingItem && !distanceGrabbable.isGrabbed)
        {
            //stop using item
            UsingItem = false;
            castingToObject.IsCasting = false;
            drawLaser.IsShowingLaser = false;
        }
    }

    private bool IsSelectedShelf()
    {
        GameObject selectedObject = GameObject.Find(CastingToObject.selectedObject);
        if (!selectedObject) return false;
        if (selectedObject.transform.IsChildOf(KitchenShelf.transform))
        {
            return true;
        }
        else return false;
    }

    private bool IsSelectedDoor()
    {
        GameObject selectedObject = GameObject.Find(CastingToObject.selectedObject);
        if (!selectedObject) return false;
        if (selectedObject.GetComponent<MoveDoor>()) //if selected object has component MoveDoor
        {
            moveDoor = selectedObject.GetComponent<MoveDoor>();
            return true;
        }
        else return false;
    }

    private bool IsSelectedCurtain()
    {
        GameObject selectedObject = GameObject.Find(CastingToObject.selectedObject);
        //Debug.Log(selectedObject.name);
        if (!selectedObject) return false;
        if (selectedObject.name == "zaslony") 
        {
            return true;
        }
        else return false;
    }

    private void FindIntersectionPoint()
    {
        RaycastHit objectHit;

        Debug.DrawRay(transform.position, transform.forward, Color.blue, 2);
        // Shoot raycast
        if (Physics.Raycast(transform.position, transform.forward, out objectHit, 50))
        {
            PointToGo = objectHit.point;
        }
    }

}
