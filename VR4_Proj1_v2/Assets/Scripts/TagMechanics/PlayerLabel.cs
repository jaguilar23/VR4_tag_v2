using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLabel : MonoBehaviour
{
    public bool isSeeker;
    public float waitTime = 7.0f;
    public float designateSeekerTimer = 0.0f;
    float seekerStartTime = 120.0f;
    public float currentSeekerTimer;

    public int numTeleports = 0;
    public float teleportDistance = 15.0f;
    private MeshRenderer myRenderer;

    public GameObject playerTeleportUI;
    [SerializeField] TextMeshProUGUI teleportCountDisplay;
    [SerializeField] TextMeshProUGUI seekerLabel;
    [SerializeField] TextMeshProUGUI seekerTimer;

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

            // timer display
            seekerTimer.gameObject.SetActive(true);
            seekerTimer.text = ((int)currentSeekerTimer).ToString();
        } else
        {
            seekerTimer.gameObject.SetActive(false);
        }

        // Displays teleport use count in UI
        teleportCountDisplay.text = "Teleports: " + numTeleports.ToString();
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

    public IEnumerator DisplayStatus()
    {
        if (isSeeker)
        {
            seekerLabel.text = "You are the SEEKER ";
        } else
        {
            seekerLabel.text = "You are a HIDER ";
        }

        seekerLabel.gameObject.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        seekerLabel.gameObject.SetActive(false);
    }
}