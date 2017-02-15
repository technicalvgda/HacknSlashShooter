using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {
    public float projectileWait = 1;
    public float ProjectileSpeed = 1;
    public GameObject ProjectilePrefab;
    private EnemyVision vision;
    private bool shooting;
    private GameObject player;

    // Use this for initialization
    void Start () {
        vision = GetComponent<EnemyVision>();
        shooting = false;
        player = GameObject.FindGameObjectWithTag("Player");
	}

    IEnumerator EnemyShoot()
    {
        while (vision.sight)
        {
            Debug.Log("shoot");
            Projectile.create(ProjectilePrefab, transform.gameObject, GetAngle(this.transform.position, player.transform.position), ProjectileSpeed);
            yield return new WaitForSeconds(projectileWait);
        }
        shooting = false;

    }

    float GetAngle(Vector3 v1, Vector3 v2)
    {
        return Mathf.Atan2(v2.z - v1.z, v2.x - v1.x);
    }

    public void startShooting()
    {
        if (!shooting)
        {
            StartCoroutine(EnemyShoot());
            shooting = true;
        }
    }
}
