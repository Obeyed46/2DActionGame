using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    //Generic Variables
    public float healthPoints;
    public BoxCollider2D EnemyCollider;
    public GameObject Instance;
   

    // Use this for initialization
    void Start () {

        EnemyCollider = GetComponent<BoxCollider2D>();

	}
	
	// Update is called once per frame
	void Update () {
		
        if(healthPoints <= 0)
        {
            healthPoints = 0;
            Destroy(Instance);
        }

       
	}

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        Debug.Log(healthPoints);
        CameraScript.Instance.CameraShake(0.05f, 0.07f);
    }
}
