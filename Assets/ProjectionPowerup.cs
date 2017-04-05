using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;
using DG.Tweening;

public class ProjectionPowerup : MonoBehaviour {
    public float destructTimer = 8.0f;
    public float damage;
    public float radius;

    private bool canExplode = false;

    public void Activate(Vector3 target)
    {
        transform.DOJump(target, 0.75f, 1, 0.65f, false);
        Timing.RunCoroutine(Timer());
    }

    private IEnumerator<float> Timer()
    {
        yield return Timing.WaitForSeconds(destructTimer);
        canExplode = true;
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider c in hit)
        {
            if(c.gameObject.tag == "Enemy")
            {
                c.gameObject.GetComponent<DestructableData>().TakeDamage(damage);
            }
        }
        Destroy(this.gameObject);
    }
}
