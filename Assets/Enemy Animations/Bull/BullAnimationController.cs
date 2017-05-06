using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAnimationController : MonoBehaviour {

    public Animator anim;
    public bool dead = false;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    void LateUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && dead)
        {
            Destroy(transform.parent.gameObject);
        }
    }
	
    public void Run()
    {
        anim.Play("Running", -1, 0f);
    }

    public void StopRun()
    {
        anim.Play("Idle");
    }

    public void Die()
    {
        anim.Play("Dead",-1,0f);
        dead = true;
    }
	
}
