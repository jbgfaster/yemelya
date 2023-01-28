using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float jumpForce=1f; 
    [SerializeField]private float speed=1f;
    
    private PlayerInput playerInput;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private GroundCheck groundCheck;

    private Vector2 move;
    private bool inJump =false;

    void Start()
    {
        playerInput=GetComponent<PlayerInput>();
        animator=GetComponent<Animator>();
        rigidBody=GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        playerInput.actions["Jump"].performed+=OnClickJump;
    }

    void Update()
    {  
        move = playerInput.actions["Move"].ReadValue<Vector2>();

        Move();
        FixBonds();

        SetAnimations();
    }

    private void Move()
    {
        if(!inJump)
        {
            transform.Translate(move*Time.deltaTime*speed);
        }
        else
        {
            transform.position+= Vector3.right*move.x*Time.deltaTime*speed/2;
        }
    }

    private void FixBonds()
    {
        if(move.x>0)
        {
            transform.localScale=new Vector3(2,2,2);
        }
        else if (move.x<0)
        {
            transform.localScale=new Vector3(-2,2,2);
        }
    }

    private void SetAnimations()
    {
        animator.SetFloat("Speed", Math.Abs(move.x));
        animator.SetBool("OnGround",groundCheck.IsGrounded);       
    }

    private void OnClickJump(InputAction.CallbackContext a)
    {
        Jump();
    }

    void Jump()
    {
        if(!groundCheck.IsGrounded)
            return;
        animator.Play("Jump");
        animator.SetBool("OnGround",false);
        GetComponent<Rigidbody2D>().AddForce(Vector3.up*jumpForce+Vector3.right*move.x*(jumpForce/2),ForceMode2D.Impulse);
    }
}
