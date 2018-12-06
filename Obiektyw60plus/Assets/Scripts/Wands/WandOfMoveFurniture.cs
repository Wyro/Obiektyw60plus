using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandOfMoveFurniture : MonoBehaviour {

    public bool IsPushing = true;
    public bool move = false;
    public Vector3 PointToGo;

    ParticleSystem particleSystem;
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
        particleSystem = GetComponent<ParticleSystem>();
        castingToObject = GetComponent<CastingToObject>();
        drawLaser = GetComponent<DrawLaser>();

        if (!distanceGrabbable) Debug.Log("No DistanceGrabbable script attached");
        if (!particleSystem) Debug.Log("No particle system attached");
        if (!castingToObject) Debug.Log("No CastingToObject script attached");
        if (!drawLaser) Debug.Log("No DrawLaser script attached");
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput = Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger");
        
        Debug.Log(MovementInput);

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            //IsPushing = !IsPushing;
            move = !move;
        }

        if (OVRInput.GetDown(OVRInput.Button.One))//A key
        {
            FindIntersectionPoint();            
        }

        

        if (distanceGrabbable.isGrabbed)
        {
            

            //use item
            if (!UsingItem)
            {
                castingToObject.IsCasting = true; //activating script for raycasting
                drawLaser.IsShowingLaser = true;
                StartCoroutine(UseWand());
                particleSystem.Play(); //maybe move to DrawLaser ?
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
            particleSystem.Stop(); //maybe move to DrawLaser ?
        }
    }

    private void FindIntersectionPoint()
    {
        RaycastHit objectHit;

        Debug.DrawRay(transform.position, transform.forward, Color.blue, 2);
        // Shoot raycast
        if (Physics.Raycast(transform.position, transform.forward, out objectHit, 50))
        {
            Debug.Log("Raycast hitted to: " + objectHit.collider);
            Debug.Log("Raycast hitted to: " + objectHit.point);
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
