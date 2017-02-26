using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class EnemyGun : MonoBehaviour {
    public float projectileWait = 0.03f;//1 second intervals
    public float ProjectileSpeed = 5.0f;//shoots with force of 1 
    public GameObject ProjectilePrefab;

    private bool canfire;
    private GameObject player;

    // Use this for initialization
    void Start () {
        canfire = true; //initializes shooting boolean to not shooting
        player = GameObject.FindGameObjectWithTag("Player"); //finds the player game object
	}

    

    float GetAngle(Vector3 v1, Vector3 v2)
    {
        return Mathf.Atan2(v2.z - v1.z, v2.x - v1.x);
    }

    public void startShooting()//function call to start coroutine
    {
        if (canfire)//if enemy is already shooting do nothing
        {
            canfire = false;
            Timing.RunCoroutine(EnemyShoot()); //start shooting
            
        }
    }

    IEnumerator<float> EnemyShoot()
    {
        Projectile.create(ProjectilePrefab, transform.gameObject, GetAngle(this.transform.position, player.transform.position), ProjectileSpeed); //fire bullet towards player
        yield return Timing.WaitForSeconds(projectileWait);// wait projectile wait time before firing again
        canfire = true; //set shooting boolean to true
    }
}
