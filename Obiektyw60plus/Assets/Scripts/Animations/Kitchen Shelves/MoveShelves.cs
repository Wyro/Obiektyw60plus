using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelves : MonoBehaviour {

    public int ShelvesNum = 14;
    public float ShelfSpeed = 4f;
    public bool IsShelfSelected = false;

    private enum MoveDirection { left, right, up, down};
    private int[] MoveDirections;
    private int[] CurrentShelvesPositions;
    private GameObject KitchenShelf;
    private GameObject Player;

    private bool MoveAgain = true; //true means shelves stopped moving, and we can move again
    private bool DistanceFound = false; //true means we found distance between shelf and player
    private bool GoingRight = true; //shelves are moving right or left depending on which way is faster
    private int NumShelvesAway = 5; //5 for testing


    private float DistanceHorizontal;
    private float DistanceVertical;
    private Vector3 Up;
    private Vector3 Down;
    private Vector3 Right;
    private Vector3 Left;


    // Use this for initialization
    void Start ()
    {
        KitchenShelf = GameObject.Find("salonRegal");
        if (!KitchenShelf) Debug.Log("Can't find salonRegal");

        Player = GameObject.Find("TempPlayer"); //TODO change this to Player when done testing
        if (!Player) Debug.Log("Can't find TempPlayer"); //TODO change this also

        AssignDistancesBetweenShelves();
    }

    private void AssignDistancesBetweenShelves()
    {

        Transform Shelf1 = KitchenShelf.transform.GetChild(0); //KitchenShelf.transform.Find("box0");//
        Transform Shelf2 = KitchenShelf.transform.GetChild(1);
        Transform Shelf3 = KitchenShelf.transform.GetChild(ShelvesNum - 1);

        DistanceHorizontal = Vector3.Distance(Shelf1.transform.position, Shelf2.transform.position);
        DistanceVertical = Vector3.Distance(Shelf1.transform.position, Shelf3.transform.position);

        Up = new Vector3(0, DistanceVertical, 0);
        Down = new Vector3(0, -DistanceVertical, 0);
        Right = new Vector3(DistanceHorizontal, 0, 0);
        Left = new Vector3(-DistanceHorizontal, 0, 0);
    }

    private void AssignMoveDirections(bool GoingRight)
    {
        MoveDirections = new int[ShelvesNum];
        CurrentShelvesPositions = new int[ShelvesNum];
        //assigning move directions, starting for the upper row of shelves
        if (GoingRight)
        {
            for (int i = 0; i < (ShelvesNum / 2) - 1; i++) MoveDirections[i] = (int)MoveDirection.right;
            MoveDirections[(ShelvesNum / 2) - 1] = (int)MoveDirection.down;
            for (int i = (ShelvesNum / 2); i < ShelvesNum - 1; i++) MoveDirections[i] = (int)MoveDirection.left;
            MoveDirections[ShelvesNum - 1] = (int)MoveDirection.up;
        }
        else //negative speed
        {
            MoveDirections[0] = (int)MoveDirection.down;
            for (int i = 1; i < (ShelvesNum / 2); i++) MoveDirections[i] = (int)MoveDirection.left;
            MoveDirections[(ShelvesNum / 2)] = (int)MoveDirection.up;
            for (int i = (ShelvesNum / 2)+1; i < ShelvesNum; i++) MoveDirections[i] = (int)MoveDirection.right;
        }

        CurrentShelvesPositions = MoveDirections;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown("x"))
        {
            IsShelfSelected = true;
            NumShelvesAway = 5;
        }

        //if(IsShelfSelected) if player selected a shelf start moving
        //(selecting a shelf, by clicking index button when wand is highlighting it), it should be set in WandOfMoveFurniture script

        if (IsShelfSelected)
        {
            //Assign distance in number of shelves and calculate which way to go, right or left
            if (!DistanceFound) 
            {
                //NumShelvesAway = FindClosestShelf() - FindSelectedObject(); //TODO uncomment when done testing
                //if (NumShelvesAway < 0) NumShelvesAway = ShelvesNum + NumShelvesAway; //TODO uncomment when done testing
                //Depending on the distance assign move directions
                if (NumShelvesAway > ShelvesNum / 2)
                {
                    NumShelvesAway = ShelvesNum - NumShelvesAway;
                    GoingRight = false;
                }
                else
                {
                    GoingRight = true; 
                }

                AssignMoveDirections(GoingRight); //true means going right 
                DistanceFound = true;  
            }
            
            //Move shelves one position right or left
            if(MoveAgain)
            {
                MoveAgain = false;

                if (NumShelvesAway == 1)
                {
                    IsShelfSelected = false;
                    MoveAgain = true;
                    DistanceFound = false;
                }

                int i = 0;
                foreach (Transform Shelf in KitchenShelf.transform)
                {
                    //Assign the direction to go depending on which shelf we are on
                    if (CurrentShelvesPositions[i] == (int)MoveDirection.up)
                    {
                        StartCoroutine(MoveShelfUp(Shelf, Up, Shelf.transform.position + Up));
                    }
                    else if (CurrentShelvesPositions[i] == (int)MoveDirection.down)
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
                if (GoingRight) ChangeTableRightMove();
                else ChangeTableLeftMove();
            }
        }

	}

    private void ChangeTableRightMove()
    {
        //going right one step
        int temp = CurrentShelvesPositions[0];
        for (int j = 0; j < ShelvesNum - 1; j++)
        {
            CurrentShelvesPositions[j] = CurrentShelvesPositions[j + 1];
        }
        CurrentShelvesPositions[ShelvesNum - 1] = temp;
    }

    private void ChangeTableLeftMove()
    {
        //going left one step
        int temp = CurrentShelvesPositions[ShelvesNum - 1];
        for (int j = ShelvesNum - 1; j > 0; j--)
        {
            CurrentShelvesPositions[j] = CurrentShelvesPositions[j - 1];
        }
        CurrentShelvesPositions[0] = temp;
    }

    private int FindSelectedObject()
    {
        int Index = 0;
        //GameObject selectedObject = GameObject.Find(CastingToObject.selectedObject); //TODO uncomment when done testing
        //if (!selectedObject) Debug.Log("no object selected"); //TODO uncomment when done testing

        string name = "box13"; //TODO comment when done testing
        string IndexStr = name.Substring(3); //TODO comment when done testing

        //string IndexStr = selectedObject.name.Substring(3); //TODO uncomment when done testing

        if (!Int32.TryParse(IndexStr, out Index))
        {
            Index = -1;
        }

        return Index;
    }

    //Finds the shelf with min distance to player, returns it's index 0 - 13
    int FindClosestShelf()
    {
        float MinDist = Vector3.Distance(Player.transform.position, KitchenShelf.transform.GetChild(0).position);
        int MinIndex = 0;
        int i = 0;
        foreach (Transform Shelf in KitchenShelf.transform)
        {
            if(Vector3.Distance(Player.transform.position, Shelf.transform.position) < MinDist)
            {
                MinDist = Vector3.Distance(Player.transform.position, Shelf.transform.position);
                MinIndex = i;
            }
            i++;
        }

            return MinIndex;
    }

    IEnumerator MoveShelfLeft(Transform Shelf, Vector3 Direction, Vector3 Destination)
    {
        while (Shelf.transform.position.x > Destination.x )
        { 
            Shelf.GetComponent<Rigidbody>().MovePosition(Shelf.transform.position + ShelfSpeed * Direction * Time.deltaTime);

            yield return new WaitForSeconds(0.05f);
        }
        
    }

    IEnumerator MoveShelfRight(Transform Shelf, Vector3 Direction, Vector3 Destination)
    {
        while (Shelf.transform.position.x < Destination.x)
        {
            Shelf.GetComponent<Rigidbody>().MovePosition(Shelf.transform.position + ShelfSpeed * Direction * Time.deltaTime);

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator MoveShelfUp(Transform Shelf, Vector3 Direction, Vector3 Destination)
    {
        while (Shelf.transform.position.y < Destination.y)
        {
            Shelf.GetComponent<Rigidbody>().MovePosition(Shelf.transform.position + ShelfSpeed * Direction * Time.deltaTime);

            yield return new WaitForSeconds(0.05f);
        }
        //last shelf stopped moving
        MoveAgain = true;
        NumShelvesAway--;
    }

    IEnumerator MoveShelfDown(Transform Shelf, Vector3 Direction, Vector3 Destination)
    {
        while (Shelf.transform.position.y > Destination.y)
        {
            Shelf.GetComponent<Rigidbody>().MovePosition(Shelf.transform.position + ShelfSpeed * Direction * Time.deltaTime);

            yield return new WaitForSeconds(0.05f);
        }
    }

}
