using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoldCarrotPickup : MonoBehaviour
{
   //public AudioSource collectSound;
   public GameObject getCarrotsText;
   
   void OnTriggerEnter(Collider collider) {
      if ((collider.gameObject.tag == "Player") && (Score.carrots == 10)) {
         //collectSound.Play();
         Destroy (gameObject);
         SceneManager.LoadScene(0);
      }
      else {
         getCarrotsText.GetComponent<Text>().text = "Collect all carrots to collect the golden carrot!";
      }
   }
}
