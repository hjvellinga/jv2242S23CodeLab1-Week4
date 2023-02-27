using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
using System.IO;
using System.Net.Mime;
using Unity.VisualScripting;
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

          //  if (score > HighScore) //if the current score is higher than the high score TODO REMOVE this if statement ?? 
          //  {
          //      HighScore = score; //change the high score to current score TODO REMOVE?? 
          //  }
        }
    }

    private int highScore = 2;

    public List<int> highScores = new List<int>();
    private string FILE_PATH;
    private const string FILE_DIR = "/Data/";
    private const string FILE_NAME = "highScores.txt"; 

    [Header("Load Scene Delay")] 
    private float delayBeforeLoading = 5f;
    private float timeElapsed;

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
        
        //reset timer
        GetComponent<TimerScript>().currentTime = 0;

        //WEEK 3
        FILE_PATH = Application.dataPath + FILE_DIR + FILE_NAME;
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

        void UpdateHighScores()
        {
            if (highScores.Count == 0)
            {
                //if we have high scores stored in the datafile 
                if (File.Exists(FILE_PATH))
                {
                    string fileContents = File.ReadAllText(FILE_PATH); //distill as string
                    
                    //turn string into an array
                    string[] fileSplit = fileContents.Split('\n');
                    //go through all strings that are numbers
                    for (int i = 1; i < fileSplit.Length - 1; i++)
                    {
                        //and add the distilled numbers to the HighScores list
                        highScores.Add(Int32.Parse(fileSplit[i]));
                    }
                }
                else
                {
                    //insert placeholder highscore 
                    highScores.Add(0);
                }
            }
            //inserting our score into the high scores list, if it's large enough
            for (int i = 0; i < highScores.Count; i++) //TODO what on earth are for loops & how do arrays work exactly? ASK BRACKEYS
            {
                if (highScores[i] < Score)
                {
                    highScores.Insert(i, Score);
                    break;
                }
            }

            if (highScores.Count > 5) //if we have more than 5 high scores in the highScores list
            {
                highScores.RemoveRange(5, highScores.Count - 5); //cut it to 5 high scores
            }
            
            //make string of our high scores -- not sure what's going on here with the for loop. Again LOOK EM UP
            string highScoreStr = "High Scores: \n";

            for (int i = 0; i < highScores.Count; i++)
            {
                highScoreStr += highScores[i] + "\n"; //we take out the values of the integers in the array and turn them into strings to be displayed in the text element? 
            }
            //display high scores 
            scoreText.text = 
                "score: " + score + "\n" +
                highScoreStr; 
            
            File.WriteAllText(FILE_PATH, highScoreStr); //I guess this refreshes the txt file?? 
        }
        

    }
}
//as for the high scores, I need a high score per level. 
//tomorrow watch: https://www.youtube.com/watch?v=XOjd_qU2Ido (save and load systems in Unity) (18 mins) 
//and https://www.youtube.com/watch?v=YiE0oetGMAg Arrays (or https://www.youtube.com/watch?v=Q16KIxtomeo Loops & Arrays) (20 mins)
//and https://www.youtube.com/watch?v=9ozOSKCiO0I loops (18 mins)
