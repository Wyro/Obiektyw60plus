using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles simple steering of the wheelchair via keyboard or VR controllers
/// Currently only reads input from left VR controller
public class wheelchair_movement : MonoBehaviour {

    // Parameters, editable in the component view
    public float movement_acceleration; // rate of gaining movement speed, 1 is good
    public float max_movement_speed; // terminal velocity of wheelchair, 2 is good
    public float movement_decceleration; // rate of losing movement speed on its own, 2.5 is good
    public float rotation_acceleration; // rate of gaining turning speed, 10 is good
    public float max_rotation_speed; // maximum turning rate of wheelchair, 20 is good
    public float rotation_decceleration; // rate of losing turning speed on its own, 25 is good

    public GameObject Remote; // remote controlling the wheelchair

    // Variables for holding user input
    private float movement_input;
    private float rotation_input;
    private float break_input;

    // Variables for holding speed
    private float movement_speed = 0; // rate of forward/backwards motion
    private float rotation_speed = 0; // rate of left/right rotation

    private Rigidbody rb; // Unity's physics component
    private DistanceGrabbable dg; // Distance Grabbable script

    // Awake is run once, when game starts
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        dg = Remote.GetComponent<DistanceGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        // If Remote is in hand, get user input
        if (dg.isGrabbed)
        {
            movement_input = Input.GetAxis("Vertical"); // W, S; moving joystick up or down
            rotation_input = Input.GetAxis("Horizontal"); // A, D; moving joystick left or right
            break_input = Input.GetAxis("Break"); // space
        }
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
        movement_speed = movement_speed + movement_input * movement_acceleration * Time.deltaTime; // when forward/backward motion button is pressed speed is increased at a rate controlled by acceleration variable
        if (movement_speed > max_movement_speed) movement_speed = max_movement_speed; // caps the speed at max speed

        // when forward/backward motion button is not pressed speed is decreased at rate controlled by decceleration variable
        if (movement_input == 0)
        {
            // speed can be forward or backward
            if (movement_speed > 0)
            {
                movement_speed = movement_speed - movement_decceleration * Time.deltaTime;
                if (movement_speed < 0) movement_speed = 0; // caps decreasing speed on 0
            }
            if (movement_speed < 0)
            {
                movement_speed = movement_speed + movement_decceleration * Time.deltaTime;
                if (movement_speed > 0) movement_speed = 0; // caps decreasing speed on 0
            }
        }

        // break button stops motion faster
        if (movement_input == 0)
        {
            // speed can be forward or backward
            if (movement_speed > 0)
            {
                movement_speed = movement_speed - 5 * movement_decceleration * Time.deltaTime;
                if (movement_speed < 0) movement_speed = 0; // caps decreasing speed on 0
            }
            if (movement_speed < 0)
            {
                movement_speed = movement_speed + 5 * movement_decceleration * Time.deltaTime;
                if (movement_speed > 0) movement_speed = 0; // caps decreasing speed on 0
            }
        }

        Vector3 movement; // vector of forward/backwards motion
        movement = transform.forward * movement_speed * Time.deltaTime; // Time.deltaTime allows for parameter to be speed in meters per second instead of meters per frame

        rb.MovePosition(rb.position + movement); // transforms object's position
    }

    // Left/right turning
    private void Turn()
    {
        rotation_speed = rotation_speed + rotation_input * rotation_acceleration * Time.deltaTime; // when rotation button is pressed rotation speed is increased at a rate controlled by acceleration variable
        if (rotation_speed > max_rotation_speed) rotation_speed = max_rotation_speed; // caps the speed at max speed

        // when rotation button is not pressed speed is decreased at rate controlled by decceleration variable
        if (rotation_input == 0)
        {
            // speed can be left or right
            if (rotation_speed > 0)
            {
                rotation_speed = rotation_speed - rotation_decceleration * Time.deltaTime;
                if (rotation_speed < 0) rotation_speed = 0; // caps decreasing speed on 0
            }
            if (rotation_speed < 0)
            {
                rotation_speed = rotation_speed + rotation_decceleration * Time.deltaTime;
                if (rotation_speed > 0) rotation_speed = 0; // caps decreasing speed on 0
            }
        }

        // break button stops motion faster
        if (rotation_input == 0)
        {
            // speed can be left or right
            if (rotation_speed > 0)
            {
                rotation_speed = rotation_speed - 5 * rotation_decceleration * Time.deltaTime;
                if (rotation_speed < 0) rotation_speed = 0; // caps decreasing speed on 0
            }
            if (rotation_speed < 0)
            {
                rotation_speed = rotation_speed + 5 * rotation_decceleration * Time.deltaTime;
                if (rotation_speed > 0) rotation_speed = 0; // caps decreasing speed on 0
            }
        }

        float turn = rotation_speed * Time.deltaTime; // Time.deltaTime allows for parameter to be speed in meters per second instead of meters per frame

        Quaternion rotation; // quaternion of left/right rotation
        rotation = Quaternion.Euler(0f, turn, 0f); // we can only turn left/right, rotating around the Y axis

        rb.MoveRotation(rb.rotation * rotation); // transforms objects' rotation
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Floor")
        {
            //movement_speed = 0;
            //rotation_speed = 0;
        }
    }
}
