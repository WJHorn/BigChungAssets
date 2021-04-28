using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateGO : MonoBehaviour
{
   public GameObject target;
   public float speed = 50.0f;

   // Update is called once per frame
   void Update()
   {
      transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
   }
}
