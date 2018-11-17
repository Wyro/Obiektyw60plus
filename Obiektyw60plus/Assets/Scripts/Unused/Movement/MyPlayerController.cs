using UnityEngine.EventSystems;
using UnityEngine;


public class MyPlayerController : MonoBehaviour {



    EquipmentManager equipmentManager;



	// Use this for initialization
	void Start () {

       equipmentManager = EquipmentManager.instance;
        //TODO find currently grabbed object
    }
	
	// Update is called once per frame
	void Update () {

        

        //check if we are hovering over UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }



        //left mouse button
        if (Input.GetKeyDown(KeyCode.G))
        {
            //print("dropping"+ equipmentManager.ActiveItem.name);
            ////Unequip the item, if it is equipped
            //if (equipmentManager.IsItemEquipped)
            //{
            //    //deactivate script, which the item uses
            //    equipmentManager.ToggleScript(equipmentManager.ActiveItem, false);
            //    //TODO change item's position to previous, use WandHolder class
            //    equipmentManager.UnEquipActiveItem();
            //    //activate ItemPickup's script
            //    GameObject pickUp = GameObject.Find(equipmentManager.ActiveItem.name);
            //    pickUp.GetComponent<ItemPickup>().enabled = true;
            //}
            //else
            //{
            //    print("nothing to unequip");
            //}
            //equipmentManager.IsItemEquipped = false;
        }
            //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit hit;

            //    if(Physics.Raycast(ray, out hit, 100, movementMask))
            //    {
            //        Debug.Log("we hit" + hit.collider.name + " " + hit.point);
            //        //Move our player to what we hit
            //        motor.MoveToPoint(hit.point);
            //        //Stop focusing any objects
            //        RemoveFocus();
            //    }
            //}
            //right mouse button
        //    if (Input.GetMouseButtonDown(1))
        //{
        //    if (!cam)
        //    {
        //        Debug.Log("no cam");
        //    }
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, 100))
        //    {
        //        //Debug.Log("we hit" + hit.collider.name + " " + hit.point);
        //        Interactable interactable = hit.collider.GetComponent<Interactable>();
        //        //Check if we hit an interractable
        //        if(interactable != null)
        //        {
        //            SetFocus(interactable);
        //        }
        //    }

        //}




    }
    //void SetFocus(Interactable newInteractable)
    //{
    //    //if we have a new focus
    //    if(newInteractable != focus)
    //    {
    //        if(focus != null) focus.OnDefocused();
    //        focus = newInteractable;
    //        //motor.FollowTarget(newFocus);
    //    }

    //    newInteractable.OnFocused(transform);

    //}

    //void RemoveFocus()
    //{
    //    if (focus != null) focus.OnDefocused();
    //    focus = null;
    //    //motor.stopFollowingTarget();
    //}
}
