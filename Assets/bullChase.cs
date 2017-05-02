using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MovementEffects;

public class bullChase : MonoBehaviour
{
    public string[] priorityTargetTag = new string[3];
    private GameObject _primary, _secondary, _tertiary, _current;
    private EnemyMovement _movement;
    private RaycastHit checkLineOfSite;
    private bool charging = false;
    private bool hasCharged = false;
    RaycastHit[] hits;

    public float runWait = 3;
    private CoroutineHandle chase;

    private GameObject player;

    void Start()
    {
        _movement = GetComponent<EnemyMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _primary = GameObject.FindGameObjectWithTag(priorityTargetTag[0]);
        _secondary = GameObject.FindGameObjectWithTag(priorityTargetTag[1]);
        _tertiary = GameObject.FindGameObjectWithTag(priorityTargetTag[2]);
        if (_current == null)
        {
            _current = player;
        }
        if (_primary != null)
        {
            _current = _primary;
        }
        else if (_secondary != null)
        {
            _current = _secondary;
        }
        else if (_tertiary != null)
        {
            _current = _tertiary;
        }
        /*if(_vision.alertness == 0)
        {
            //do nothing for now
        }
        if(_vision.alertness >= 1)
        {
            transform.LookAt(_vision.personalLastSighting);
			_movement.ChasePlayer ();
            _weapon.startShooting();
        }*/
        if (GetComponentInChildren<BullAnimationController>().dead)
        {
            GetComponent<NavMeshAgent>().speed = 0;
        }
        if (!charging && !GetComponentInChildren<BullAnimationController>().dead)
        {
            Vector3 Target = player.transform.position - transform.position;
            float step = Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, Target, step, 0);
            transform.rotation = Quaternion.LookRotation(newDir);
            //Physics.Raycast(transform.position - new Vector3(0,0.5f,0), transform.forward, out hit, 100);
            //Debug.DrawRay(transform.position, transform.forward , Color.red);
            hits = Physics.RaycastAll(new Ray(transform.position - new Vector3(0, 0.5f, 0), transform.forward), 100);

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.gameObject == player && !hasCharged)
                {
                    GetComponent<NavMeshAgent>().speed = 15;
                    charging = true;
                    _movement.ChasePoint(hit.transform.position + (hit.transform.position - transform.position) / 10);
                    chase = Timing.RunCoroutine(Chase());
                }
            }
        }
        


    }

    IEnumerator<float> Chase()
    {
        GetComponentInChildren<BullAnimationController>().Run();
        yield return Timing.WaitForSeconds(runWait);
        GetComponentInChildren<BullAnimationController>().StopRun();
        GetComponent<NavMeshAgent>().speed = 0;
        charging = false;
        hasCharged = true;
        yield return Timing.WaitForSeconds(0.5f);
        hasCharged = false;
    }

    void OnDestroy()
    {
        Timing.KillCoroutines(chase);
    }
}