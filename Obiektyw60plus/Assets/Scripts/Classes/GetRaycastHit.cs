// Prints the name of the object camera is directly looking at
using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.UIElements.GraphView;   
using System.Collections.Generic;

    
public class GetRaycastHit : MonoBehaviour
{
    private bool canHover = false;
    public float distanceOfRay = (float)1.0;
    //public GameObject pickupObjcet;
    //public List<GameObject> pickupObjects;

    void Start()
    {
        //pickupObjects = new List<GameObject>();
    }

    void Update()
    {
        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        Ray ray;

        ray = new Ray(transform.position, transform.forward * distanceOfRay);
        Debug.DrawRay(transform.position, transform.forward *distanceOfRay, Color.red);

        if (Physics.Raycast(ray, out hit)) //(Physics.Raycast(transform.position, transform.forward, out hit, (float)2))
        {
            
            if (hit.distance <= 3.0 && hit.collider.gameObject.tag == "pickup")
            {
                //Debug.Log("Nacisnij \"Space\"");

                hit.transform.SendMessage("DettectedByRay");
                canHover = true;
                //if(Input.GetKeyDown(KeyCode.Space))
                //{
                //    //Destroy(pickupObjcet);
                //}
            }

            else
            {
                canHover = false;
            }
        }
    }

    private void OnGUI()
    {
        if(canHover == true)
        {
            string stringContent = "'O' - maximalizing" + System.Environment.NewLine + "'P' - minimalizing" + System.Environment.NewLine + "'U, J, K, L' - moving";
            GUIContent guiContent = new GUIContent(stringContent);
            GUIStyle guiStyle = new GUIStyle();
            GUI.Box(UnityEngine.Rect.MinMaxRect(Screen.width / 2 - 100, Screen.height / 2 - 100, 20, 200), stringContent, GUIStyle.none);
        }
        
    }

}