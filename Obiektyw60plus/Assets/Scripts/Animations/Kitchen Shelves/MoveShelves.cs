using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelves : MonoBehaviour {

    public int ShelvesNum = 14;
    public float ShelfSpeed = 4f;

    private enum MoveDirection { left, right, up, down};
    private int[] MoveDirections;
    private int[] CurrentShelvesPositions;
    GameObject KitchenShelf;
    private bool MoveAgain = true;

    float DistanceHorizontal;
    float DistanceVertical;
    private Vector3 Up;
    private Vector3 Down;
    private Vector3 Right;
    private Vector3 Left;


    // Use this for initialization
    void Start ()
    {
        KitchenShelf = GameObject.Find("salonRegal");
        if (!KitchenShelf) Debug.Log("Can't find salonRegal");

        AssignDistancesBetweenShelves();
        AssignMoveDirections();
    }

    private void AssignDistancesBetweenShelves()
    {

        Transform Shelf1 = KitchenShelf.transform.GetChild(0); //KitchenShelf.transform.Find("box0");//
        Transform Shelf2 = KitchenShelf.transform.GetChild(1);
        Transform Shelf3 = KitchenShelf.transform.GetChild(ShelvesNum - 1);

        DistanceHorizontal = Vector3.Distance(Shelf1.transform.position, Shelf2.transform.position);
        DistanceVertical = Vector3.Distance(Shelf1.transform.position, Shelf3.transform.position);
        Debug.Log("vertical" + DistanceVertical);
        Debug.Log("horizontal" + DistanceHorizontal);

        Up = new Vector3(0, DistanceVertical, 0);
        Down = new Vector3(0, -DistanceVertical, 0);
        Right = new Vector3(DistanceHorizontal, 0, 0);
        Left = new Vector3(-DistanceHorizontal, 0, 0);
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

        if (Input.GetKeyDown("x") && MoveAgain) MoveAgain = !MoveAgain;

        //TODO implement this below (using wand to select shelf)
        //if(IsShelfSelected) if player selected a shelf start moving
        //(selecting a shelf, by clicking index button when wand is highlighting it), it should be set in WandOfMoveFurniture script
        
        //FindClosestShelf() returns shelf index from 0 - 15, returns the shelf closest to player.

        //find which shelf was selected (from CastingToObject) or attach script to each shelf and from this get the selected shelf
        //  selectedObject = GameObject.Find(CastingToObject.selectedObject);
        //  if (!selectedObject)Debug.Log("no object selected");

        //Calculate how many shelves away is the selected shelf from closest shelf
        //Move shelves this many times

            if (MoveAgain)
            {
                MoveAgain = false;
            //move shelves one position

            int i = 0;
            foreach (Transform Shelf in KitchenShelf.transform)
            {
                //Assign the direction to go depending on which shelf we are on
                if(CurrentShelvesPositions[i] == (int)MoveDirection.up)
                {
                    StartCoroutine(MoveShelfUp(Shelf, Up, Shelf.transform.position + Up));
                }
                else if(CurrentShelvesPositions[i] == (int)MoveDirection.down)
                {
                    StartCoroutine(MoveShelfDown(Shelf, Down, Shelf.transform.position + Down));
                }
                else if (CurrentShelvesPositions[i] == (int)MoveDirection.right)
                {
                    StartCoroutine(MoveShelfRight(Shelf, Right, Shelf.transform.position + Right));
                }
                else if (CurrentShelvesPositions[i] == (int)MoveDirection.left)
                {
                    StartCoroutine(MoveShelfLeft(Shelf, Left, Shelf.transform.position + Left));
                }

                i++;
            }

            //assign new values to CurrentPositions array
            //going right (for now one step only)
            //TODO adjust this to more than one step, after it is working for one step
            int temp = CurrentShelvesPositions[0];
            for (int j = 0; j < ShelvesNum - 1; j++)
            {
                CurrentShelvesPositions[j] = CurrentShelvesPositions[j + 1];
            }
            CurrentShelvesPositions[ShelvesNum - 1] = temp;
        }

	}

    int FindClosestShelf()
    {

        return 0;
    }

    IEnumerator MoveShelfLeft(Transform Shelf, Vector3 Direction, Vector3 Destination)
    {
        Debug.Log("coroutine running");
        while (Shelf.transform.position.x > Destination.x )
        { 
            Shelf.GetComponent<Rigidbody>().MovePosition(Shelf.transform.position + ShelfSpeed * Direction * Time.deltaTime);

            yield return new WaitForSeconds(0.05f);
        }
        
    }

    IEnumerator MoveShelfRight(Transform Shelf, Vector3 Direction, Vector3 Destination)
    {
        Debug.Log("coroutine running");
        while (Shelf.transform.position.x < Destination.x)
        {
            Shelf.GetComponent<Rigidbody>().MovePosition(Shelf.transform.position + ShelfSpeed * Direction * Time.deltaTime);

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator MoveShelfUp(Transform Shelf, Vector3 Direction, Vector3 Destination)
    {
        Debug.Log("coroutine running");
        while (Shelf.transform.position.y < Destination.y)
        {
            Shelf.GetComponent<Rigidbody>().MovePosition(Shelf.transform.position + ShelfSpeed * Direction * Time.deltaTime);

            yield return new WaitForSeconds(0.05f);
        }
        //last shelf stopped moving
        MoveAgain = true;
    }

    IEnumerator MoveShelfDown(Transform Shelf, Vector3 Direction, Vector3 Destination)
    {
        Debug.Log("coroutine running");
        while (Shelf.transform.position.y > Destination.y)
        {
            Shelf.GetComponent<Rigidbody>().MovePosition(Shelf.transform.position + ShelfSpeed * Direction * Time.deltaTime);

            yield return new WaitForSeconds(0.05f);
        }
    }



}
