using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject aboutPanel;
    private AudioSource audioSource;
    public GameObject pauseCanvas;
    public GameObject playerController;
<<<<<<< HEAD

	// Use this for initialization
	void Start () {
        //mainPanel.SetActive(true);
        //optionsPanel.SetActive(false);
        //aboutPanel.SetActive(false);
        audioSource = gameObject.GetComponent<AudioSource>();
=======

    public Toggle toggleYellowing;
    

    // Use this for initialization
    void Start() {
        BackToMainPanel();

        toggleYellowing.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggleYellowing);
        });

>>>>>>> AleksandrraKotula
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
<<<<<<< HEAD
    }
=======
        confirmQuitPanel.SetActive(false);
    }

>>>>>>> AleksandrraKotula

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
