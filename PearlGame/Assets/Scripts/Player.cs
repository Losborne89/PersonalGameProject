using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float horizontalInput;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpVelocity = 5f;

    [SerializeField] private Rigidbody rigidbodyComponent;
    [SerializeField] private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Direction of movement based on input
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f);

        // Apply force to the player's rigidbody
        rigidbodyComponent.AddForce(movement * speed, ForceMode.Force);
        
        // Jump with Space Key
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbodyComponent.velocity = Vector3.up * jumpVelocity;
        }
    }

    // Detect collision with ground
    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    // Detect collision exit with the ground
    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
