using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameOver = false;
    public float delay = 1f;
    //pre:  is called from PlayerCollision/PlayerMovement
    //post: if player hits obstacle restart the scene 
    public void EndGame()
    {
        if( !gameOver)
        {
            gameOver = true;
            Invoke("Restart",delay);//delay the restart by a second
        }
    }
    //restart the active scene
    void Restart()
    {
        SceneManager.LoadScene("Shreck-Dominic");
    }
}
