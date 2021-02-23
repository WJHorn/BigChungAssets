using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
   //public AudioSource collectSound;
   
   void OnTriggerEnter(Collider collider) {
      if (collider.gameObject.tag == "Player") {
         //collectSound.Play();
         Score.carrots += 1;
         Destroy (gameObject);
      }
   }
}
