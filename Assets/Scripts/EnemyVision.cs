using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour
{
    public float FOVangle = 110.0f; //Edge of vision cone
    public float alertAngle = 90.0f; //directly in sight
    public float sightDistance = 20.0f; //edge of view
    public float alertDistance = 5.0f; //directly in sight
    [Range(0,2)]
    public int alertness; //how aware of the player this enemy is, used by AI handler to determine actions. levels [0,2]

    public Vector3 personalLastSighting; //where did we last see the player?

    private GameObject _player;
    private RaycastHit _hit;

    // Use this for initialization
    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        alertness = 0;
    }

    // Update is called once per frame
    void Update()
    {
        alertness = CanSeeTarget(_player);
    }


    public int CanSeeTarget(GameObject target)
    {
        float heightOfPlayer = 0.5f;

        Vector3 startVec = transform.position;
        startVec.y += heightOfPlayer;
        Vector3 startVecFwd = transform.forward;
        startVecFwd.y += heightOfPlayer;
        if (target != null) {
            Vector3 rayDirection = target.transform.position - startVec;
            // Detect entities in view
            if ((Vector3.Angle(rayDirection, startVecFwd)) < FOVangle && Physics.Raycast(startVec, rayDirection, out _hit, sightDistance)) {
                //Make sure it was the player
                if (_hit.collider.gameObject == target) {
                    personalLastSighting = target.transform.position;
                    //They are very close
                    if ((Vector3.Angle(rayDirection, startVecFwd)) < alertAngle && (Vector3.Distance(startVec, target.transform.position) <= alertDistance)) {
                        return 2;
                    }
                    return 1;
                }
                //didn't hit the player
                else {
                    //Debug.Log("Can not see player");
                    return 0;
                }
            }
        }
        //looking at nothing
        return 0;
    }
}