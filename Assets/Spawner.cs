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
    CoroutineHandle spawn;
    public bool waveStart = false;

    //temporary
    public GameObject door1;
    public GameObject door2;

    public GameObject NPC;
    private RescueNPC NPCScript;

    //Wave Spawn Variables
    public bool wave = false;
    public int numberOfWaves = 1;
    private int currentWave = 1;
    private PlayerController player;
    private int numEnemies;
    public bool arenaSpawn;


	// Use this for initialization
	void Start () {
        if (NPC != null)
        {
            NPCScript = NPC.GetComponent<RescueNPC>();
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        maxX = transform.position.x + transform.localScale.x / 2;
        maxZ = transform.position.z + transform.localScale.z / 2;
        minX = transform.position.x - transform.localScale.x / 2;
        minZ = transform.position.z - transform.localScale.z / 2;
        numEnemies = 0;
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
            if (NPC != null) {
                NPC.tag = "Objective";
            }
            if (wave && !waveStart)
            {
                Debug.Log("Wave spawner not done yet");
                Timing.RunCoroutine(WaveSpawn(numberOfWaves));
            }
            else
            {
                spawn = Timing.RunCoroutine(Spawn());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (wave && !waveStart)
            {
                
            }
            else
            {
                Timing.KillCoroutines(spawn);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("player killed: " + player.numKilled + " total enemies: " + numEnemies);

    }

    IEnumerator<float> Spawn()
    {
        while (true)
        {
            float spawn = Random.Range(0, (enemy1spawn + enemy2spawn + enemy3spawn));

            if (spawn < enemy1spawn)
            {
                SpawnEnemy(enemy1, false);
            }
            else if (spawn < enemy2spawn+enemy1spawn && enemy2 != null)
            {
                SpawnEnemy(enemy2, false);
            }
            else if (spawn < enemy3spawn + enemy2spawn + enemy1spawn && enemy3 != null)
            {
                SpawnEnemy(enemy3, false);
            }
            else
            {
                SpawnEnemy(enemy1, false);
            }
                
            yield return Timing.WaitForSeconds(spawnWait);// wait projectile wait time before firing again
            
            
        }
    }

    IEnumerator<float> WaveSpawn(int waves)
    {
        waveStart = true;
        player.numKilled = 0;
        Debug.Log("WaveSpawn start");
        int numOfEnemy1 = 0;
        int numOfEnemy2 = 0;
        int numOfEnemy3 = 0;
        if (door1 != null)
        {
            door1.GetComponentInChildren<SlidingTwoDoor>().isLocked = true;
            door2.GetComponentInChildren<SlidingTwoDoor>().isLocked = true;
        }
        while (currentWave <= numberOfWaves || arenaSpawn)
        {
            if(enemy1spawn > 0)
            {
                numOfEnemy1 = 7 * currentWave;
                numEnemies += numOfEnemy1;
            }
            if(enemy2spawn > 0)
            {
                numOfEnemy2 = 2 * currentWave;
                numEnemies += numOfEnemy2;
            }
            if(enemy3spawn > 0)
            {
                numOfEnemy3 = 1 * currentWave;
                numEnemies += numOfEnemy3;
            }
            while(numOfEnemy1 > 0 || numOfEnemy2 > 0 || numOfEnemy3 > 0)
            {
                //Debug.Log("enemy1: " + numOfEnemy1 + " enemy2: " + numOfEnemy2 + " enemy3: " + numOfEnemy3);
                bool successfulsummon = false;
                while (!successfulsummon)
                {
                    int x = Random.Range(1, 100);
                    if(x <= 70)
                    {
                        if (numOfEnemy1 > 0)
                        {
                            SpawnEnemy(enemy1, true);
                            numOfEnemy1--;
                            successfulsummon = true;
                        }
                    }
                    else if ( x > 70 && x <= 90)
                    {
                        if (numOfEnemy2 > 0)
                        {
                            SpawnEnemy(enemy2, true);
                            numOfEnemy2--;
                            successfulsummon = true;
                        }
                    }
                    else if ( x > 90)
                    {
                        if (numOfEnemy3 > 0)
                        {
                            SpawnEnemy(enemy3, true);
                            numOfEnemy3--;
                            successfulsummon = true;
                        }
                    }
                }
                yield return Timing.WaitForSeconds(spawnWait);
            }


            while(player.numKilled < numEnemies)
            {
                //Debug.Log("player killed: " + player.numKilled + " total enemies: " + numEnemies);
                yield return Timing.WaitForSeconds(spawnWait);
            }
            currentWave++;
        }
        if (door1 != null)
        {
            if(NPC != null)
            {
                NPCScript.rescued = true;
            }
            door1.GetComponentInChildren<SlidingTwoDoor>().isLocked = false;
            door2.GetComponentInChildren<SlidingTwoDoor>().isLocked = false;
        }
    }

    private void SpawnEnemy(GameObject enemyToSpawn, bool waveEnemy)
    {
        Vector3 spawnPoint = new Vector3(Random.Range(minX, maxX), 0.33f, Random.Range(minZ, maxZ));
        while ((Vector3.Distance(player.gameObject.transform.position, spawnPoint) < 5))
        {
            spawnPoint = new Vector3(Random.Range(minX, maxX), 0.33f, Random.Range(minZ, maxZ));
        }
        GameObject enemy = Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
        if (waveEnemy)
        {
            enemy.AddComponent<WaveEnemy>();
        }
        
    }
}
