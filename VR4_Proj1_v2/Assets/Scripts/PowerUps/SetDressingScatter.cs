using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDressingScatter : MonoBehaviour
{

    public GameObject[] myObjects;


    private void Start()
    {
        SpawnNow();
       
    }

    void SpawnNow()
    {
        int randomIndex = Random.Range(0, myObjects.Length);
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-62.5f, 62.5f), Random.Range(-62.5f, 62.5f), Random.Range(-62.5f, 62.5f));

        Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
    }

  
}
