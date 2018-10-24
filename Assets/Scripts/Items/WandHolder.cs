using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandHolder : MonoBehaviour {

    public bool[] IsSlotOccupied;
    public int SlotsNum = 5;

    Collider equipmentSpace;

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

    void OnTriggerEnter(Collider otherCollider)
    {
        int firstEmpty = CheckFirstEmptySlot();
        if (firstEmpty != SlotsNum)
        {
            IsSlotOccupied[firstEmpty] = true;
        }
        DistanceGrabbable dg = otherCollider.GetComponentInChildren<DistanceGrabbable>();
        if (dg)
        {
            dg.InRange = true;
        }

    }

    void OnTriggerExit(Collider otherCollider)
    {

    }

    public int OccupyFirstEmptySlot(GameObject gameObject)
    {
        for (int i = 0; i < SlotsNum; i++)
        {
            if (IsSlotOccupied[i] == false)
            {
                GameObject wandHolderChild = transform.GetChild(i).gameObject;
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
