using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class RangedMovement : MonoBehaviour {
    public float minRadius = 5, maxRadius = 10;
    public List<Vector3> _potentialList = new List<Vector3>();
    private int _tries = 10;
    private Vector3 _potentialLoc;
    private Vector3 _attackLoc;
    private RaycastHit _hit;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    //normal movement, no attacks
    public void NormalMove()
    {
        //looping incase we don't find a suitable spot, chances are very low.
        for (int i = 0; i < _tries; i++)
        {
            _potentialLoc = _player.transform.position;
            //random point on a a circle with Radius between min and max
            float r = Random.Range(minRadius, maxRadius);
            float theta = Random.Range(0, 360);
            
            _potentialLoc.y += 100.0f;                //make the height really tall so we can scan down
            _potentialLoc.x += r * Mathf.Cos(theta);
            _potentialLoc.z += r * Mathf.Sin(theta); //shift the y to the z, since 3d circles use y for height
            RaycastHit[] hits;
            hits = Physics.RaycastAll(_potentialLoc, Vector3.down);
            /*if (Physics.Raycast(_potentialLoc, Vector3.down, out _hit))
            {
                if (_hit.transform.gameObject.tag == "Ground")
                {
                    //_potentialList.Add(_potentialLoc);
                    transform.position = new Vector3(_potentialLoc.x, _hit.point.y + 0.5f, _potentialLoc.z);
                    i = _tries + 2;
                    break;
                }
            }*/
            foreach (RaycastHit rh in hits)//switched from a single raycast to a raycast all since the raycast will be hitting the spawners and any other collider
            {
                if(rh.transform.gameObject.tag == "Ground")
                {
                    transform.position = new Vector3(_potentialLoc.x, 0.33f, _potentialLoc.z);
                    i = _tries + 2;
                    break;
                }
            }
        }

        
    }
    //attacking the player, moves to the minimum distance
    public void AttackMove()
    {
        for (int i = 0; i < _tries; i++)
        {
            _potentialLoc = _player.transform.position;
            //random point on a a circle with Radius between min and max
            _attackLoc = Random.insideUnitCircle.normalized * minRadius; //makes it always on the edge of the circle

            _potentialLoc.y += 100.0f;                //make the height really tall so we can scan down
            _potentialLoc.x += _attackLoc.x;
            _potentialLoc.z += _attackLoc.y; //shift the y to the z, since 3d circles use y for height
            if (Physics.Raycast(_potentialLoc, Vector3.down, out _hit))
            {
                if (_hit.transform.gameObject.tag == "Ground")
                {
                    //_potentialList.Add(_potentialLoc);
                    transform.position = new Vector3(_potentialLoc.x, _hit.point.y, _potentialLoc.z);
                    i = _tries + 2;
                    break;
                }
            }
        }
    }
}
