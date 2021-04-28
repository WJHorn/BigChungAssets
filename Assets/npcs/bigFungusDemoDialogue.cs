using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigFungusDemoDialogue : DialogueTrigger
{
  public GameObject goldCarrot;
  public DemoScore score;
  public Dialogue[] DialogueQueuePrePuzzle;
  public Dialogue[] DialogueQueuePostPuzzle;

  private int preQueueLength;
  private int preQueueIndex = 0;
  private int postQueueLength;
  private int postQueueIndex = 0;
  private bool goldCarrotSpawned = false;
  void Awake() {
    preQueueLength = DialogueQueuePrePuzzle.Length;
    postQueueLength = DialogueQueuePostPuzzle.Length;
  }

  private int dialogueLengthReverse = 0;

  public override void TriggerDialogue() {
    //if player has not yet collected the 3 carrots
    if (score.carrots < 3) {
      dialogue = DialogueQueuePrePuzzle[preQueueIndex];
      dialogueLength = dialogue.sentences.Length;
      
      if (dialogueLengthReverse == 0) {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
      } else {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
      }
      dialogueLengthReverse++;
      if (dialogueLengthReverse == dialogueLength+1) {
        preQueueIndex++;
        dialogueLengthReverse = 0;
        if (preQueueIndex == preQueueLength) {
          preQueueIndex--;
        }
      }
    }
    //if player has solved puzzle
    else {
      dialogue = DialogueQueuePostPuzzle[postQueueIndex];
      dialogueLength = dialogue.sentences.Length;
      
      if (dialogueLengthReverse == 0) {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
      } else {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
      }
      dialogueLengthReverse++;
      if (dialogueLengthReverse == dialogueLength+1) {
        postQueueIndex++;
        dialogueLengthReverse = 0;
        if (!goldCarrotSpawned) {
          goldCarrot.SetActive(true);
          goldCarrotSpawned = true;
        }  
        if (postQueueIndex == postQueueLength) {
          postQueueIndex--;
        }
      }
    }

  } 
}
