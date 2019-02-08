using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour {

    SpriteRenderer sprite;
    float timer = 0.2f;


	// Use this for initialization
	void Start () {

        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = PlayerController.Instance.sprite.sprite;
        sprite.color = new Vector4(50, 50, 50, 0.1f);

        transform.position = PlayerController.Instance.transform.position;
        transform.localScale = PlayerController.Instance.transform.localScale;

       
	}
	
	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
		
	}
}
