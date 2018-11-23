using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingToObject : MonoBehaviour {


    public static string selectedObject;
    public string internalObject;
    public RaycastHit theObject;

    HighlightSelected lastHighlighted = null;
    Transform tempObject;
    bool objectChanged = false;
    bool firstTime = true;

    // Use this for initialization
    void Start () {
        tempObject = transform;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.forward*100f, Color.blue);

	    if(Physics.Raycast(transform.position, transform.forward, out theObject))
        {
            
            selectedObject = theObject.transform.gameObject.name;
            internalObject = theObject.transform.gameObject.name;

            if (tempObject.transform.gameObject.name != theObject.transform.gameObject.name)
            {
                print("raycast hit");
                print(selectedObject);

                objectChanged = !objectChanged;

                if(firstTime)
                    lastHighlighted = theObject.transform.gameObject.GetComponent<HighlightSelected>();
                else
                    lastHighlighted = tempObject.gameObject.GetComponent<HighlightSelected>();

                if (lastHighlighted)
                {
                    print("changing object");
                    lastHighlighted.rayCasted = objectChanged;
                    firstTime = false;
                }

                tempObject = theObject.transform;
            }

        }
	}
}
