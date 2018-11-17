using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseRedWand : MonoBehaviour {

    ParticleSystem particleSystem;
    DistanceGrabbable distanceGrabbable;
    bool UsingItem = false;

    // Use this for initialization
    void Start()
    {
        distanceGrabbable = GetComponent<DistanceGrabbable>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {



        if (distanceGrabbable.isGrabbed)
        {
            //use item
            if (!UsingItem)
            {
                if (OVRInput.GetDown(OVRInput.Button.One))//A key
                {
                    print("using red wand");
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
            //TODO stop coroutine
            StopCoroutine(UseWand());
            particleSystem.Stop();
            print("stopped using wand");
        }
    }

    IEnumerator UseWand()
    {
        for (int i = 0; i < 10; i++)
        {
            particleSystem.Emit(1);
            particleSystem.emissionRate = 10f;
            print("coroutine running every second");
            yield return new WaitForSeconds(0.5f);
        }
    }
}
