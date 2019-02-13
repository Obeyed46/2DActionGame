using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Instance
    public static PlayerController Instance;

    //General
    public Rigidbody2D MyRb;
    public Animator MyAnim;
    public SpriteRenderer sprite;

    //Run variables
    public float MaxSpeed;
    bool FacingRight, CanMove;

    //Jump variables
    bool Grounded, CanJump;
    float GroundCheckRadius = 0.1f;
    public LayerMask GroundLayer;
    public Transform GroundCheck;
    public float JumpHeight;

    //Attack variables
    bool CanAttack, Attack2, Attack3, DashAttack;
    public BoxCollider2D PlayerCollider;
    public Transform weaponPos;
    public float weaponRange, weaponDamage;
    public LayerMask enemiesLayer;

    //VFXs
    [SerializeField]
    GameObject playerGhost;
    bool SpawnGhost;

    //Dash variables
    bool CanDash, IsDashing;

    // Use this for initialization
    void Start () {

        MyRb = GetComponent<Rigidbody2D>();
        MyAnim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        PlayerCollider = GetComponent<BoxCollider2D>();
        FacingRight = true;
        CanMove = true;
        CanJump = true;
        CanDash = true;
        Attack2 = false;
        Attack3 = false;
        DashAttack = false;
        SpawnGhost = false;

	}

    void Awake()
    {
        Instance = this;
    }


    // Update is called once per frame
    void Update () {

        //When Idle state
        if (MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            CanMove = true;
            CanJump = true;
            CanAttack = true;
            CanDash = true;
            IsDashing = false;
            Attack2 = false;
            Attack3 = false;
            DashAttack = false;
        }

        //Jump
        if (Grounded && CanJump && Input.GetKeyDown(KeyCode.Joystick1Button0) && !MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
        {
            Grounded = false;
            MyRb.AddForce(new Vector2(0, JumpHeight));
        }

        //HandleAnimationState
        if (MyRb.velocity.x != 0 && Grounded)
        {
            MyAnim.SetInteger("State", 4);
        }
        else if (MyRb.velocity.x == 0 && Grounded)
        {
            MyAnim.SetInteger("State", 3);
        }

        if (MyRb.velocity.y > 0 && !Grounded)
        {
            MyAnim.SetInteger("State", 1);
        }
        else if (MyRb.velocity.y < 0 && !Grounded)
        {
            MyAnim.SetInteger("State", 2);
        }



        //Attack
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) && CanAttack && !Attack2 && !Attack3 && !Input.GetKey(KeyCode.Joystick1Button4))
        {
            MyAnim.SetTrigger("Attack1");
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button2) && Attack2 && !Attack3 && !Input.GetKey(KeyCode.Joystick1Button4))
        {
            MyAnim.SetTrigger("Attack2");
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton2) && !Attack2 && Attack3 && !Input.GetKey(KeyCode.Joystick1Button4))
        {
            MyAnim.SetTrigger("Attack3");
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton2) && Input.GetKey(KeyCode.Joystick1Button4))
        {
            MyAnim.SetTrigger("SpecialAttack");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button3) && !Input.GetKey(KeyCode.Joystick1Button4))
        {
            MyAnim.SetTrigger("HeavyAttack");
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button3) && Input.GetKey(KeyCode.Joystick1Button4))
        {
            MyAnim.SetTrigger("DashAttack");
        }

        //Do not move if Attacking or 
        if (MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack3") || MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") || MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") || MyAnim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack") || MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Dash") || MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Stagger") || MyAnim.GetCurrentAnimatorStateInfo(0).IsName("DashAttack") || MyAnim.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"))
        {
            CanMove = false;
            CanJump = false;
            CanDash = false;
        }

        //Dashing
        if(Input.GetKeyDown(KeyCode.Joystick1Button1) && CanDash)
        {
            MyAnim.SetTrigger("Dashing");
        }

        //SpawnGhostEffect
        if (SpawnGhost)
        {
            GameObject ghost = Instantiate(playerGhost, transform.position, transform.rotation);
        }



    }

    //Called once per frame
    void FixedUpdate()
    {
        if(MyRb.bodyType == RigidbodyType2D.Dynamic)
        {
            //CheckIfGrounded
            Grounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer);

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


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weaponPos.position, weaponRange);
    }

    public void EndCombo()
    {
        Attack2 = false;
        Attack3 = false;
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

    public void DoNotPlay()
    {
        CanAttack = false;
    }

    public void EndAttackSprint()
    {
        MyRb.velocity = new Vector2(0, MyRb.velocity.y);
    }


    public void Sprint(float sprintValue)
    {
        if (FacingRight)
        {
            MyRb.velocity = Vector2.right * sprintValue;
        }
        else if (!FacingRight)
        {
            MyRb.velocity = Vector2.left * sprintValue;
        }

    }


    public void CheckEnemies()
    {
        Collider2D[] enemiesTouched = Physics2D.OverlapCircleAll(weaponPos.position, weaponRange, enemiesLayer);
        for (int i = 0; i < enemiesTouched.Length; i++)
        {
            enemiesTouched[i].GetComponent<EnemyScript>().TakeDamage(weaponDamage);
        }

    }

    public void CheckEnemiesJumpStagger()
    {
        Collider2D[] enemiesTouched = Physics2D.OverlapCircleAll(weaponPos.position, weaponRange, enemiesLayer);
        for (int i = 0; i < enemiesTouched.Length; i++)
        {
            enemiesTouched[i].GetComponent<EnemyScript>().rb.AddForce(new Vector2(0f, 1000f));
        };

    }

    public void StartEndGhostEffect()
    {
        if (!SpawnGhost)
        {
            SpawnGhost = true;
        }
        else if (SpawnGhost)
        {
            SpawnGhost = false;
        }
    }


    

}
