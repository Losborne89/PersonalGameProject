using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float minGroundedDistance = 0.2f;

    [SerializeField] private Rigidbody rigidbodyComponent;
    [SerializeField] private bool isGrounded = false;
    
    [SerializeField] private Transform rayStartLocation;
    


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

        // Shoots a ray down and on collision, ouput raycast hit and returns true
        if (Physics.Raycast(rayStartLocation.position, Vector3.down, out RaycastHit hit))
        {
          
            if (hit.distance < minGroundedDistance)
            {
                isGrounded = true;
            }

        }

        // Jump when grounded and Space Key pressed
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbodyComponent.velocity = Vector3.up * jumpVelocity;
        }


    }
   
}
