using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.EyeEffectsPostprocessing {
    [ExecuteInEditMode]
    public class TransformationEffect : MonoBehaviour
    {
        public Shader transfShader;

        [NonSerialized]
        Material material = null;

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (material == null)
            {
                material = new Material(transfShader);
                transfShader.hideFlags = HideFlags.HideAndDontSave;
            }

            GetComponent<Renderer>().sharedMaterial.SetMatrix("_Trafo0",
                this.GetComponent<Camera>().GetComponent<Renderer>().localToWorldMatrix);  
                   
            Graphics.Blit(source, destination, material);
        }


    }
}
