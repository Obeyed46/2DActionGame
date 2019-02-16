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
    public float specialAttXtimer, specialAttYtimer;

    //Stats
    public float maxHealth;
    float currentHealth;


    //UIs//
    //HealthBar
    public Image redBar, yellowBar;

    //SpecialAttacks
    [SerializeField]
    Image icon1, icon2;
    [SerializeField]
    Text attackTxt1, attackTxt2;


	// Use this for initialization
	void Start () {

        canBeHit = true;
        currentHealth = maxHealth;
        specialAttXtimer = 0.0f;
        specialAttYtimer = 0.0f;

	}
	
	// Update is called once per frame
	void Update () {
		
        //YellowBarEffect
        if(yellowBar.fillAmount > redBar.fillAmount)
        {
            yellowBar.fillAmount -= 0.002f;
        }



        //Timers
        if(specialAttXtimer > 0)
        {
            specialAttXtimer -= Time.deltaTime;
            icon1.color = new Color32(110, 110, 110, 255);
            attackTxt1.text = specialAttXtimer.ToString();
            attackTxt1.enabled = true;
        }
        else if(specialAttXtimer < 0)
        {
            specialAttXtimer = 0;
            icon1.color = new Color32(255, 255, 255, 255);
            attackTxt1.enabled = false;
        }
        if (specialAttYtimer > 0)
        {
            specialAttYtimer -= Time.deltaTime;
            icon2.color = new Color32(110, 110, 110, 255);
            attackTxt2.enabled = true;
            attackTxt2.text = specialAttYtimer.ToString();
        }
        else if (specialAttYtimer < 0)
        {
            specialAttYtimer = 0;
            icon2.color = new Color32(255, 255, 255, 255);
            attackTxt2.enabled = false;
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
            PlayerController.Instance.EndCombo();
            CameraScript.Instance.CameraShake(0.05f, 0.07f);
            if (currentHealth <= 0)
            {
                Destroy(player);
                yellowBar.fillAmount = 0f;
            }
        }
    }

}
