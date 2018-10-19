using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandHolder : MonoBehaviour {

    public bool[] IsSlotOccupied;
    public int SlotsNum = 5;

    // Use this for initialization
    void Start()
    {
        IsSlotOccupied = new bool[SlotsNum];
        for (int i = 0; i < SlotsNum; i++)
        {
            IsSlotOccupied[i] = false;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}

    public int OccupyFirstEmptySlot(GameObject gameObject)
    {
        for (int i = 0; i < SlotsNum; i++)
        {
            if (IsSlotOccupied[i] == false)
            {
                GameObject wandHolderChild = transform.GetChild(i).gameObject;
                gameObject.transform.position = wandHolderChild.transform.position;
                Vector3 rotate90 = new Vector3(0, 0, 90f);
                gameObject.transform.Rotate(rotate90); 
                return i;
                
            }
        }
        return SlotsNum; //all slots occupied
    }

    public int CheckFirstEmptySlot()
    {
        for (int i = 0; i < SlotsNum; i++)
        {
            if (IsSlotOccupied[i] == false)
            {
                return i;
            }
        }
        return SlotsNum; //all slots occupied
    }

}
