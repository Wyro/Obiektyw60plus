using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCastSystem : MonoBehaviour
{

    private RaycastHit hit_Info;
    public Canvas colorMenu;
    public Canvas lampSwitch;
    public Button redButton;
    public Button blueButton;
    public Button greenButton;
    public Button yellowButton;
    public Button whiteButton;
    public Button blackButton;

    // Use this for initialization
    void Start()
    {
        redButton.onClick.AddListener(delegate { ChangeColor(Color.red); });
        blueButton.onClick.AddListener(delegate { ChangeColor(Color.blue); });
        greenButton.onClick.AddListener(delegate { ChangeColor(Color.green); });
        yellowButton.onClick.AddListener(delegate { ChangeColor(Color.yellow); });
        whiteButton.onClick.AddListener(delegate { ChangeColor(Color.white); });
        blackButton.onClick.AddListener(delegate { ChangeColor(Color.black); });
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 5);

        Ray ray = new Ray(this.transform.position, this.transform.forward);

        if (Physics.Raycast(ray, out hit_Info, 10f))
        {
            Debug.Log("wall");
            if (hit_Info.collider.tag == "Wall")
            {
                Debug.Log(hit_Info.collider);
                colorMenu.gameObject.SetActive(true);
            }

            if (hit_Info.collider.tag == "Lamp")
            {
                Debug.Log(hit_Info.collider);
                lampSwitch.gameObject.SetActive(true);
            }
        }
    }

    void ChangeColor(Color color)
    {
        hit_Info.transform.gameObject.GetComponent<Renderer>().material.color = hit_Info.transform.gameObject.GetComponent<Renderer>().material.color = color;
        //hit_Info.transform.gameObject.GetComponent<Renderer>().material.color = Color.LerpUnclamped(hit_Info.transform.gameObject.GetComponent<Renderer>().material.color, color, Mathf.PingPong(Time.time * 0.005f, 1));
    }
}

