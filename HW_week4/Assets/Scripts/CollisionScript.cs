using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionScript : MonoBehaviour
{
    public static CollisionScript Instance; //declare an instance of this script
    //public int currentLevel = 0; -> should be in game manager
    // Start is called before the first frame update
    
    //declare variable original position player 1
    //declare game manager holder object and then the game manager script
    public GameObject gameManager;
    public GameManager myManager;

    void Awake() //plays before scene is loaded
    {
        if (Instance == null) //if the instance hasn't been set 
        {
            DontDestroyOnLoad(gameObject); //don't destroy the game objects in the script ?
            Instance = this; //and set to this
        }
        else 
        {
            Destroy(gameObject); //once it's been set to this, destroy all consecutive instances
        }
    }
    void Start()
    {
        FindManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
            
        {

            Debug.Log("collided with other player");
            myManager.playersCollided = true; //set bool players collided to true 
            
            
            
          //  GetComponent <ParticleSystem>().Play (); //spawn the particle system! 
          //  ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
          //  em.enabled = true;

        }
    }

    public void FindManager()
    {
        gameManager = GameObject.Find("GameManagerHolder"); //set the game manager holder
        myManager = gameManager.GetComponent<GameManager>();
    }
}
