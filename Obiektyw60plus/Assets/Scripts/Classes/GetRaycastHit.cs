// Prints the name of the object camera is directly looking at
using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.UIElements.GraphView;
using System.Reflection;

public class GetRaycastHit : MonoBehaviour
{
     Ray ray;
     RaycastHit hit;
     Transform p0 = null;
     Transform p1 = null;
     Vector3 h0;
     Vector3 h1;
     int i = 0;
     Transform container;
     float distance;
     string label = "";
     
     void Start()
     {
         container = new GameObject("Lines").transform;
     }
     
     void Update()
     {
         ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         
         if(Physics.Raycast(ray, out hit))
         {
             if(!p0 && !p1)
             {
                 p0 = hit.transform;
                 h0 = hit.point;
                 return;
             }
             
             if(p0 && !p1)
             {
                 p1 = hit.transform;
                 h1 = hit.point;
             }
                 
             if(p0 && p1)
             {
                 LineRenderer line = new GameObject("Line " + i.ToString()).AddComponent<LineRenderer>();
                 line.transform.parent = container;
                 line.SetWidth(0.025F, 0.025F);
                 line.SetColors(Color.red, Color.green);
                 line.SetVertexCount(2);
                 line.SetPosition(0, h0);
                 line.SetPosition (1, h1);
                 distance = Vector3.Distance(h0, h1);
                 label = "Distance between hit point 0 of " + p0.name + " and hit point 1 of " + p1.name + " = " + distance.ToString();
                 p1 = null;
                 p0 = null;
             }

            Debug.Log("I'm looking at......" + hit.transform.name);
         }

        Debug.Log("Im looking at nothing :(");
     }
}