using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const int MaxHorizontalVelocity = 5;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float minGroundedDistance = 0.2f;

    [SerializeField] private Rigidbody rigidbodyComponent;
    [SerializeField] private bool isGrounded = false;

    [SerializeField] private bool hasJumped = false;
    [SerializeField] private FoodBarInteraction foodBarInteraction;

    [SerializeField] private Transform rayStartLocation;

    [SerializeField] private AudioSource audioSource2;

    [SerializeField] public bool canMove = true;
    [SerializeField] public bool isMovingTooFast = true;

    [SerializeField] private Animator animator;
    [SerializeField] private bool isFacingRight = true;

    public void OnEnable()
    {
        //register the event with + 
        InputManager.OnMoveRight += HandleOnMoveRight;
        InputManager.OnMoveLeft += HandleOnMoveLeft;
        InputManager.OnJump += HandleOnJump;
    }

    public void OnDisable()
    {
        //Deregister the event with -
        InputManager.OnMoveRight -= HandleOnMoveRight;
        InputManager.OnMoveLeft -= HandleOnMoveLeft;
        InputManager.OnJump -= HandleOnJump;
    }

    private void HandleOnMoveRight()
    {
        if (canMove == false)
        {
            return;
        }

        if (isMovingTooFast == false)
        {
            //move right
            Move(Vector3.right, true);

            //limit velocity
            CheckMoveSpeed();

        }
    }

    private void HandleOnMoveLeft()
    {
        if (canMove == false)
        {
            return;
        }

        if (isMovingTooFast == false)
        {
            //move left
            Move(Vector3.left, false);

            //limit velocity
            CheckMoveSpeed();
        }
    }

    private void HandleOnJump()
    {
        if(canMove == false)
        {
            return;
        }
        if (isGrounded)
        {
            Jump();

            // Double jump after regular jump
            hasJumped = true;
        }
        else
        {
            // Double Jump when grounded, Space Key pressed and slider value at full
            if (foodBarInteraction.GetSliderValue() == 10)
            {
                if (hasJumped)
                {

                    Jump();

                    // Stop Double jump until grounded
                    hasJumped = false;

                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        

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
    }

    void Move(Vector3 direction, bool faceRight)
    {
        if (isFacingRight != faceRight)
        {
            // Flips player's scale to face the correct direction when moving
            isFacingRight = faceRight;
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }

        if (isGrounded)
        {
            rigidbodyComponent.AddForce(direction * speed * Time.deltaTime);
        }
        else
        {
            rigidbodyComponent.AddForce((direction * speed * Time.deltaTime) / 2);
        }

        // Play's run animation
        animator.SetFloat("horizontal", Mathf.Abs(direction.x));

    }

    private void CheckMoveSpeed()
    {
        float absHorizontalVolcity = MathF.Abs(rigidbodyComponent.velocity.x);
        if (absHorizontalVolcity > MaxHorizontalVelocity)
        {
            isMovingTooFast = true;
        }
        else
        {
            isMovingTooFast = false;
        }
    }

    private void Jump()
    {
        rigidbodyComponent.velocity += Vector3.up * jumpVelocity;
        audioSource2.Play();

        if (isGrounded)
        {
            animator.SetTrigger("jump");
        }
        else
        {
            animator.ResetTrigger("jump");
        }
 
    }

    public void StopMovement()
    {
        canMove = false;
    }

}
