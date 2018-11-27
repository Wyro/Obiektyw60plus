using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandOfMoveFurniture : MonoBehaviour {

    public bool IsPushing = true;


    ParticleSystem particleSystem;
    DistanceGrabbable distanceGrabbable;
    CastingToObject castingToObject;

    bool UsingItem = false;


    // Use this for initialization
    void Start()
    {
        distanceGrabbable = GetComponent<DistanceGrabbable>();
        particleSystem = GetComponent<ParticleSystem>();
        castingToObject = GetComponent<CastingToObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (distanceGrabbable.isGrabbed)
        {
            //use item
            if (!UsingItem)
            {
                castingToObject.IsCasting = true; //activating script for raycasting

                if (OVRInput.GetDown(OVRInput.Button.Two)) { 
                    IsPushing = !IsPushing;
                }

                if (OVRInput.GetDown(OVRInput.Button.One))//A key
                {
                    StartCoroutine(UseWand());
                    particleSystem.Play();
                    UsingItem = true;
                }

            }
        }
        if (UsingItem && !distanceGrabbable.isGrabbed)
        {
            //stop using item
            UsingItem = false;
            castingToObject.IsCasting = false;
            StopCoroutine(UseWand());
            particleSystem.Stop();
        }
    }


    IEnumerator UseWand()
    {
        for (int i = 0; i < 10; i++)
        {
            particleSystem.Emit(1);
            particleSystem.emissionRate = 10f;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
