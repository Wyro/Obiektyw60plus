using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class CataractEffect : MonoBehaviour {

    public Shader cataractShader;    

    [NonSerialized]
    Material material = null;
    
    [Range(0.0f, 3.0f)]
    public float blurStrength = 2.2f;
    [Range(0.0f, 3.0f)]
    public float blurWidth = 1.0f;
    [Range(5.5f, 11.0f)]
    public float intensity = 11.0f;

    [Range(0.0f, 1.0f)]
    public float red = 0.8f;
    [Range(0.0f, 1.0f)]
    public float green = 0.75f;
    [Range(0.0f, 1.0f)]
    public float blue = 0.7f;

    public void Start()
    {  

    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material == null)
        {
            material = new Material(cataractShader)
            {
                hideFlags = HideFlags.HideAndDontSave
            };
        }

        material.SetFloat("_BlurStrength", blurStrength);
        material.SetFloat("_BlurWidth", blurWidth);
        material.SetFloat("_RedParam", red);
        material.SetFloat("_GreenParam", green);
        material.SetFloat("_BlueParam", blue);
        material.SetFloat("_Intensity", intensity);
        Graphics.Blit(source, destination, material);
    }
}
