using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //General
    public Rigidbody2D MyRb;
    public Animator MyAnim;

    //Run variables
    public float MaxSpeed;
    bool FacingRight, CanMove;

    //Jump variables
    bool Grounded, CanJump;
    float GroundCheckRadius = 0.1f;
    public LayerMask GroundLayer;
    public Transform GroundCheck;
    public float JumpHeight;

    //Combo variables
    bool Attack2, Attack3;

    // Use this for initialization
    void Start () {

        MyRb = GetComponent<Rigidbody2D>();
        MyAnim = GetComponent<Animator>();
        FacingRight = true;
        CanMove = true;
        CanJump = true;
        Attack2 = false;
        Attack3 = false;

	}
	
	// Update is called once per frame
	void Update () {

        //When Idle state
        if (MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            CanMove = true;
            CanJump = true;
            Attack2 = false;
            Attack3 = false;
        }

        //Jump
        if (Grounded && CanJump && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Grounded = false;
            MyAnim.SetBool("IsGrounded", Grounded);
            MyRb.AddForce(new Vector2(0, JumpHeight));
        }

        //Attack
        if (Grounded)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button2) && !Attack2 && !Attack3)
            {
                MyAnim.SetTrigger("Attack1");
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button2) && Attack2 && !Attack3)
            {
                MyAnim.SetTrigger("Attack2");
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton2) && !Attack2 && Attack3)
            {
                MyAnim.SetTrigger("Attack3");
            }
        }

        //Do not move if Attacking
        if(MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack3") || MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") || MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            CanMove = false;
            CanJump = false;
        }



    }

    //Called once per frame
    void FixedUpdate()
    {
        if(MyRb.bodyType == RigidbodyType2D.Dynamic)
        {
            //CheckIfGrounded
            Grounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer);
            MyAnim.SetBool("IsGrounded", Grounded);
            MyAnim.SetFloat("VerticalSpeed", MyRb.velocity.y);

            //Run
            if (CanMove)
            {
                MyAnim.SetBool("IsRunning", false);
                float move = Input.GetAxis("Horizontal");
                MyAnim.SetFloat("Speed", Mathf.Abs(move));
                MyRb.velocity = new Vector2(move * MaxSpeed, MyRb.velocity.y);

                if (move > 0 && !FacingRight)
                {
                    Flip();
                }
                else if (move < 0 && FacingRight)
                {
                    Flip();
                }
            }

        }


    }


    void Flip()
    {
        FacingRight = !FacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    
    //Animations Events

    public void ComboAttack2()
    {
        Attack2 = true;
    }

    public void Attack2End()
    {
        Attack2 = false;
    }

    public void ComboAttack3()
    {
        Attack3 = true;
    }

    public void Attack3End()
    {
        Attack3 = false;
    }

}
