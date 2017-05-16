using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour {

    //sound
    public AudioClip bulletImpactWall;
    public AudioClip bulletImpactEnemy;
    private AudioSource source;
    //end sound

	public float damage = 5.0f;					// base damage of bullet
	public float critMultiplier = 2.0f;			// crit multiplier
	public bool canPierceThroughEnemies = true;

	public enum ProjectileType { ANTITANK, ANTIRANGE, ANTISWARM, NORMAL }
	public ProjectileType projectileType;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

	void OnTriggerEnter(Collider col)
	{
		DestructableData hit;
		GameObject owner = GetComponent<Projectile> ().owner; 
		if ((hit = col.GetComponent<DestructableData>()))
		{
			//When the owner is destroyed
			if (owner != null && owner.name == "Player") 
			{
				// Check if player didn't hit themself
				if (col.GetComponent<PlayerController>() == null && col.GetComponent<RescueNPC>() == null) 
				{
					damageEnemy (hit, col);
                    //source.PlayOneShot(bulletImpactEnemy);
					if (!canPierceThroughEnemies) 
					{
						PoolManager.Destroy (transform.gameObject);
					}
				}
			} 
			else //Assume that the Enemy fired the projectile
			{
				if (col.GetComponent<PlayerController>() != null) 
				{
					hit.TakeDamage (damage);
					PoolManager.Destroy(transform.gameObject);
				}
			}

		}
		//NOTE: Probably find a different way to check what can't be passed through by the projectile if the condition gets too long
		else if(col.GetComponent<Spawner>() == null &&
            col.GetComponent<Projectile>() == null &&
            col.GetComponent<SlidingTwoDoor>() == null &&
            col.GetComponent<RescueNPC>() == null &&
            !col.name.Contains("colliderI"))
		{
            //source.PlayOneShot(bulletImpactWall);

			PoolManager.Destroy(transform.gameObject);
		}
	}

	/// <summary>
	/// Logic on how much to damage enemy. Dependent on projectile type and enemy type.
	/// </summary>
	/// <param name="hit">Reference to enemy's Destructable Data</param>
	/// <param name="col">Reference to enemy's </param>
	void damageEnemy(DestructableData hit, Collider col){
		switch (projectileType) {
			case ProjectileType.ANTIRANGE:
				decideToCrit (hit, col.GetComponent<RangedEnemy_AI> ());
				break;
			case ProjectileType.ANTITANK:
				decideToCrit (hit, col.GetComponent<bullChase> ());
				break;
			case ProjectileType.ANTISWARM:
				decideToCrit (hit, col.GetComponent<AlwaysChaseAI> ());
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// Crits enemy if they contain the component
	/// </summary>
	/// <param name="hit">Hit.</param>
	/// <param name="component">Component.</param>
	void decideToCrit (DestructableData hit, Component component)
	{
		if (component != null) 
		{
			Debug.Log ("Crit Damage on " + hit.name);
			hit.TakeDamage (damage * critMultiplier);
		} else 
		{
			hit.TakeDamage (damage);
		}
	}
}
