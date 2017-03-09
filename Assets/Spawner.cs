using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class Spawner : MonoBehaviour {
    public GameObject enemy;
    public float spawnWait = 5;
    private float maxX, maxZ, minX, minZ;
    public bool wave = false;
    public int numberOfWaves = 1;
    private int currentWave = 0;
    private List<GameObject> WaveEnemies;
    private GameObject player;
    private List<GameObject> WaveList;


	// Use this for initialization
	void Start () {
        WaveEnemies = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
        maxX = transform.position.x + transform.localScale.x / 2;
        maxZ = transform.position.z + transform.localScale.z / 2;
        minX = transform.position.x - transform.localScale.x / 2;
        minZ = transform.position.z - transform.localScale.z / 2;
        WaveList = new List<GameObject>();
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (wave)
            {
                Timing.RunCoroutine(WaveSpawn());
            }
            else
            {
                Timing.RunCoroutine(Spawn(), "spawn");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (wave)
            {
                WaveEnemies = new List<GameObject>();
                Timing.RunCoroutine(WaveSpawn());
            }
            else
            {
                Timing.KillCoroutines("spawn");
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator<float> Spawn()
    {
        while (true)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
            while(!(Vector3.Distance(player.transform.position, spawnPoint) < 5))
            {
                Instantiate(enemy, spawnPoint, Quaternion.identity);
                yield return Timing.WaitForSeconds(spawnWait);// wait projectile wait time before firing again
                break;
            }
        }
    }

    IEnumerator<float> WaveSpawn()
    {
        do
        {
            Debug.Log("WaveSpawn start");
            for (int i = 0; i < (5); i++)
            {
                WaveList.Add(Instantiate(enemy, new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ)), Quaternion.identity));
                Debug.Log("Spawn " + i);
                Timing.WaitForSeconds(1);// wait projectile wait time before firing again
                Debug.Log("return");
            }
            Debug.Log("exitted 4 loop");
            //while (WaveList.Count > 0)
            //{
               // yield return Timing.WaitForSeconds(spawnWait);// wait projectile wait time before firing again
            //}
            currentWave++;
            WaveList.Clear();
        } while (currentWave != numberOfWaves);
        yield return Timing.WaitForSeconds(spawnWait); 

    }
}
