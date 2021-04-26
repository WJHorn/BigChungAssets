using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//requires u to make an empty game object in the scene
//name it "DialogueManager"
//and attatch the DialogueManager.cs script to it
[System.Serializable]
public class DialogueTrigger : MonoBehaviour
{
  //bonus to do: make this an array so it can be applied to any generic interactible 
  public Dialogue dialogue;
  protected int dialogueLength;
  void Awake() {
    dialogueLength = dialogue.sentences.Length;
  }
  public virtual void TriggerDialogue() {
    if (dialogueLength == dialogue.sentences.Length) {
      FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    } else {
      FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }
    dialogueLength--;
    if (dialogueLength == -1) dialogueLength = dialogue.sentences.Length;
  }
}
