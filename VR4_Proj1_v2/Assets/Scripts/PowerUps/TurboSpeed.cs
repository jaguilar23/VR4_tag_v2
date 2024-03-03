using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurboSpeed : MonoBehaviour
{
    public float speedMultiplier = 2f; // Multiplier for player's walk speed
    public float duration = 5f; // Duration of speed boost
    private float originalSpeed;
    private bool boostActive = false;

    [SerializeField]
    private GameObject disableBoost = null;

    private MeshRenderer myRenderer;
    private SphereCollider myCollider;

    private void Start()
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
        MultiplayerMovement multiplayerMovement = collision.gameObject.GetComponent<MultiplayerMovement>();
        if (collision.gameObject.CompareTag("Player")) // If collided object is the player
        {
            Debug.Log("collide");
            if (!boostActive) // Check if boost is not already active
            {
                StartCoroutine(BoostSpeed(multiplayerMovement));
                Debug.Log("Speed");
            }
            // "Delete" powerup
            myRenderer.enabled = false;
            myCollider.enabled = false;
        }

        
    }

    IEnumerator BoostSpeed(MultiplayerMovement multiplayerMovement)
    {

        Debug.Log("Start Couroutine");
        boostActive = true;
        ActivateTurboSpeed(multiplayerMovement);
        // playerController.walkSpeed += speedMultiplier; // Double the player's walk speed
        yield return new WaitForSeconds(duration);
        DeactivateTurboSpeed(multiplayerMovement);
        // playerController.walkSpeed = 14; // Reset the player's walk speed 
        boostActive = false;
    }

    private void ActivateTurboSpeed(MultiplayerMovement multiplayerMovement)
    {
        multiplayerMovement.movementSpeed *= speedMultiplier;
    }

    private void DeactivateTurboSpeed(MultiplayerMovement multiplayerMovement)
    {
        multiplayerMovement.movementSpeed /= speedMultiplier;
    }
}