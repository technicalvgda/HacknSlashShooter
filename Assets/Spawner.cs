﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject WinScreen;

    //Wave Spawn Variables
    public bool wave = false;
    public int numberOfWaves = 1;
    public int currentWave = 1;
    private PlayerController player;
    public int numEnemies;
    public bool arenaSpawn;

    public CountdownTimer cdTime;
    public float startDelay = 10.0f;
    private string playerTag = "Player", objectTag = "Objective";
    private bool finished = false;

	// Use this for initialization
	void Start () {
        SetAggro(1);
        if (NPC != null)
        {
            NPCScript = NPC.GetComponent<RescueNPC>();
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        maxX = (transform.position.x + transform.localScale.x / 2) - 0.5f;
        maxZ = (transform.position.z + transform.localScale.z / 2) - 0.5f;
        minX = (transform.position.x - transform.localScale.x / 2) + 0.5f;
        minZ = (transform.position.z - transform.localScale.z / 2) + 2.5f;
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
        if (other.transform.tag == "Player" && !finished)
        {
            if (NPC != null) {
                NPC.tag = "Objective";
            }
            if (wave && !waveStart)
            {
                Debug.Log("Wave spawner not done yet");
                Timing.RunCoroutine(WaveSpawn(numberOfWaves));
            }
            if (wave)
            {
                if (door1 != null)
                {
                    door1.GetComponentInChildren<SlidingTwoDoor>().isLocked = true;
                    door2.GetComponentInChildren<SlidingTwoDoor>().isLocked = true;
                }
            }
            else
            {
                spawn = Timing.RunCoroutine(Spawn());
            }
            if(cdTime != null)
            {
                cdTime.StartTimer(startDelay, this);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (wave)
            {
                if (door1 != null)
                {
                    door1.GetComponentInChildren<SlidingTwoDoor>().isLocked = false;
                    door2.GetComponentInChildren<SlidingTwoDoor>().isLocked = false;
                }
            }
            else
            {
                Timing.KillCoroutines(spawn);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("player killed: " + player.numKilled + " total enemies: " + numEnemies);

    }

    public void SetAggro(int state)
    {
        string[] targetList;

        if (state == 1)
        {
            targetList = new string[] { "Pheremone", playerTag, objectTag };
        }
        else
        {
            targetList = new string[] { "Pheremone", objectTag, playerTag };
        }

        //really gross, but it should work
        if (enemy1 != null)
        {
            enemy1.GetComponent<AlwaysChaseAI>().priorityTargetTag = targetList;
        }
        if (enemy3 != null)
        {
            enemy3.GetComponent<bullChase>().priorityTargetTag = targetList;
        }
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
        while (currentWave <= numberOfWaves)
        {
            if(enemy1spawn > 0)
            {
                if (currentWave < 5)
                {
                    numOfEnemy1 = 15;
                }else
                {
                    numOfEnemy1 = 5 * currentWave;
                }
                numEnemies += numOfEnemy1;
            }
            if(enemy2spawn > 0)
            {
                if (currentWave < 3)
                {
                    numOfEnemy2 = 3;
                }else if( currentWave < 6)
                {
                    numOfEnemy2 = 4;
                }
                else
                {
                    numOfEnemy2 = 2 * currentWave;
                }
                numEnemies += numOfEnemy2;
            }
            if(enemy3spawn > 0)
            {
                if (currentWave < 7)
                {
                    numOfEnemy3 = 4;
                } else
                {
                    numOfEnemy3 = 1 * currentWave;
                }
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
                NPCScript.SetRescue(true);
                finished = true;
            }
            door1.GetComponentInChildren<SlidingTwoDoor>().isLocked = false;
            door2.GetComponentInChildren<SlidingTwoDoor>().isLocked = false;
        }
        if (cdTime != null)
        {
            cdTime.StopTimer();
        }
        if(SceneManager.GetActiveScene().name.Contains("Arena"))
        {
            WinScreen.SetActive(true);
        }
    }

    private void SpawnEnemy(GameObject enemyToSpawn, bool waveEnemy)
    {
        Vector3 spawnPoint = new Vector3(Random.Range(minX, maxX), 0.33f, Random.Range(minZ, maxZ));
        if (enemyToSpawn == enemy2)
        {
            while ((Vector3.Distance(player.gameObject.transform.position, spawnPoint) < 4) && (Vector3.Distance(player.gameObject.transform.position, spawnPoint) > 7))
            {
                spawnPoint = new Vector3(Random.Range(minX, maxX), 0.33f, Random.Range(minZ, maxZ));
            }
        }
        else
        {
            while ((Vector3.Distance(player.gameObject.transform.position, spawnPoint) < 5))
            {
                spawnPoint = new Vector3(Random.Range(minX, maxX), 0.33f, Random.Range(minZ, maxZ));
            }
        }
        
        GameObject enemy = Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
        if (waveEnemy)
        {
            enemy.AddComponent<WaveEnemy>();
        }
        
    }
}
