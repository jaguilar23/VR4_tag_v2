using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    public GameObject[] myObjects;

    public float SpawningTime;

    private void Start()
    {
        SpawningTime = Random.Range(1.0f, 3.5f);
    }

    void SpawnNow()
    {
        int randomIndex = Random.Range(0, myObjects.Length);
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-62.5f, 62.5f), Random.Range(-62.5f, 62.5f), Random.Range(-62.5f, 62.5f));

        Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        SpawningTime -= Time.deltaTime;

        if (SpawningTime <= 0 )
        {

            SpawnNow();
            SpawningTime = Random.Range(1.0f, 3.5f);
        }
    }
}
