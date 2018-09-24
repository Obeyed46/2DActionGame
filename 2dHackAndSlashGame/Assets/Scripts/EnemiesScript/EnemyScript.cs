using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    //Generic Variables
    public int healthPoints;
    public BoxCollider2D EnemyCollider;




	// Use this for initialization
	void Start () {

        EnemyCollider = GetComponent<BoxCollider2D>();

	}
	
	// Update is called once per frame
	void Update () {
		
       
	}

    void TakeDamage(int damage)
    {
        healthPoints -= damage;
        Debug.Log(healthPoints);
    }
}
