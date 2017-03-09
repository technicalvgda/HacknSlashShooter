using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class Spawner : MonoBehaviour {
    public GameObject enemy;
    public float spawnWait = 5;
    private float maxX, maxZ, minX, minZ;
	// Use this for initialization
	void Start () {
        maxX = transform.position.x + transform.localScale.x / 2;
        maxZ = transform.position.z + transform.localScale.z / 2;
        minX = transform.position.x - transform.localScale.x / 2;
        minZ = transform.position.z - transform.localScale.z / 2;
        Timing.RunCoroutine(Spawn());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator<float> Spawn()
    {
        while (true)
        {
            Instantiate(enemy, new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ)), Quaternion.identity);
            yield return Timing.WaitForSeconds(spawnWait);// wait projectile wait time before firing again

        }

    }
}
