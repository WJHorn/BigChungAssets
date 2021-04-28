using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoldCarrotPickupV2 : MonoBehaviour
{
   //public AudioSource collectSound;
   public GameObject getCarrotsText;
   public GameObject biggerChungus;
   public GameObject RGC;
   private Animator anim;
   
   void OnTriggerEnter(Collider collider) {
      if (collider.gameObject.tag == "Player") {
         //collectSound.Play();
         Destroy (gameObject);
         anim = biggerChungus.GetComponent<Animator>();
         anim.SetTrigger("Active");
         RGC.SetActive(true);
      }
   }
}
