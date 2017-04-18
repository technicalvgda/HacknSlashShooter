using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public GameObject owner;
    public float angle;
    public float Speed;
	public Vector3 vel;

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
		//proj.GetComponent<Rigidbody>().velocity = owner.transform.TransformDirection (Vector3.right * speed) + owner.GetComponent<CharacterController>().velocity;
        return obj;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += Speed * Mathf.Cos(angle) * Time.deltaTime;
        pos.z += Speed * Mathf.Sin(angle) * Time.deltaTime;
        transform.position = pos;

    }

}
