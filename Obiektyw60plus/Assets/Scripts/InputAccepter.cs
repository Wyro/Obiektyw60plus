using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAccepter : MonoBehaviour
{
    public Rigidbody cameraRigidbody;

    private void Awake()
    {
        PlayerInput.onTriggerDown += TriggerDown;
        PlayerInput.onTriggerUp += TriggerUp;
    }

    private void TriggerDown()
    {
        print("Trigger Down!");
        cameraRigidbody.AddForce(0, 0, 1000 * Time.deltaTime);
    }

    private void TriggerUp()
    {

    }
}
