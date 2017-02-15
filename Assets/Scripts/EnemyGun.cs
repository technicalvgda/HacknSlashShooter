using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {
    public float projectileWait = 1;//1 second intervals
    public float ProjectileSpeed = 1;//shoots with force of 1 
    public GameObject ProjectilePrefab;

    private EnemyVision vision;
    private bool shooting;
    private GameObject player;

    // Use this for initialization
    void Start () {
        vision = GetComponent<EnemyVision>(); // get vision component for the enemy
        shooting = false; //initializes shooting boolean to not shooting
        player = GameObject.FindGameObjectWithTag("Player"); //finds the player game object
	}

    IEnumerator EnemyShoot()
    {
        while (vision.sight)//while enemy has sight continue to shoot
        {
            Projectile.create(ProjectilePrefab, transform.gameObject, GetAngle(this.transform.position, player.transform.position), ProjectileSpeed); //fire bullet towards player
            yield return new WaitForSeconds(projectileWait);// wait projectile wait time before firing again
        }
        shooting = false;

    }

    float GetAngle(Vector3 v1, Vector3 v2)
    {
        return Mathf.Atan2(v2.z - v1.z, v2.x - v1.x);
    }

    public void startShooting()//function call to start coroutine
    {
        if (!shooting)//if enemy is already shooting do nothing
        {
            StartCoroutine(EnemyShoot()); //start shooting
            shooting = true; //set shooting boolean to true
        }
    }
}
