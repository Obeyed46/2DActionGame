using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    //General
    public static CameraScript Instance;

    //Player Transform
    public Transform playerTransform;

    //Time and shake variables
    public float smoothTime = 0.10f;
    public float shakeTimer, shakeAmount;
    Vector3 Velocity = Vector3.zero;



    //Comes before start
    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

	}
	
	// Update is called once per frame
	void Update () {

        if (shakeTimer >= 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmount;
            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
            shakeTimer -= Time.deltaTime;
        }

    }


    //Called every frame
    void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(playerTransform.position.x, playerTransform.position.y + 0.3f, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref Velocity, smoothTime);
    }


    /////METHODS/////

    public void CameraShake(float shakePWR, float shakeDUR)
    {
        shakeAmount = shakePWR;
        shakeTimer = shakeDUR;
    }
}
