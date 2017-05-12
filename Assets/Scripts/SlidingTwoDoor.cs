using System;
using DG.Tweening;
using UnityEngine;

public class SlidingTwoDoor : MonoBehaviour, Door {
    public Transform left, right;
    public bool isLocked = false;
    public bool oneWay = false;
    public bool open = false;
    public float openingWidth;
    private float _leftX, _rightX;
	// Use this for initialization
	void Start () {
        _leftX = left.localPosition.x;
        _rightX = right.localPosition.x;
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player" && !isLocked)
        {
            Open();
            open = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if(open && isLocked)
        {
            Close();
        }
        if (c.gameObject.tag == "Player" && !isLocked)
        {
            Close();
            if (oneWay)
            {
                isLocked = true;
            }
        }
    }
    //slides door open
    public void Open()
    {
        left.DOLocalMoveX(_leftX - openingWidth / 2, 2.0f, false);
        right.DOLocalMoveX(_rightX + openingWidth / 2, 2.0f, false);
    }
    //slides it back to where it used to be
    public void Close()
    {
        open = false;
        left.DOLocalMoveX(_leftX, 2.0f, false);
        right.DOLocalMoveX(_rightX, 2.0f, false);
    }

    public void Unlock()
    {
        isLocked = false;
    }

    public void Lock()
    {
        isLocked = true;
    }
}
