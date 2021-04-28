using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   void Start() {
      Cursor.visible = true;
   }
   
   public void StartGame() {
      SceneManager.LoadScene(1);
   }
   
   public void Quit() {
      Application.Quit();
   }
}
