using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UIElements;

public class TimerScript : MonoBehaviour
{
    [Header("Make singleton")] 
    public static TimerScript Instance; 
    
    [Header("Component")] //set a header for organization
    public TextMeshProUGUI timerText; //allows adding text to script component

    [Header("Timer settings")] 
    public float currentTime; //time value

    public bool countDown; //set if i want count down or count up

    public bool timerActive; //set if timer is counting or not

    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this; 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timerActive = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
            //if countDown bool is true (<?>), take time - time.deltaTime. Else (<:>) take time + time.deltaTime
            timerText.text =
                ("time: " + currentTime.ToString("0.0")); //update the displayed timerText.text component as 1 decimal
        }
    }
}
