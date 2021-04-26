using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
  //for displaying talkPrompt
  public Animator animator;
  //for passing in simple dialogue lines
  public DialogueTrigger dialogueTrigger;

  private bool nearNPC = false;

  void OnTriggerEnter(Collider collider) {
    if (collider.gameObject.tag == "Player") {
      //display button prompt
      animator.SetBool("nearbyNPC", true);
      nearNPC = true;
    }
  }

  void Update() {
    //if player is in range of npc
    if (nearNPC) {  
      if (Input.GetKeyDown(KeyCode.E)) {
        dialogueTrigger.TriggerDialogue();
      }
    }
  }

  void OnTriggerExit(Collider collider) {
    if (collider.gameObject.tag == "Player") {
      //remove button prompt
      animator.SetBool("nearbyNPC", false);
      nearNPC = false;
    }
  }
}
