using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float jumpForce=1f; 
    [SerializeField]private float speed=1f;
    
    private PlayerInput playerInput;
    private GameObject player;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private Vector3 move;
    private bool inJump =false;
    private int timer=0;

    void Start()
    {
        rigidBody=GetComponent<Rigidbody2D>();
        playerInput=GetComponent<PlayerInput>();
        animator=GetComponent<Animator>();
        playerInput.actions["Jump"].performed+=OnClickJump;
    }

    void Update()
    {  
         move = playerInput.actions["Move"].ReadValue<Vector2>();
        if(move.x>0)
        {
            transform.localScale=new Vector3(2,2,2);
        }
        else if (move.x<0)
        {
            transform.localScale=new Vector3(-2,2,2);
        }
        animator.SetFloat("Speed", Math.Abs(move.x));
        if(!inJump)
        {
            transform.Translate(move*Time.deltaTime*speed);
        }
        else
        {
            transform.position+= Vector3.right*move.x*Time.deltaTime*speed/2;
        }

        animator.SetBool("OnGround",GroundCheck());
        
    }


    public void OnClickJump(InputAction.CallbackContext a)
    {
        Jump();
    }

    void Jump()
    {
        if(!GroundCheck())
            return;
        animator.Play("Jump");
        animator.SetBool("OnGround",false);
        GetComponent<Rigidbody2D>().AddForce(Vector3.up*jumpForce+Vector3.right*move.x*(jumpForce/2),ForceMode2D.Impulse);
    }
    private bool GroundCheck()
    {
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }

}
