using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {
    public float viewDist = 5.0f;
    public bool sight = false;

    public float projectileWait = 1;
    public float ProjectileSpeed = 1;
    public GameObject ProjectilePrefab;

    private RaycastHit _hit;
    private Vector3 _playerLocation, _raycastDirection, _startVec;
    private GameObject _player;

	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(EnemyShoot());
	}
	
	// Update is called once per frame
	void Update () {
        _startVec = this.transform.position;
        _playerLocation = _player.transform.position;

        _raycastDirection = _playerLocation - _startVec;
        //Debug.Log("something");
        if(Vector3.Distance(_playerLocation, _startVec) < viewDist)
        {
            //Debug.Log("player within range");
            if (Physics.Raycast(_startVec, _raycastDirection, out _hit, viewDist) && _hit.collider.gameObject.transform.parent.tag == "Player")
            {
               transform.LookAt(_player.transform.position);
               sight = true;
               //Debug.Log("I see the player"); 
            }
            else
            {
                sight = false;
            }    
        }
        else
        {
            sight = false;
        }
	}

    IEnumerator EnemyShoot()
    {
        while (true)
        {
            if (sight)
            {
                Debug.Log("shoot");
                Projectile.create(ProjectilePrefab, transform.gameObject, GetAngle(this.transform.position, _playerLocation), ProjectileSpeed);
                yield return new WaitForSeconds(projectileWait);
            }
            yield return new WaitForSeconds(projectileWait);
        }
    }

    float GetAngle(Vector3 v1, Vector3 v2)
    {
        return Mathf.Atan2(v2.z - v1.z, v2.x - v1.x);
    }
}


