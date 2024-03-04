using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeCounter : MonoBehaviour
{

    public GameObject[] hearts;
    public int life;
    private bool dead;

    [SerializeField] TextMeshProUGUI gameOver;

    private void Start()
    {
        life = hearts.Length;
    }


    // Update is called once per frame
    void Update()
    {
        if (dead == true)
        {
            Debug.Log("You died!");
        }
    }

    public void TakeDamage(int d)
    {
        life -= d;
        hearts[life].gameObject.SetActive(false);
        if (life < 1)
        {
            gameOver.text = "GAME OVER";
            dead = true;
           // Application.Quit();
        }
    }
}