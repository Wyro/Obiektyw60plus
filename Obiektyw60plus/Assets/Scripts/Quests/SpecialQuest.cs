using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpecialQuest : Quest
{
    public new void StartQuest()
    {
        completed = false;
        // Display text
        // Update notebook
        Task();
        // Highlight objects
    }

    public new void EndQuest()
    {
        completed = true;
        qm.RestoreQuest();
    }
}