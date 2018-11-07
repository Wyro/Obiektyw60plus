using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject aboutPanel;

	// Use this for initialization
	void Start () {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        aboutPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Exit button functionality
    public void ExitGame()
    {
        Application.Quit();
    }

    // Scene loader
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Back to main panel
    public void BackToMainPanel()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        aboutPanel.SetActive(false);
    }

    // 
    public void SwitchPanelsToAbout()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        aboutPanel.SetActive(true);
    }

    // 
    public void SwitchPanelsToOptions()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
        aboutPanel.SetActive(false);
    }


}
