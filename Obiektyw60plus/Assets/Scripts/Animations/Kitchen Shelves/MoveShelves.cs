using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelves : MonoBehaviour {

    public int ShelvesNum = 16;

    private enum MoveDirection { left, right, up, down};
    private int[] MoveDirections;
    private int[] CurrentShelvesPositions;
    GameObject KitchenShelf;
    

	// Use this for initialization
	void Start ()
    {

        AssignMoveDirections();

        for (int i = 0; i < ShelvesNum; i++) Debug.Log(MoveDirections[i]);

        KitchenShelf = GameObject.Find("salon szafka modułowa przesuwna");
        if (!KitchenShelf) Debug.Log("Can't find salon szafka modułowa przesuwna");

    }

    private void AssignMoveDirections()
    {
        MoveDirections = new int[ShelvesNum];
        CurrentShelvesPositions = new int[ShelvesNum];
        //assigning move directions, starting for the upper row of shelves
        for (int i = 0; i < (ShelvesNum / 2) - 1; i++) MoveDirections[i] = (int)MoveDirection.right;
        MoveDirections[(ShelvesNum / 2) - 1] = (int)MoveDirection.down;
        for (int i = (ShelvesNum / 2); i < ShelvesNum - 1; i++) MoveDirections[i] = (int)MoveDirection.left;
        MoveDirections[ShelvesNum - 1] = (int)MoveDirection.up;

        CurrentShelvesPositions = MoveDirections;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown("x"))
        {
            //move shelves one position
            //assign new values to CurrentPositions array
            //going right (for now one step only)
            //TODO adjust this to more than one step, after it is working for one step
            for (int j = 0; j < ShelvesNum - 1; j++)
            {
                CurrentShelvesPositions[j] = MoveDirections[j + 1];
            }
            CurrentShelvesPositions[ShelvesNum - 1] = MoveDirections[0];

            int i = 0;
            Vector3 DirectionToGo = transform.position + new Vector3(0.07f, 0, 0);
            foreach (Transform Shelf in KitchenShelf.transform)
            {
                //Assign the direction to go depending on which shelf we are on
                if(CurrentShelvesPositions[i] == (int)MoveDirection.up)
                {
                    DirectionToGo = Shelf.transform.position + new Vector3(0, 0, 0.08f);
                }
                else if(CurrentShelvesPositions[i] == (int)MoveDirection.down)
                {
                    DirectionToGo = Shelf.transform.position + new Vector3(0, 0, -0.08f);
                }
                else if (CurrentShelvesPositions[i] == (int)MoveDirection.right)
                {
                    DirectionToGo = Shelf.transform.position + new Vector3(0.07f, 0, 0);
                }
                else if (CurrentShelvesPositions[i] == (int)MoveDirection.left)
                {
                    DirectionToGo = Shelf.transform.position + new Vector3(-0.07f, 0, 0);
                }

                //Move shelf in this direction
                //TODO start coroutine for each shelf, where the shelf will be moving to its destination
                Shelf.GetComponent<Rigidbody>().MovePosition(DirectionToGo);
                i++;
            }
        }


	}




}
