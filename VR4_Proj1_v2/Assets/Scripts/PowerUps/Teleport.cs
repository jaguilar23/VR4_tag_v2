using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Teleport : MonoBehaviour
{
    public Vector3 teleportPosition;
    public float teleportDistance = 3.0f; // Distance to teleport
    private MeshRenderer myRenderer;
    private BoxCollider myCollider;

    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        myCollider = GetComponent<BoxCollider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // If collided object is the player
        {
            PlayerLabel playerStats = collision.gameObject.GetComponent<PlayerLabel>();
            Debug.Log("collide");
            playerStats.numTeleports++;
            //PowerUpCollector.instance.AddScore();

            //Vector3 teleportPosition = playerTransform.position + playerTransform.forward * teleportDistance;
            //playerTransform.position = teleportPosition;
            myRenderer.enabled = false;
            myCollider.enabled = false;
            //Destroy(this.gameObject);

        }
    }
}