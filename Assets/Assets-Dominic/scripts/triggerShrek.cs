using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerShrek : MonoBehaviour
{
    public Transform shrek;
    //public Spawn _spawnScript;
    public GameObject spawnObject;
    public float speed;
    private bool triggered = false;
    private bool inPlace = false;
    void OnTriggerEnter(Collider triggerObject)
    {
        if (triggered)
            return;
        triggered = true;
        //spawnObject.GetComponent<Spawn>().enabled = true;
        Instantiate(spawnObject,new Vector3(0,0,0),Quaternion.identity); 

    }

    void Start(){
        //spawnObject.GetComponent<Spawn>().enabled = false;
    }

    void Update()
    {
        if(triggered && !inPlace){
            Vector3 targetPosition = new Vector3(300,0,150);
            float step = speed * Time.deltaTime;
            shrek.position = Vector3.MoveTowards(shrek.position, targetPosition, step);
            if(shrek.position == targetPosition)
                inPlace = true;
        }
    }
    
}
