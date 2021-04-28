using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigFungTP : MonoBehaviour
{
    void OnTriggerEnter(Collider collider) {
      if (collider.gameObject.tag == "Player") {
         SceneManager.LoadScene(2);
      }
   }
}
