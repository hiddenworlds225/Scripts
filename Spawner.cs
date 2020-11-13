using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    private Transform pos;
    public float spawnTimer = 0f;
    public float nextSpawn = 0f;
    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
        nextSpawn = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        nextSpawn -= Time.deltaTime;
        if(nextSpawn <= 0)
        {
            Spawn();
            nextSpawn = spawnTimer;
        }
    }


    void Spawn()
    {
        Instantiate(enemy, pos.position, pos.rotation);
    }
}
