using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelves : MonoBehaviour {

    public int ShelvesNum = 16;

    private enum MoveDirection { left, right, up, down};
    private int[] MoveDirections;
    private int[] CurrentShelvesPositions;
    private GameObject KitchenShelf;

    private Vector3 Up = new Vector3(0, 0.73f, 0);
    private Vector3 Down = new Vector3(0, -0.73f, 0);
    private Vector3 Right = new Vector3(0.7f, 0, 0);
    private Vector3 Left = new Vector3(-0.7f, 0, 0);


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

        //TODO stop the coroutines some day
	}

    IEnumerator MoveShelfLeft(Transform Shelf, Vector3 Direction, Vector3 Destination)
    {
        while (Shelf.transform.position.x > Destination.x )
        {
            Shelf.GetComponent<Rigidbody>().MovePosition(Shelf.transform.position + Direction * Time.deltaTime);

            yield return new WaitForSeconds(0.005f);
        }
    }

    IEnumerator MoveShelfRight(Transform Shelf, Vector3 Direction, Vector3 Destination)
    {
        while (Shelf.transform.position.x < Destination.x)
        {
            Shelf.GetComponent<Rigidbody>().MovePosition(Shelf.transform.position + Direction * Time.deltaTime);

            yield return new WaitForSeconds(0.005f);
        }
    }

    IEnumerator MoveShelfUp(Transform Shelf, Vector3 Direction, Vector3 Destination)
    {
        while (Shelf.transform.position.y < Destination.y)
        {
            Shelf.GetComponent<Rigidbody>().MovePosition(Shelf.transform.position + Direction * Time.deltaTime);

            yield return new WaitForSeconds(0.005f);
        }
    }

    IEnumerator MoveShelfDown(Transform Shelf, Vector3 Direction, Vector3 Destination)
    {
        while (Shelf.transform.position.y > Destination.y)
        {
            Shelf.GetComponent<Rigidbody>().MovePosition(Shelf.transform.position + Direction * Time.deltaTime);

            yield return new WaitForSeconds(0.005f);
        }
    }



}
