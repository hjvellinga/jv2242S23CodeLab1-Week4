using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D rb2d2; //declare rigidbody2d variable + give name
    
    public static ArrowController Instance; 

    public float forceAmount2 = 10; //declare forceAmount variable
    // Start is called before the first frame update

    private void Awake() //called before the scene has loaded
    {
        if (Instance == null) //if instance hasn't been set
        {
            DontDestroyOnLoad(gameObject); //don't destroy the gameobject
            Instance = this;  //and set it to this
        }
        else
        {
            Destroy(gameObject); //when it's been set to this, destroy all other gameobjects
        }
    }

    void Start()
    {
        rb2d2 = GetComponent<Rigidbody2D>(); //determine content of rb2d2
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            rb2d2.AddForce(Vector2.left*forceAmount2);
        }
        else
        {
            rb2d2.velocity = Vector2.zero;
        }
        rb2d2.velocity *= 0.999f;
    }
}
