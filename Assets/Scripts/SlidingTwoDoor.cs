using DG.Tweening;
using UnityEngine;

public class SlidingTwoDoor : MonoBehaviour {
    public Transform left, right;
    public bool isLocked = false;
    public bool oneWay = false;
    public float openingWidth;
    private float _leftX, _rightX;
	// Use this for initialization
	void Start () {
        _leftX = left.position.x;
        _rightX = right.position.x;
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player" && !isLocked)
        {
            Open();
        }
    }

    void OnTriggerExit(Collider c)
    {
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
        left.DOMoveX(_leftX - openingWidth/2, 2.0f, false);
        right.DOMoveX(_rightX + openingWidth / 2, 2.0f, false);
    }
    //slides it back to where it used to be
    public void Close()
    {
        left.DOMoveX(_leftX, 2.0f, false);
        right.DOMoveX(_rightX, 2.0f, false);
    }

}
