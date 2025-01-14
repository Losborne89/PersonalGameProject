using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 500f;
    float jumpHeight = 10f;
    float wallBounceForce = 2f;
    float wallNormalValue = 0.2f;

    public enum PlayerState
    {
        NULL,
        GROUNDED_IDLE,
        GROUNDED_RUNNING,
        INAIR,
        DOUBLE_JUMP,
    }

    public PlayerState currentState = PlayerState.NULL;

    public Rigidbody playerRB;

    public Animator playerAnimator;

    public bool CanDoubleJump = false;

    public AudioSource audioSource;

    bool isMoving = false;

    bool isLeftPressed = false;
    bool isRightPressed = false;    
    bool isJumpPressed = false;



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
       isRightPressed = true;
    }

    private void HandleOnMoveLeft()
    {
        isLeftPressed = true;
    }

    private void HandleOnJump()
    {
        isJumpPressed = true;
    }

    // sets the player state based on the new state and updates animations
    public void SetState(PlayerState newState)
    {
        if (currentState == newState)
        {
            //do nothing if the state is the same
            return;
        }

        currentState = newState;
        switch (currentState)
        {
            case PlayerState.GROUNDED_IDLE:
                playerAnimator.SetBool("IsMoving", isMoving);
                playerAnimator.SetBool("IsGrounded", true);
                break;
            case PlayerState.GROUNDED_RUNNING:
                playerAnimator.SetBool("IsMoving", isMoving);
                playerAnimator.SetBool("IsGrounded", true);
                break;
            case PlayerState.INAIR:
                playerAnimator.SetTrigger("Jump");
                playerAnimator.SetBool("IsMoving", isMoving);
                playerAnimator.SetBool("IsGrounded", false);
                break;
            case PlayerState.DOUBLE_JUMP:
                playerAnimator.SetTrigger("Jump");
                playerAnimator.SetBool("IsMoving", isMoving);
                playerAnimator.SetBool("IsGrounded", false);
                break;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.canMove == false)
        {
            return;
        }

        isMoving = false;

        if (isRightPressed)
        {
            isMoving = true;
            // flip the plater based on the direction
            this.transform.localEulerAngles = new Vector3(0, 0, 0);

            // in the air dont update the state and move at half speed
            if (currentState is PlayerState.INAIR or PlayerState.DOUBLE_JUMP)
            {
                playerRB.AddForce(Vector3.right * speed / 2 * Time.fixedDeltaTime);
            }
            else
            {
                // when on the ground update the state and move at full speed
                SetState(PlayerState.GROUNDED_RUNNING);
                playerRB.AddForce(Vector3.right * speed * Time.fixedDeltaTime);
            }
        }

        if (isLeftPressed)
        {
            isMoving = true;
            //flip the plater based on the direction
            this.transform.localEulerAngles = new Vector3(0, 180, 0);

            //in the air dont update the state and move at half speed
            if (currentState is PlayerState.INAIR or PlayerState.DOUBLE_JUMP)
            {
                playerRB.AddForce(Vector3.left * speed / 2 * Time.fixedDeltaTime);
            }
            else
            {
                //when on the ground update the state and move at full speed
                SetState(PlayerState.GROUNDED_RUNNING);
                playerRB.AddForce(Vector3.left * speed * Time.fixedDeltaTime);
            }
        }

        if (currentState == PlayerState.DOUBLE_JUMP)
        {
            DoubleJumpingUpdate();
        }

        if (currentState == PlayerState.INAIR)
        {
            JumpingUpdate();
        }

        if (currentState is PlayerState.GROUNDED_IDLE or PlayerState.GROUNDED_RUNNING)
        {
            GroundedUpdate();
        }

        if (isMoving == false && currentState == PlayerState.GROUNDED_RUNNING)
        {
            SetState(PlayerState.GROUNDED_IDLE);
        }

        // reset the pressed keys
        isLeftPressed = false;
        isRightPressed = false;
        isJumpPressed = false;

    }

    //if the player collides with the ground update the state to grounded idle
    public void OnCollisionEnter(Collision collision)
    {
        //check the collision tag if it is ground
        if (collision.gameObject.tag == "Ground")
        {
            Vector3 Normal = Vector3.zero;

            Normal = collision.contacts[0].normal;

            float normaldDot = Vector3.Dot(Normal, Vector3.up);
            // check the normal angle with the collision based on the dot product if grater that o.2 then the player is grounded
            if (normaldDot > wallNormalValue)
            {
                SetState(PlayerState.GROUNDED_IDLE);
            }
            else
            {
                //move away from collision if the player hits a wall
                playerRB.velocity = Normal * wallBounceForce;
            }
        }
    }

    // if jump is pressed update the sate to in air
    public void GroundedUpdate()
    {
        if (Jump())
        {
            SetState(PlayerState.INAIR);
        }
    }

    // checks if the player can jump and jumps
    private bool Jump()
    {
        // if jump is pressed, jump
        if (isJumpPressed)
        {
            audioSource.Play();
            playerRB.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            isJumpPressed = false;
            return true;
        }
        return false;
    }

    // update while jumping
    public void JumpingUpdate()
    {
        // if the player can double jump and the jump button is pressed  
        if (CanDoubleJump)
        {
            if (Jump())
            {
                SetState(PlayerState.DOUBLE_JUMP);
            }
        }
    }

    // placeholder update while double jumping
    public void DoubleJumpingUpdate()
    {

    }
}
