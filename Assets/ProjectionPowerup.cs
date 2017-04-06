using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;
using DG.Tweening;

public class ProjectionPowerup : MonoBehaviour {
    public GameObject decoyModel; //Temporary, this where the model object goes so we can spawn one in
    public float destructTimer = 8.0f;
    public float damage;
    public float radius;

    private bool canExplode = false;
    private float _armTime = 0.75f;
    private Vector3 _floatHeight = new Vector3(0, 0.5f, 0);
    public void Activate(Vector3 target)
    {
        transform.DOJump(target, 0.75f, 1, 0.65f, false);
        Timing.RunCoroutine(Delay());
        Timing.RunCoroutine(Timer());
    }

    private IEnumerator<float> Delay()
    {
        yield return Timing.WaitForSeconds(_armTime);
        Instantiate(decoyModel, transform.position + _floatHeight, transform.rotation, this.transform);
        this.tag = "Pheremone";
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
