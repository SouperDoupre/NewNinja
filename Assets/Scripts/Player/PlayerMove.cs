using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float jumpPower;//creates a designer chosen variable for jump height
    [SerializeField] private float speed;//creates a designer chosen variable for movement speed
    private float wallJumpCoolDown;//creates a variable for the cooldown for wall jumping
    private Rigidbody2D body;//creates a variable to hold the rigidbody component
    private Animator anim;//creates a variable to hold the animator component
    private BoxCollider2D boxCollider;//creates a variable to hold the boxcollider component
    private float horizontalInput;//creates a variable to hold the input of left and right
    [SerializeField] private LayerMask groundLayer;//creates a designer chosen variable to hold the ground layer
    [SerializeField] private LayerMask wallLayer;//creates a designer chosen variable to hold the wall layer

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();//places the rigidbody componenet into the body variable
        anim = GetComponent<Animator>();//places the animator componenet into the anim variable
        boxCollider = GetComponent<BoxCollider2D>();//places the boxcollider component into the boxCollider variable
    }

    private void Update()
    {
        //sets the input.getaxis to horzontal input
        horizontalInput = Input.GetAxis("Horizontal");
        //moves the player using a designer chosen speed
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        //flips the direction player is looking
        if(horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;//if the player is moving right make the player's sprite look right
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);//if the player is moving left then make the player's sprite look left
        }

        //Set animator parameters
        anim.SetBool("isWalking", horizontalInput != 0);//sets the bool "isWalking" to true and sets the horizontalInput to not 0
        anim.SetBool("grounded", isGrounded());//sets the bool "grounded" to true and calls the isGrounded function

        //jump code
        if (wallJumpCoolDown > .2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);//allows the player to continue making vertical movement while on a wall

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;//while the player is on a wall and not grounded gravity is 0
                body.velocity = Vector2.zero;//and movement is stopped
            }
            else
                body.gravityScale = 7;//otherwise gravity is set to 1 for the player
            
            if (Input.GetKey(KeyCode.Space))
                Jump();//if the player presses the Space key then call the Jump function
        }
        else
            wallJumpCoolDown += Time.deltaTime;//wallJumpCoolDown will increase with Tim.deltaTime while it is below the set range

    }
    private void Jump()//a function that will dedcide how the player will jump
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);//as long as the player is grounded,
                                                                    //change the players x position and gradually increase it by jumpPower
            anim.SetTrigger("jump");//start the jump animation
        }
        else if(onWall() && !isGrounded())
        {
            if(horizontalInput == 0)//also if the player is on a wall and not grounded
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);//then take the players current scale and multiply it by 2 and put the y on plus 6 up
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 2, 6);

            wallJumpCoolDown = 0;//reset the cooldown to 0
        }
    }
    private bool isGrounded()//a function that will check if the player is grounded
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);//shoots a laser below the player checking
                                                                                                                                            //for something assigned to the ground layer
        return raycastHit.collider != null;//normally will return false unless something is detected
    }
    private bool onWall()//a function that will check if the player is on a wall
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);//shoots a laser in the direction the player
                                                                                                                                                                    //is facing to detect something assigned to the wall layer
        return raycastHit.collider != null;//will normally return false unless something is detected
    }
    public bool CanAttack()//a function that will decide if the player can attack
    {
        return horizontalInput == 0 && isGrounded() && !onWall();//will only return true when the player is not moving, not on a wall, 
    }
}
