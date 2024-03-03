using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TagManager : MonoBehaviour
{

    public GameObject[] playerList;
    public GameObject playerLabel;
    private float designateSeekerTimer = 7.0f;
    private PlayerLabel script;
    bool gameStarted;

    private void Start()
    {
        gameStarted = false;
        script = playerLabel.GetComponent<PlayerLabel>();
    }

    // Update is called once per frame
    void Update()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");

        if (designateSeekerTimer <= 0 && !gameStarted)
        {
            designateSeeker();
            gameStarted = true;
        } else if (designateSeekerTimer > 0)
            designateSeekerTimer -= Time.deltaTime;

        /*
        if (Input.GetKeyDown(KeyCode.K))
        {
            designateSeeker();
        }
        */
    }

    public void designateSeeker()
    {
        // setting everyone to hider
        for (int i = 0; i < playerList.Length; i++)
        {
            playerList[i].GetComponent<PlayerLabel>().isSeeker = false;
        }

        // selecting random player as seeker
        playerList[Random.Range(0, playerList.Length)].GetComponent<PlayerLabel>().isSeeker = true;
    }
}