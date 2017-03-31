using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    //sound
    public AudioClip bulletImpactWall;
    public AudioClip bulletImpactEnemy;
    private AudioSource source;
    //endsound

    public GameObject owner;
    public float angle;
    public float Speed;
    public float damage = 5;
    DestructableData dd = null;
    public static GameObject create(GameObject parent, GameObject owner, float Angle, float speed)
    {
        Vector3 pos = owner.transform.position;
        pos.x += 1f * Mathf.Cos(Angle);
        pos.z += 1f * Mathf.Sin(Angle);
        pos.y += .25f;
        GameObject obj = PoolManager.Create(parent, pos, owner.transform.rotation);
        Projectile proj = obj.GetComponent<Projectile>();
        proj = proj == null ? obj.AddComponent<Projectile>() : proj;
        proj.owner = owner;
        proj.angle = Angle;
        proj.Speed = speed;
        return obj;
    }
    GameObject Shoot()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)));
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != owner)
            {
                if(hit.distance <= .25f)
                {
                    Debug.Log(hit.collider.gameObject);
                    return hit.collider.gameObject;
                }
            }
        }
        return null;
    }
    void Update()
    {
        //sound
        source = GetComponent<AudioSource>();
        //endsound
        Vector3 pos = transform.position;
        pos.x += Speed * Mathf.Cos(angle) * Time.deltaTime;
        pos.z += Speed * Mathf.Sin(angle) * Time.deltaTime;
        transform.position = pos;
        /*GameObject hit = Shoot();

        if(hit != null)
        {
            if (hit.transform.GetComponent<DestructableData>())
            {
                dd = hit.transform.GetComponent<DestructableData>();
            }
            else if (hit.transform.parent != null)
            {
                if(hit.transform.parent.GetComponent<DestructableData>())
                {
                    dd = hit.transform.parent.GetComponent<DestructableData>();
                }
                else
                {
                    dd = null; 
                }

            }
            else
            {
                dd = null;
            }
        }

        //dd = hit != null ? hit.transform.parent != null ? hit.transform.parent.GetComponent<DestructableData>() : null : null;
        if (dd != null)
        {
            dd.TakeDamage(damage);
        }

        if (hit)
        {
            PoolManager.Destroy(transform.gameObject);
        }*/
    }

    void OnTriggerEnter(Collider col)
    {
        DestructableData hit;
        if (hit = col.GetComponent<DestructableData>())
        {
            //sound
            source.PlayOneShot(bulletImpactEnemy);
            //endsound
            hit.TakeDamage(damage);
            PoolManager.Destroy(transform.gameObject);
        }
        else if(col.transform.name.Contains("Spawner"))
        {
        }
        else
        {
            //sound
            source.PlayOneShot(bulletImpactWall);
            PoolManager.Destroy(transform.gameObject);
        }
    }
}
