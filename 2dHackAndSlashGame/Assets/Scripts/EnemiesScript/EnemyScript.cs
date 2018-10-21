using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

    //Generic Variables
    public BoxCollider2D EnemyCollider;
    public GameObject Instance;
    Animator Anim;
    Rigidbody2D rb;

    //Moving
    public bool CanFlip;
    bool FacingRight;

    //Player target
    Transform player;

    //Stats
    public float maxHealht;
    float currentHealth;

    //HealthBar
    public Image backGroundBar, maskBar, redBar, yellowBar;


    // Use this for initialization
    void Start () {

        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        EnemyCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        CanFlip = true;
        FacingRight = false;
        currentHealth = maxHealht;

	}
	
	// Update is called once per frame
	void Update () {
		
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(Instance);
        }

        if(transform.position.x < player.transform.position.x && !FacingRight && CanFlip)
        {
            Flip();
        }
        else if(transform.position.x > player.transform.position.x && FacingRight && CanFlip)
        {
            Flip();
        }

        //HealthBars
        if (yellowBar.fillAmount > redBar.fillAmount)
        {
            yellowBar.fillAmount -= 0.003f;
        }

        //Do not flip if attacking
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            CanFlip = false;
        }
        else
        {
            CanFlip = true;
        }

    }


    private void FixedUpdate()
    {
        //Chasing State
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 2 * Time.deltaTime);
        }

    }

    public void TakeDamage(float damage)
    {
        if(currentHealth == maxHealht)
        {
            backGroundBar.enabled = true;
            maskBar.enabled = true;
            redBar.enabled = true;
            yellowBar.enabled = true;
        }
        currentHealth -= damage;
        redBar.fillAmount -= damage / maxHealht;
        Anim.SetTrigger("Stagger");
        CameraScript.Instance.CameraShake(0.05f, 0.07f);
    }

    void Flip()
    {
        FacingRight = !FacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    //Animations Events

    public void AttackSprint()
    {
        if (FacingRight)
        {
            rb.velocity = Vector2.right * 2;
        }
        else if (!FacingRight)
        {
            rb.velocity = Vector2.left * 2;
        }

    }

    public void EndAttackSprint()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

}



