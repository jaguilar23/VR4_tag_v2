using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLabel : MonoBehaviour
{
    public bool isSeeker;
    public float waitTime = 15.0f;
    public float designateSeekerTimer = 0.0f;
    float seekerStartTime = 10.0f;
    public float currentSeekerTimer;

    public int numTeleports = 0;
    float teleportDistance = 15.0f;
    private MeshRenderer myRenderer;

    [SerializeField] TextMeshProUGUI teleportCountDisplay;
    [SerializeField] TextMeshProUGUI seekerLabel;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        currentSeekerTimer = seekerStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        designateSeekerTimer += Time.deltaTime;

        if (isSeeker)
        {
            if (currentSeekerTimer <= 0.0f)
            {
                GetComponentInParent<LifeCounter>().TakeDamage(1);
                resetTimer();
            }
            else
            {
                currentSeekerTimer -= Time.deltaTime;
            }
        }

        // Displays teleport use count in UI
        teleportCountDisplay.text = "Teleports: " + numTeleports.ToString();

        if (Input.GetKeyDown(KeyCode.U) && numTeleports > 0)
        {
            Vector3 teleportPosition = transform.position + transform.forward * teleportDistance;
            transform.position = teleportPosition; // move the player

            numTeleports--;
        }

        if (designateSeekerTimer > waitTime)
        {
            // display UI to user
            if (isSeeker)
            {
               // Debug.Log("You are the SEEKER");
                seekerLabel.text = "You are the SEEKER ";

            }
            else
            {
              //  Debug.Log("You are a HIDER");
                seekerLabel.text = "You are a HIDER ";
            }
        }
    }

    void resetTimer()
    {
        currentSeekerTimer = seekerStartTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isSeeker)
        {
            Debug.Log("SEEK'D");
            isSeeker = false;
            collision.gameObject.GetComponentInParent<LifeCounter>().TakeDamage(1);
            collision.gameObject.GetComponent<PlayerLabel>().isSeeker = true;
            collision.gameObject.transform.position += new Vector3(10.0f, 10.0f, 10.0f);
        }
    }
}