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

    bool UsingItem = false;

    // Variables for holding user input
    private float MovementInput;

    // Use this for initialization
    void Start()
    {
        distanceGrabbable = GetComponent<DistanceGrabbable>();
        castingToObject = GetComponent<CastingToObject>();
        drawLaser = GetComponent<DrawLaser>();

        if (!distanceGrabbable) Debug.Log("No DistanceGrabbable script attached");
        if (!castingToObject) Debug.Log("No CastingToObject script attached");
        if (!drawLaser) Debug.Log("No DrawLaser script attached");
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput = Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger"); //TODO check this input

        //Input test
        //Debug.Log("Lindex " +Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger")); //left index
        //Debug.Log("Rindex " +Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger")); //right index
        //Debug.Log("Primary hand "+Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger")); //left grab
        //Debug.Log("Secondary hand "+Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger")); //right grab
        //Debug.Log("Primary thumbstick " + Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstick"));
        //Debug.Log("Secondary thumbstick " + Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstick"));


        if (MovementInput > 0) { move = true; }
        else{ move = false; }

        Debug.Log("move = "+move);

        FindIntersectionPoint();            
        
        if (distanceGrabbable.isGrabbed)
        {         
            //use item
            if (!UsingItem)
            {
                castingToObject.IsCasting = true; //activating script for detecting which object we hit
                drawLaser.IsShowingLaser = true; 
                StartCoroutine(UseWand());
                UsingItem = true;
            }
        }
        if (UsingItem && !distanceGrabbable.isGrabbed)
        {
            //stop using item
            UsingItem = false;
            castingToObject.IsCasting = false;
            drawLaser.IsShowingLaser = false;
            StopCoroutine(UseWand());
        }
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

    IEnumerator UseWand()
    {
        while (UsingItem)
        {
            //Handling user input

            //if (OVRInput.GetDown(OVRInput.Button.Two))
            //{
            //    IsPushing = !IsPushing;
            //}

            //if (OVRInput.GetDown(OVRInput.Button.One))//A key
            //{
            //    move = !move;
                
            //}
            //TODO maybe change color of laser when moving ?
        }
        
            yield return new WaitForSeconds(0.05f);
    }
}
