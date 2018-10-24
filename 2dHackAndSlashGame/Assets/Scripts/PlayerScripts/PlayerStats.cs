using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    //Instance & Player
    public static PlayerStats Instance;
    public GameObject player;

    //Attack
    public bool canBeHit;

    //Stats
    public float maxHealth;
    float currentHealth;

    //HealthBar
    public Image redBar, yellowBar;


	// Use this for initialization
	void Start () {

        canBeHit = true;
        currentHealth = maxHealth;

	}
	
	// Update is called once per frame
	void Update () {
		
        //YellowBarEffect
        if(yellowBar.fillAmount > redBar.fillAmount)
        {
            yellowBar.fillAmount -= 0.002f;
        }
        
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
            currentHealth -= damage;
            yellowBar.fillAmount = redBar.fillAmount;
            redBar.fillAmount -= damage / maxHealth;
            PlayerController.Instance.MyAnim.SetTrigger("Stagger");
            CameraScript.Instance.CameraShake(0.05f, 0.07f);
            if (currentHealth <= 0)
            {
                Destroy(player);
                yellowBar.fillAmount = 0f;
            }
        }
    }

}
