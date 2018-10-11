using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    //Generic Variables
    public float healthPoints;
    public BoxCollider2D EnemyCollider;
    public GameObject Instance;
    Animator Anim;
    bool FacingRight;

    //Player target
    Transform player;
   

    // Use this for initialization
    void Start () {

        Anim = GetComponent<Animator>();
        EnemyCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        FacingRight = false;

	}
	
	// Update is called once per frame
	void Update () {
		
        if(healthPoints <= 0)
        {
            healthPoints = 0;
            Destroy(Instance);
        }

        if(transform.position.x < player.transform.position.x && !FacingRight)
        {
            Flip();
        }
        else if(transform.position.x > player.transform.position.x && FacingRight)
        {
            Flip();
        }
       
	}

    public void TakeDamage(float damage)
    {
        //healthPoints -= damage;
        Anim.SetTrigger("Stagger");
        //Debug.Log(healthPoints);
        CameraScript.Instance.CameraShake(0.05f, 0.07f);
    }

    void Flip()
    {
        FacingRight = !FacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

}



