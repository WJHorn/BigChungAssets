using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
   public AudioSource collectSound;
   public DemoScore score;
   
   void OnTriggerEnter(Collider collider) {
      if (collider.tag == "Player") {
         
         collectSound.Play();
         score.carrots += 1;
         Destroy (gameObject);
      }
   }
}
