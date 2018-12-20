using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int currentQuest = 0;
    public Quest[] quests;
    public SpecialQuest[] specialQuests;

    private int pausedQuest;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Progress()
    {
        quests[++currentQuest].StartQuest();
    }

    public void PauseQuest(int need /* frustration or bladder */) // When level of frustration or bladder changes active quest
    {
        pausedQuest = currentQuest;
        specialQuests[need].StartQuest();
    }

    public void RestoreQuest()
    {
        quests[pausedQuest].StartQuest();
    }
}
