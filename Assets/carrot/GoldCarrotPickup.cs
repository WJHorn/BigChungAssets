using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoldCarrotPickup : MonoBehaviour
{
   //public AudioSource collectSound;
   
   void OnTriggerEnter(Collider collider) {
      if (collider.gameObject.tag == "Player") {
        //collectSound.Play();
        gameObject.SetActive(false);
      }
   }
}
