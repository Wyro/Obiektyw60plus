using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour
{
    public bool enter = false;
    public GameObject enterTargetPlace;
    public bool pickUp = false;
    public GameObject pickUpTarget;
    public GameObject[] highlightedGameObjects;
    public string text;
    public bool completed = false;

    public QuestManager qm;

    public void StartQuest()
    {
        // Display text
        // Update notebook
        Task();
        // Highlight objects
    }

    public void Task()
    {
        if (enter)
        {
            EndQuest();
        }

        if (pickUp)
        {
            EndQuest();
        }
    }

    public void EndQuest()
    {
        completed = true;
        qm.Progress();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
