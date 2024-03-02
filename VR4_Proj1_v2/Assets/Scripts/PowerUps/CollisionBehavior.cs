using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionBehavior : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Teleport"))
        {
            Debug.Log("u there bro");
            PowerUpCollector.instance.AddScore();
            Destroy(other.gameObject);
            //ddScore();
            //  Awake();

        }

    }
}