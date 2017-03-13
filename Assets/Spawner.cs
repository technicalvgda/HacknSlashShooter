using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class Spawner : MonoBehaviour {
    public GameObject enemy1;//most likely to spawn
    public float enemy1spawn = 60f;
    public GameObject enemy2;//medium likely to spawn
    public float enemy2spawn = 25f;
    public GameObject enemy3;//least likely to spawn 
    public float enemy3spawn = 15f;
    
    public float spawnWait = 5;//time inbetween each spawn
    private float maxX, maxZ, minX, minZ;//the bound in which the enemy can spawn

    //Wave Spawn Variables
    public bool wave = false;
    public int numberOfWaves = 1;
    private int currentWave = 0;
    private GameObject player;
    private List<GameObject> WaveList;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        maxX = transform.position.x + transform.localScale.x / 2;
        maxZ = transform.position.z + transform.localScale.z / 2;
        minX = transform.position.x - transform.localScale.x / 2;
        minZ = transform.position.z - transform.localScale.z / 2;
        WaveList = new List<GameObject>();
        if(enemy2 == null)
        {
            enemy2spawn = 0;
        }
        if (enemy3 == null)
        {
            enemy3spawn = 0;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (wave)
            {
                Debug.Log("Wave spawner not done yet");
                //Timing.RunCoroutine(WaveSpawn());
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
            float spawn = Random.Range(0, (enemy1spawn + enemy2spawn + enemy3spawn));
            while (!(Vector3.Distance(player.transform.position, spawnPoint) < 5))
            {
                
                if(spawn < enemy1spawn)
                {
                    Instantiate(enemy1, spawnPoint, Quaternion.identity);
                }
                else if (spawn < enemy2spawn+enemy1spawn && enemy2 != null)
                {
                    Instantiate(enemy2, spawnPoint, Quaternion.identity);
                }
                else if (spawn < enemy3spawn + enemy2spawn + enemy1spawn && enemy3 != null)
                {
                    Instantiate(enemy3, spawnPoint, Quaternion.identity);
                }
                else
                {
                    Instantiate(enemy1, spawnPoint, Quaternion.identity);
                }
                

               
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
                //WaveList.Add(Instantiate(enemy, new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ)), Quaternion.identity));
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
