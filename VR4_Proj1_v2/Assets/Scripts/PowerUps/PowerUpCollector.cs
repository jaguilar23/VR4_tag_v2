using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpCollector : MonoBehaviour
{
    public static PowerUpCollector instance;

    [SerializeField] TextMeshProUGUI teleportCounter;

    int score = 0;

    private void Awake()
    {
        /*
        if (!scoreText) scoreText = GetComponent<TextMeshProUGUI>() 
        score++;
        scoreText.text = "Score: " + score;
        */
        instance = this;

    }

    void Start()
    {
        teleportCounter.text = "Teleports: " + score.ToString();
    }



    public void AddScore()
    {

        Debug.Log("Hit");
        score++;
        //teleportCounter.text = "Teleports: " + score.ToString();
        teleportCounter.text = "Teleports: ";
        Debug.Log("Add");

    }

}
