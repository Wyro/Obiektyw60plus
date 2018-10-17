using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureFeaturesDetection : MonoBehaviour {

    private enum MoveDirection{
        left,
        right,
        forward,
        backward
    }

    [Range(0.1f, 4.0f)]
    public float scaleOfTransform = 0.1f;
    [Range(0.1f, 4.0f)]
    public float speedOfMooving = 0.1f;

    private bool isDetected;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //isDetected = DettectedByRay();

        if (isDetected)
        {
            Debug.Log("Detected");

            if (Input.GetKeyDown(KeyCode.O))
            {
                Maximazing();
                Debug.Log("max");
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Minimazing();
                Debug.Log("min");
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                MovingOfObjects(MoveDirection.forward);
                Debug.Log("move forward");
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                MovingOfObjects(MoveDirection.backward);
                Debug.Log("move backward");
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                MovingOfObjects(MoveDirection.left);
                Debug.Log("move left");
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                MovingOfObjects(MoveDirection.right);
                Debug.Log("move right");
            }
            //else if (Input.GetKeyDown(KeyCode.K))
            //{
            //    Destroy(this.gameObject);
            //    Debug.Log("K input");
            //}
        }

        isDetected = false;

    }

    public void DettectedByRay()
    {
        isDetected = true;
    }

    private void Maximazing()
    {
        this.gameObject.transform.localScale += new Vector3(scaleOfTransform, scaleOfTransform, scaleOfTransform);
    }

    private void Minimazing()
    {
        this.gameObject.transform.localScale -= new Vector3(scaleOfTransform, scaleOfTransform, scaleOfTransform);
    }

    private void MovingOfObjects(MoveDirection moveDirection)
    {
        switch (moveDirection)
        {
            case MoveDirection.left:
                this.gameObject.transform.Translate(new Vector3(speedOfMooving, 0.0f, 0.0f));
                break;
            case MoveDirection.right:
                this.gameObject.transform.Translate(new Vector3(-speedOfMooving, 0.0f, 0.0f));
                break;
            case MoveDirection.forward:
                this.gameObject.transform.Translate(new Vector3(0.0f, 0.0f, -speedOfMooving));
                break;
            case MoveDirection.backward:
                this.gameObject.transform.Translate(new Vector3(0.0f, 0.0f, speedOfMooving));
                break;
        }
    }
}
