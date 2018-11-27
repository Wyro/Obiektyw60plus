using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCastSystem : MonoBehaviour
{
    private RaycastHit hit_Info;
    public Canvas Menu;
    public GameObject lampSwitch;
    public Button onOffButton;
    public Button redButton;
    public Button blueButton;
    public Button greenButton;
    public Button yellowButton;
    public Button whiteButton;
    public Button blackButton;
    public bool MenuIsActive;

    // Use this for initialization
    void Start()
    {
        redButton.onClick.AddListener(delegate { UIManager(Color.red); });
        blueButton.onClick.AddListener(delegate { UIManager(Color.blue); });
        greenButton.onClick.AddListener(delegate { UIManager(Color.green); });
        yellowButton.onClick.AddListener(delegate { UIManager(Color.yellow); });
        whiteButton.onClick.AddListener(delegate { UIManager(Color.white); });
        blackButton.onClick.AddListener(delegate { UIManager(Color.black); });
        onOffButton.onClick.AddListener(UIManager);
        MenuIsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 5);

        Ray ray = new Ray(this.transform.position, this.transform.forward);

        if (Physics.Raycast(ray, out hit_Info, 10f))
        {
                if (!MenuIsActive)
                    ToggleMenu();
        }
        else
        {
            if (MenuIsActive)
                ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        Menu.gameObject.SetActive(!Menu.gameObject.activeSelf);
        MenuIsActive = !MenuIsActive;
    }

    void UIManager(Color color)
    {
        if(hit_Info.collider.tag == "Wall")
            hit_Info.transform.gameObject.GetComponent<Renderer>().material.color = hit_Info.transform.gameObject.GetComponent<Renderer>().material.color = color;
        else
            hit_Info.transform.gameObject.GetComponent<ChangeLight>().SwitchLight(color);
    }

    void UIManager()
    {
            hit_Info.transform.gameObject.GetComponent<ChangeLight>().SwitchLight();
    }

}

