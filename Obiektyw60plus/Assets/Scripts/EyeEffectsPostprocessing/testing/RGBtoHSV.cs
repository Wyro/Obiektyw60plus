using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class RGBtoHSV : MonoBehaviour {

    public Shader cataractShader;

    [NonSerialized]
    Material material = null;

    [Range(0.0f, 1.0f)]
    public float hueShift = 0.0f;
    [Range(0.0f, 1.0f)]
    public float saturation = 1.0f;
    [Range(0.0f, 1.0f)]
    public float value = 1.0f;
                   
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material == null)
        {
            material = new Material(cataractShader)
            {
                hideFlags = HideFlags.HideAndDontSave
            };
        }
         
        material.SetFloat("_HueShift", hueShift);
        material.SetFloat("_Sat", saturation);
        material.SetFloat("_Val", value);
        Graphics.Blit(source, destination, material);
    }
}
