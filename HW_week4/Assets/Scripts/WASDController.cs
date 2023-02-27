using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDController : MonoBehaviour
{
    public static WASDController Instance; 
    
    private Rigidbody2D rb2d;

    public float forceAmount = 10; //movement

    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null) //if instance hasn't been set
        {
            DontDestroyOnLoad(gameObject); //don't destroy the gameObject
            Instance = this; //and set it to this
        }
        else
        {
            Destroy(gameObject); //when it's been set to this, destroy all other game objects 
        }
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frames
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddForce(Vector2.right*forceAmount);
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
        rb2d.velocity *= 0.999f;
    }
}
