using UnityEngine;
using System;


namespace Assets.Scripts
{
    /// <summary>
    /// Simulation of eye dissfunction
    /// </summary>
    [ExecuteInEditMode, ImageEffectAllowedInSceneView]
    public class DepthOfFieldEffect : MonoBehaviour
    {

        const int circleOfConfusionPass = 0;
        const int preFilterPass = 1;
        const int bokehPass = 2;
        const int postFilterPass = 3;
        const int combinePass = 4;

        /// <summary>
        /// Can change focus distance in edit mode
        /// </summary>
        [Range(0.1f, 100f)]
        public float focusDistance = 2f;
        /// <summary>
        /// Can change range of focus in edit mode
        /// </summary>
        [Range(0.1f, 100f)]
        public float focusRange = 10f;
        /// <summary>
        /// Can change the intensity of blur
        /// </summary>
        [Range(1f, 10f)]
        public float bokehRadius = 2.08f;
        /// <summary>
        /// Necessary field of Class (DepthOfFieldShader)
        /// </summary>
        //[HideInInspector]
        public Shader dofShader;

        [NonSerialized]
        Material dofMaterial;
        

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (dofMaterial == null)
            {
                dofMaterial = new Material(dofShader);
                dofMaterial.hideFlags = HideFlags.HideAndDontSave;
            }

            dofMaterial.SetFloat("_BokehRadius", bokehRadius);
            dofMaterial.SetFloat("_FocusDistance", focusDistance);
            dofMaterial.SetFloat("_FocusRange", focusRange);

            RenderTexture coc = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.RHalf, RenderTextureReadWrite.Linear);

            int nonScaledWidth = source.width;
            int nonSclaedHeight = source.height;
            //Debug.Log("Non scaled width: " + nonScaledWidth);
            //Debug.Log("Non sclaed height: " + nonSclaedHeight);

            int width = source.width / 2;
            int height = source.height / 2;
            //Debug.Log("Scaled width: " + width);
            //Debug.Log("Sclaed height: " + height);
            RenderTextureFormat format = source.format;
            RenderTexture dof0 = RenderTexture.GetTemporary(width, height, 0, format);
            RenderTexture dof1 = RenderTexture.GetTemporary(width, height, 0, format);

            dofMaterial.SetTexture("_CoCTex", coc);
            dofMaterial.SetTexture("_DoFTex", dof0);

            Graphics.Blit(source, coc, dofMaterial, circleOfConfusionPass);
            Graphics.Blit(source, dof0, dofMaterial, preFilterPass);
            Graphics.Blit(dof0, dof1, dofMaterial, bokehPass);
            Graphics.Blit(dof1, dof0, dofMaterial, postFilterPass);
            Graphics.Blit(source, destination, dofMaterial, combinePass);

            RenderTexture.ReleaseTemporary(coc);
            RenderTexture.ReleaseTemporary(dof0);
            RenderTexture.ReleaseTemporary(dof1);
        }
    }
}