using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject languageManager;
    public GameObject mainPanel;
    public GameObject gameModePanel;
    public GameObject storyModePanel;
    public GameObject designerModePanel;
    public GameObject settingsPanel;
    public GameObject aboutPanel;
    public GameObject confirmQuitPanel;

    public GameObject pauseCanvas;
    //public GameObject playerController;
    public Dropdown languageSelection;
    public Slider volumeSlider;

    // Use this for initialization
    void Start()
    {
        // Add language dropdown listener
        languageSelection.onValueChanged.AddListener(delegate {
            LanguageChanged(languageSelection);
        });

        // Initialize language
        if (!PlayerPrefs.HasKey("lang"))
        {
            languageSelection.value = 0;
            PlayerPrefs.SetInt("lang",0);
        }
        else
        {
            languageSelection.value = PlayerPrefs.GetInt("lang");
        }

        // Add volume slider listener
        volumeSlider.onValueChanged.AddListener(delegate { VolumeChanged(); });

        // Initialize volume
        if (!PlayerPrefs.HasKey("volume"))
        {
            volumeSlider.value = 50;
            PlayerPrefs.SetInt("volume", 50);
        }
        else
        {
            volumeSlider.value = PlayerPrefs.GetInt("volume");
        }







        // Reset menu to proper layer configuration
        BackToMainPanel();
    }

    private void VolumeChanged()
    {
        PlayerPrefs.SetInt("volume", Mathf.RoundToInt(volumeSlider.value));
    }

    private void LanguageChanged(Dropdown languageSelection)
    {
        //throw new NotImplementedException();
        Debug.Log("Language changed to :" + languageSelection.value);
        string languageFile = null;
        switch (languageSelection.value)
        {
            case 0:
                languageFile = "localizedText_en.json";
                break;
            case 1:
                languageFile = "localizedText_pl.json";
                break;
            default:
                Debug.Log("Error with language dropdown settings!");
                break;
        }
        Debug.Log("Loading " + languageFile);
        ActivateAllPanels();
        languageManager.GetComponent<LocalizationManager>().LoadLocalizedText(languageFile);
        DeactivateAllPanels();

    }

    // Keep the menu in front of player
    void Update()
    {
        /*if (OVRInput.Get(OVRInput.Button.Two))
        {
            Vector3 playerPosition = (playerController.transform.forward * 1.0f) + playerController.transform.position;
            //pauseCanvas.transform.position = new Vector3(0, 1.2f, 0);
            pauseCanvas.transform.position = playerPosition;
            pauseCanvas.transform.rotation = playerController.transform.rotation;
        }*/


    }

    private void ActivateAllPanels()
    {
        mainPanel.SetActive(true);
        gameModePanel.SetActive(true);
        storyModePanel.SetActive(true);
        designerModePanel.SetActive(true);
        settingsPanel.SetActive(true);
        aboutPanel.SetActive(true);
        confirmQuitPanel.SetActive(true);
    }

    private void DeactivateAllPanels()
    {
        mainPanel.SetActive(false);
        gameModePanel.SetActive(false);
        storyModePanel.SetActive(false);
        designerModePanel.SetActive(false);
        settingsPanel.SetActive(true);  // Only settings should stay on
        aboutPanel.SetActive(false);
        confirmQuitPanel.SetActive(false);
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

    public void ChangeVisualImpairment()
    {
        //Debug.Log()
    }


    // 
    public void SwitchPanelsToAbout()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(false);
        aboutPanel.SetActive(true);
    }

    // 
    public void SwitchPanelsToSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
        aboutPanel.SetActive(false);
    }

    // Exit button functionality
    public void ExitGame()
    {
        Debug.Log("Quit called!");
        Application.Quit();
    }

    public void ConfirmQuitPanel()
    {
        mainPanel.SetActive(false);
        confirmQuitPanel.SetActive(true);
    }

}