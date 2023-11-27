using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float minGroundedDistance = 0.2f;

    [SerializeField] private Rigidbody rigidbodyComponent;
    [SerializeField] private bool isGrounded = false;

    [SerializeField] private bool canDoubleJump = false;
    [SerializeField] private FoodBarInteraction foodBarInteraction;

    [SerializeField] private Transform rayStartLocation;

    [SerializeField] private AudioSource audioSource2;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //move right
        if(Input.GetKey(KeyCode.D))
        {
            rigidbodyComponent.AddForce(Vector3.right * speed * Time.deltaTime);
        }
        //move left
        if (Input.GetKey(KeyCode.A))
        {
            rigidbodyComponent.AddForce(Vector3.left * speed * Time.deltaTime);
        }
        
        // Sets grounded to false
        isGrounded = false;

        // Shoots a ray down and on collision, output raycast hit and returns true
        if (Physics.Raycast(rayStartLocation.position, Vector3.down, out RaycastHit hit))
        {
          
            if (hit.distance < minGroundedDistance)
            {
                isGrounded = true;
            }

        }

        // Double Jump when grounded, Space Key pressed and slider value at full

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {

            rigidbodyComponent.velocity = Vector3.up * jumpVelocity;
            audioSource2.Play();

            // Double jump after regular jump
            canDoubleJump = true;


        }
        else
        {
            if (foodBarInteraction.GetSliderValue() == 10)
            {
                if (canDoubleJump && Input.GetKeyDown(KeyCode.Space))
                {

                    rigidbodyComponent.velocity = Vector3.up * jumpVelocity;
                    audioSource2.Play();

                    // Stop Double jump until grounded
                    canDoubleJump = false;

                }
            }
        }

        
        


    }
   
}
