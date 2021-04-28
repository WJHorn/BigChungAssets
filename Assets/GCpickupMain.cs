using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GCpickupMain : MonoBehaviour
{
   public AudioSource collectSound;
   public GameObject getCarrotsText;
   
   void OnTriggerEnter(Collider collider) {
      if (collider.gameObject.tag == "Player") {
         collectSound.Play();
         Destroy (gameObject);
         SceneManager.LoadScene(0);
      }
   }
}