using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Move movement;
    
    void OnCollisionEnter(Collision collisionInfo)//if player hits an obstacle
    {
        Debug.Log(collisionInfo.collider.tag);
        if(collisionInfo.collider.tag == "Barrel")
        {
            movement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
    }

}
