using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLabel : MonoBehaviour
{
    public bool isSeeker;
    public float waitTime = 15.0f;
    public bool gameStarted = false;
    public float designateSeekerTimer = 0.0f;
    float seekerStartTime = 120.0f;
    public float currentSeekerTimer;
    float seekerCooldown = 5.0f;
    float currentCooldown = 0.0f;

    public int numTeleports = 0;
    public float teleportDistance = 15.0f;
    private MeshRenderer myRenderer;

    public GameObject playerTeleportUI;
    [SerializeField] TextMeshProUGUI teleportCountDisplay;
    [SerializeField] TextMeshProUGUI seekerLabel;
    [SerializeField] TextMeshProUGUI seekerTimer;
    public TextMeshProUGUI lobbyStat;
    public TextMeshProUGUI lobbyTimer;

    // player material stuff
    [SerializeField] List<GameObject> playerParts;
    public Material originalMaterial;
    public Material alertMaterial;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        currentSeekerTimer = seekerStartTime;
        lobbyStat.gameObject.SetActive(false);
        lobbyTimer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            lobbyStat.gameObject.SetActive(false);
            lobbyTimer.gameObject.SetActive(false);
            designateSeekerTimer += Time.deltaTime;
            currentCooldown = (currentCooldown >= 0) ? currentCooldown - Time.deltaTime : currentCooldown;


            if (isSeeker)
            {
                // switch to dangerous red
                changeMaterial(alertMaterial);

                if (currentSeekerTimer <= 0.0f)
                {
                    GetComponentInParent<LifeCounter>().TakeDamage(1);
                    resetTimer();
                }
                else
                {
                    currentSeekerTimer -= Time.deltaTime;
                }

                // timer display
                seekerTimer.gameObject.SetActive(true);
                seekerTimer.text = ((int)currentSeekerTimer).ToString();
            } else
            {
                changeMaterial(originalMaterial);
                seekerTimer.gameObject.SetActive(false);
            }

            // Displays teleport use count in UI
            teleportCountDisplay.text = "Teleports: " + numTeleports.ToString();
        } else
        {
            //lobbyStat.gameObject.SetActive(true);
            //lobbyTimer.gameObject.SetActive(false);
            seekerTimer.gameObject.SetActive(false);
        }
    }

    void resetTimer()
    {
        currentSeekerTimer = seekerStartTime;
        currentCooldown = seekerCooldown;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isSeeker && currentCooldown <= 0)
        {
            PlayerLabel otherPlayerLabel = collision.gameObject.GetComponent<PlayerLabel>();

            collision.gameObject.transform.position += collision.gameObject.transform.forward * 10.0f;
            transform.position -= transform.forward;
            Debug.Log("SEEK'D");
            isSeeker = false;
            collision.gameObject.GetComponentInParent<LifeCounter>().TakeDamage(1);
            otherPlayerLabel.isSeeker = true;
            otherPlayerLabel.resetTimer();

            // Displays new statuses for both players
            StartCoroutine(otherPlayerLabel.DisplayStatus());
            StartCoroutine(this.DisplayStatus());
        }
    }

    void changeMaterial(Material mat)
    {
        foreach(GameObject p in playerParts)
        {
            p.GetComponent<MeshRenderer>().material = mat;
        }
    }

    public IEnumerator DisplayStatus()
    {
        if (isSeeker)
        {
            seekerLabel.text = "YOU ARE A SEEKER ";
            seekerLabel.color = Color.red;     // red text
        } else
        {
            seekerLabel.text = "YOU ARE A HIDER ";
            seekerLabel.color = Color.cyan;    // blue text
        }

        seekerLabel.gameObject.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        seekerLabel.gameObject.SetActive(false);
    }
}