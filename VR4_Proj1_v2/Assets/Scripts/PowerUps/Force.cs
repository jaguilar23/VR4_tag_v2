using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    private MeshRenderer myRenderer;
    private SphereCollider myCollider;

    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        myCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        // rotate around and about
        transform.Rotate(transform.up * 60 * Time.deltaTime, Space.Self);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // If collided object is the player
        {
            PlayerLabel playerStats = collision.gameObject.GetComponent<PlayerLabel>();

            GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player"); ; // every player that didn't grab powerup
            GameObject[] excludedPlayerList = new GameObject[playerList.Length - 1];

            int exListCount = 0;
            // fills
            for (int i = 0; i < playerList.Length; i++)
            {
                if (playerList[i].transform.GetInstanceID().ToString() != collision.gameObject.transform.GetInstanceID().ToString())
                {
                    excludedPlayerList[exListCount] = playerList[i];
                    exListCount++;
                }
            }

            foreach (GameObject go in excludedPlayerList)
            {
                StartCoroutine(ForceTime(collision.gameObject, go, playerStats.isSeeker));
            }

            // "Delete" powerup
            myRenderer.enabled = false;
            myCollider.enabled = false;
        }
    }

    private Vector3 GetDirection(Vector3 start, Vector3 dest)
    {
        Vector3 dir = (start - dest).normalized;
        return dir;
    }

    IEnumerator ForceTime(GameObject collector, GameObject target, bool isCollectorSeeker)
    {
        for (int i = 0; i < 500; i++)
        {
            Vector3 moveDirection = GetDirection(collector.transform.position, target.transform.position); // distance between players
            Debug.Log(moveDirection);

            if (isCollectorSeeker)
            {
                target.GetComponent<Rigidbody>().MovePosition(target.GetComponent<Rigidbody>().position + (moveDirection * 0.2f));
            }
            else
                target.GetComponent<Rigidbody>().MovePosition(target.GetComponent<Rigidbody>().position + (-moveDirection * 0.2f));

            yield return new WaitForSeconds(.01f);
        }
    }
}