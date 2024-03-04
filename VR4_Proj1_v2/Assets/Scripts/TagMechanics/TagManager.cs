using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TagManager : MonoBehaviour
{

    public GameObject[] playerList;
    private float lobbyCountdown = 15.0f;
    private float currentCountdown;
    bool gameStarted;

    private void Start()
    {
        gameStarted = false;
        currentCountdown = lobbyCountdown;
    }

    // Update is called once per frame
    void Update()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");

        if (playerList.Length >= 2) // at least 2 players
        {
            if (currentCountdown <= 0 && !gameStarted)
            {
                designateSeeker();
                gameStarted = true;
            }
            else if (lobbyCountdown > 0)
            {
                currentCountdown -= Time.deltaTime;
                for (int i = 0; i < playerList.Length; i++)
                {
                    playerList[i].GetComponent<PlayerLabel>().lobbyStat.gameObject.SetActive(false);
                    playerList[i].GetComponent<PlayerLabel>().lobbyTimer.gameObject.SetActive(true);
                    playerList[i].GetComponent<PlayerLabel>().lobbyTimer.text = "Starts in " + ((int)currentCountdown).ToString();
                }
            }
        } else
        {
            for (int i = 0; i < playerList.Length; i++)
            {
                playerList[i].GetComponent<PlayerLabel>().lobbyStat.gameObject.SetActive(true);
            }
            currentCountdown = lobbyCountdown;
        }
    }

    public void designateSeeker()
    {
        // setting everyone to hider
        for (int i = 0; i < playerList.Length; i++)
        {
            playerList[i].GetComponent<PlayerLabel>().isSeeker = false;
            playerList[i].GetComponent<PlayerLabel>().gameStarted = true;
        }

        // selecting random player as seeker
        playerList[Random.Range(0, playerList.Length)].GetComponent<PlayerLabel>().isSeeker = true;

        // setting everyone to hider
        for (int i = 0; i < playerList.Length; i++)
        {
            StartCoroutine(playerList[i].GetComponent<PlayerLabel>().DisplayStatus());
        }
    }
}