using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject aboutPanel;
    private AudioSource audioSource;
    public GameObject pauseCanvas;
    public GameObject playerController;

	// Use this for initialization
	void Start () {
        //mainPanel.SetActive(true);
        //optionsPanel.SetActive(false);
        //aboutPanel.SetActive(false);
        audioSource = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            Vector3 playerPosition = (playerController.transform.forward * 1.0f) + playerController.transform.position;
            //pauseCanvas.transform.position = new Vector3(0, 1.2f, 0);
            pauseCanvas.transform.position = playerPosition;
            pauseCanvas.transform.rotation = playerController.transform.rotation;
            audioSource.Play();
        }

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
