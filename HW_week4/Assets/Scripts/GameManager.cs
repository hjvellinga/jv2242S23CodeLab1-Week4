using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
using System.IO;
using System.Net.Mime;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Application = UnityEngine.Device.Application;

public class GameManager : MonoBehaviour
{
    [Header("set singleton")]
    public static GameManager Instance;
    
    [Header("collision")]
    public bool playersCollided = false;
    
    [Header("keep track of level")]
    public int currentLevel = 0;
    
    [Header("declare players")]
    public GameObject playerOne;
    public GameObject playerTwo;
    
    [Header("declare original positions players")]
    public Vector2 originalPosPl1; //declare variable original position player 2
    public Vector2 originalPosPl2; //declare variable original position player 2

    [Header("week 3, storing high score in files and reading from them")]
    private int score = 0; //private means only accessible in this script

    private const string DIR_DATA = "/Data/"; //IO allows to refer to directory in system
    private const string FILE_HIGH_SCORE = "highScore.txt"; //refer to specific file in directory
    private string PATH_HIGH_SCORE; //refer to path by which to reach directory & file

    //public const string PREF_HIGH_SCORE = "hsScore;"; //in order to avoid making mistakes when writing multiple strings, you can set a Constant Variable, like this one (const)
    //when making a const variable, write identifier first and THEN specifications
    
    //WEEK 4
    public TextMeshProUGUI scoreText;
    private bool inGame = true; 
    public int Score
    {
        get
        {
            return score; 
            
        }
        set
        {
            score = value; 
            Debug.Log(message: "SCORE CHANGED");

            if (score > HighScore) //if the current score is higher than the high score TODO REMOVE this if statement ?? 
            {
                HighScore = score; //change the high score to current score TODO REMOVE?? 
            }
        }
    }

    private int highScore = 2;

    [Header("Load Scene Delay")] 
    private float delayBeforeLoading = 5f;
    private float timeElapsed; 

    public int HighScore
    {
        get { return highScore; }
        set
        {
            highScore = value;
            Debug.Log(message: "NEW HIGH SCORE");
            Directory.CreateDirectory(Application.dataPath + DIR_DATA); 
            //SAVE HS HERE: now we're creating a space for STORAGE of small data chunks between plays --
            //DATA PERSISTENCE by using Player Preferences. Can only save 3 types of things - strings, integers and floats
            File.WriteAllText(PATH_HIGH_SCORE, "" + highScore); //don't forget to refresh unity files when you make changes in the file system through code;
            //turning INT into string by writing <"" + int>
        }
    }
    void Awake()
    {
        if (Instance == null)  //if instance has not been set to anything yet
        {
            DontDestroyOnLoad(gameObject); //don't destroy the game manager (?)
            Instance = this; //instance is now called "this"
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //set original position of both players
        originalPosPl1 = playerOne.transform.position;
        originalPosPl2 = playerTwo.transform.position;
        
       //WEEK 3
        PATH_HIGH_SCORE = Application.dataPath + DIR_DATA + FILE_HIGH_SCORE; //takes us to assets folder + to the specific folder + to the file

        if (File.Exists(PATH_HIGH_SCORE))
        {
            HighScore = Int32.Parse(File.ReadAllText(PATH_HIGH_SCORE)); //Int32.Parse changes INT into string
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playersCollided)
        {
            timeElapsed += Time.deltaTime; //start counting up for the scene load delay
            GetComponent<TimerScript>().timerActive = false; 
            score = (int)GetComponent<TimerScript>().currentTime; //show the time at which the players collided
            //QUESTION - how do I 'freeze' the time. 

            if (timeElapsed > delayBeforeLoading)
            {
                currentLevel++;
                SceneManager.LoadScene(currentLevel);
                playersCollided = false;
                playerOne.transform.position = originalPosPl1; //reset position Player 1.
                playerTwo.transform.position = originalPosPl2; //reset position Player 2. 
                GetComponent<TimerScript>().timerActive = true; //reset timerActive bool so timer starts counting again
                //TODO FREEZE TIMER VALUE WHEN PLAYERS COLLIDE
            }


        }
        
        //WEEK 4
        scoreText.text = 
            "score: " + score + "\n" +
            "high score: " + HighScore;

        void UpdateHighScores()
        {
            
        }
        

    }
}
