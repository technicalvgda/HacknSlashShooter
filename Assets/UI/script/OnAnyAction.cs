using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class OnAnyAction : MonoBehaviour
{
    public UnityEvent onAnyAction;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            onAnyAction.Invoke();
        }
    }
}
