using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reachCarrot : MonoBehaviour
{
   public Transform shrek;
   public GameObject RGC;
   public float speed;
   private bool triggered = false;
    void OnTriggerEnter(Collider triggerObject)
    {
        if(triggerObject.tag == "Player"){
            triggered = true;
            Debug.Log("Hit");
            //DestroyImmediate(spawnObject);
            GameObject [] spawners = GameObject.FindGameObjectsWithTag("ObjectSpawn");
            GameObject [] barrels = GameObject.FindGameObjectsWithTag("Barrel");
            foreach(GameObject go in spawners){
                Destroy(go);
            }

            foreach(GameObject go in barrels){
                Destroy(go);
            }
            RGC.SetActive(true);
        }
    }

    void Update()
    {
        if(triggered){
            Vector3 targetPosition = new Vector3(300,-25,250);
            Vector3 targetLook = targetPosition - shrek.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetLook, Vector3.up);
            float step = speed * Time.deltaTime;
            //Vector3 newDirection = Vector3.RotateTowards(shrek.forward, targetPosition, step, 0.0f);
            shrek.rotation = Quaternion.RotateTowards(shrek.rotation, targetRotation, step*8);
           
            shrek.position = Vector3.MoveTowards(shrek.position, targetPosition, step);
            
        }
        
    }

}
