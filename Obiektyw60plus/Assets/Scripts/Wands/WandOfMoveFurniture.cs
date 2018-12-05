using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandOfMoveFurniture : MonoBehaviour {

    public bool IsPushing = true;
    public bool move = false;

    ParticleSystem particleSystem;
    DistanceGrabbable distanceGrabbable;
    CastingToObject castingToObject;
    DrawLaser drawLaser;

    bool UsingItem = false;


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
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            IsPushing = !IsPushing;
        }

        if (OVRInput.GetDown(OVRInput.Button.One))//A key
        {
            move = !move;

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
