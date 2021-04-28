using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
   public GameObject scoreText;
   public static int carrots;
   
   void Start() {
      carrots = 0;
      scoreText.GetComponent<Text>().text = "CARROTS: 0 / 10";
   }
   void Update() {
      scoreText.GetComponent<Text>().text = "CARROTS: " + carrots + " / 10";
      
      if (carrots == 10) {
         scoreText.GetComponent<Text>().text = "You collected all the carrots!\n\nCollect the golden carrot to return to the menu screen!";
      }
   }
}
