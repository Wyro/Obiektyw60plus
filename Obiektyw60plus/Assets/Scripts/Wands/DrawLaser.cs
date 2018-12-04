﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLaser : MonoBehaviour {

    ParticleSystem particleSystem;
    public bool IsShowingLaser = false;
    bool Started = false;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        if (!particleSystem) Debug.Log("No particle system attached");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsShowingLaser && !Started)
        {
            StartCoroutine(ShowLaser());
            Started = true;
        }

        if (!IsShowingLaser && Started)
        {
            StopCoroutine(ShowLaser());
            Started = false;
        }
    }

    IEnumerator ShowLaser()
    {
        while (IsShowingLaser)
        {
            particleSystem.Emit(10);
            particleSystem.emissionRate = 1000f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
