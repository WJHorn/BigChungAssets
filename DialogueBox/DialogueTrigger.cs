using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//requires u to make an empty game object in the scene
//name it "DialogueManager"
//and attatch the DialogueManager.cs script to it
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue() {
      FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
