using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject directionalLight;
    [SerializeField]
    private Material DaySkybox;
    [SerializeField]
    private Material NightSkybox;

    public void ToggleDirectional()
    {
        directionalLight.SetActive(!directionalLight.activeSelf);
        ToggleSkybox();
    }

    public void ToggleSkybox()
    {
        if (RenderSettings.skybox == DaySkybox)
            RenderSettings.skybox = NightSkybox;
        else
            RenderSettings.skybox = DaySkybox;
    }

}
