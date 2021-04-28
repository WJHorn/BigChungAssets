using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateTrigger : MonoBehaviour
{
    bool isTriggered = false;

    void OnTriggerEnter(Collider player)
    {
        if(!isTriggered)
        {
            isTriggered = true;
            Debug.Log("Triggered");

        }
    }
}
