using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    //Instance
    public static PlayerStats Instance;

    //Attack
    public bool canBeHit;


	// Use this for initialization
	void Start () {

        canBeHit = true;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Called before start
    private void Awake()
    {
        Instance = this;
    }

    //METHODS//

    public void TakeDamage(float damage)
    {
        if (canBeHit)
        {
            PlayerController.Instance.MyAnim.SetTrigger("Stagger");
            CameraScript.Instance.CameraShake(0.05f, 0.07f);
        }
    }

}
