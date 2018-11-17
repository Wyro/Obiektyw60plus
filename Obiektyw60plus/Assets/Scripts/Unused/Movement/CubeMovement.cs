using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour {

    public float moveSpeed;
    private float maxSpeed;

    //vector3 is x,y,z coordinate
    private Vector3 input;
    //player spawn point
    private Vector3 spawn;
    private Rigidbody rigidBody;
    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        moveSpeed = 0.5f;
        maxSpeed = 3f;
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector3(-1*Input.GetAxis("Horizontal"), 0, -1*Input.GetAxis("Vertical"));
        //keep accelerating until reaching certain speed
        if (rigidBody.velocity.magnitude < maxSpeed)
        {
            rigidBody.AddForce(input * moveSpeed, ForceMode.VelocityChange);
        }


    }

    void OnTriggerEnter(Collider other)
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "BelowFloor")
        {
            Die();
        }

    }

    void Die()
    {
        transform.position = spawn;
    }
}
