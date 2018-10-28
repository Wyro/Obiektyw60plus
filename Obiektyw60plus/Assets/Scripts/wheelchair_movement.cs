using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles simple steering of the wheelchair via keyboard or VR controllers
/// Currently only reads input from left VR controller
public class wheelchair_movement : MonoBehaviour {

    // Parameters, editable in the component view
    public float speed; // rate of forward/backwards motion, 1.25 is good
    public float maneuverability; // rate of left/right rotation, 20 is good

    private Rigidbody rb; // Unity's physics component

    // Variables for holding user input
    private float movement_input;
    private float turn_input; 

    // Awake is run once, when game starts
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get user input
        movement_input = Input.GetAxis("Vertical"); // W, S; moving joystick up or down
        turn_input = Input.GetAxis("Horizontal"); // A, D; moving joystick left or right
    }

    // FixedUpdate is called once per frame to calculate the physics
    void FixedUpdate()
    {
        Move();
        Turn();
    }

    // Forward/backwards motion
    private void Move()
    {
        Vector3 movement; // vector of forward/backwards motion
        movement = transform.forward * movement_input * speed * Time.deltaTime; // Time.deltaTime ensures fluid motion

        rb.MovePosition(rb.position + movement); // transforms object's position
    }

    // Left/right turning
    private void Turn()
    {
        float turn = turn_input * maneuverability * Time.deltaTime; // Time.deltaTime ensures fluid motion

        Quaternion rotation; // quaternion of left/right rotation
        rotation = Quaternion.Euler(0f, turn, 0f); // we can only turn left/right, rotating around the Y axis

        rb.MoveRotation(rb.rotation * rotation); // transforms objects' rotation
    }
}
