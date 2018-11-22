using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject gameModePanel;
    public GameObject storyModePanel;
    public GameObject designerModePanel;
    public GameObject settingsPanel;
    public GameObject aboutPanel;
    public GameObject confirmQuitPanel;
    
    public GameObject pauseCanvas;
    public GameObject playerController;
    

    // Use this for initialization
    void Start() {
        BackToMainPanel();
    }

    // Update is called once per frame
    void Update() {
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            Vector3 playerPosition = (playerController.transform.forward * 1.0f) + playerController.transform.position;
            //pauseCanvas.transform.position = new Vector3(0, 1.2f, 0);
            pauseCanvas.transform.position = playerPosition;
            pauseCanvas.transform.rotation = playerController.transform.rotation;
        }

    }

    // Exit button functionality
    public void ExitGame()
    {
        Application.Quit();
    }


    public void OpenStartOptionsMenu()
    {
        mainPanel.SetActive(false);
        gameModePanel.SetActive(true);
        storyModePanel.SetActive(false);
        designerModePanel.SetActive(false);
        settingsPanel.SetActive(false);
        aboutPanel.SetActive(false);
        confirmQuitPanel.SetActive(false);
    }

    public void OpenStartStoryMenu()
    {
        gameModePanel.SetActive(false);
        storyModePanel.SetActive(true);

        PlayerPrefs.SetInt("GameMode", 0);
    }

    public void OpenStartDesignerMenu()
    {
        gameModePanel.SetActive(false);
        designerModePanel.SetActive(true);

        PlayerPrefs.SetInt("GameMode", 1);
    }


    // Scene loader
    public void LoadScene(string sceneName)
    {
        //SceneManager.LoadScene(sceneName);
        Debug.Log(PlayerPrefs.GetInt("GameMode"));
    }

    // Back to main panel
    public void BackToMainPanel()
    {
        mainPanel.SetActive(true);
        gameModePanel.SetActive(false);
        storyModePanel.SetActive(false);
        designerModePanel.SetActive(false);
        settingsPanel.SetActive(false);
        aboutPanel.SetActive(false);
        confirmQuitPanel.SetActive(false);
    }

    public void ToggleYellowing()
    {

    }


    // 
    public void SwitchPanelsToAbout()
    {
        mainPanel.SetActive(false);
        //optionsPanel.SetActive(false);
        aboutPanel.SetActive(true);
    }

    // 
    public void SwitchPanelsToOptions()
    {
        mainPanel.SetActive(false);
        //optionsPanel.SetActive(true);
        aboutPanel.SetActive(false);
    }
    

}
