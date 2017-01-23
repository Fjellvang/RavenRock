using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkSpawner : MonoBehaviour {

    public float spawnTime = 5f;
    float origSpawntime;

    public GameObject toSpawn;

	// Use this for initialization
	void Start () {
        origSpawntime = spawnTime;
	}
	
	// Update is called once per frame
	void Update () {
        spawnTime -= Time.deltaTime;
        if(spawnTime < 0)
        {
            Instantiate(toSpawn, transform.position, transform.rotation);
            spawnTime = origSpawntime;
        }
	}
}
