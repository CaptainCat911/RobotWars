using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventTest : MonoBehaviour
{
    public UnityEvent testEvent;

    private void OnCollisionEnter(Collision collision)
    {
        testEvent.Invoke();
    }
}
