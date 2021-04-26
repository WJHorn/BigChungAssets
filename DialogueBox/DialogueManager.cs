using System.Collections; //needed for queue
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
  //GUI fields
  public Text CharaName;
  public Text dialogueText;

  //for animating the dialogue box opening and closing
  public Animator animator;

  private Queue<string> sentenceQueue;

  // Start is called before the first frame update
  void Start() {
    sentenceQueue = new Queue<string>();
  }

  public void StartDialogue(Dialogue dialogue) {
    if (!animator.GetBool("IsOpen")) {
      //disable player walking
      GameObject.Find("chungus").GetComponent<Move>().enabled = false;
      //open dialogue box on screen using unity animator
      animator.SetBool("IsOpen", true);
    }
      CharaName.text = dialogue.name;
      sentenceQueue.Clear();
      
      foreach (string sentence in dialogue.sentences)
      {
        Debug.Log("queueing sentence: " + sentence + "\n");
        sentenceQueue.Enqueue(sentence);
      }
    
    DisplayNextSentence();
  }

  public void DisplayNextSentence() {
    if (sentenceQueue.Count == 0) {
      EndDialogue();
      return;
    }

    string sentence = sentenceQueue.Dequeue();
    Debug.Log("dequeueing sentence: " + sentence + "\n");
    
    dialogueText.text = sentence;
    //used for enabling the typewriter effect 
    StopAllCoroutines(); //prevents overlapping text from people trying to skip text
    StartCoroutine(TypeSentence(sentence));
  }

  //typewriter effect
  IEnumerator TypeSentence(string sentence) {
    dialogueText.text = "";
    foreach (char letter in sentence.ToCharArray())
    {
      dialogueText.text += letter;
      yield return null;  //waits a single frame
    }
  }

  void EndDialogue() {
    animator.SetBool("IsOpen", false);
    //enable player walking
    GameObject.Find("chungus").GetComponent<Move>().enabled = true;
  }

  // Update is called once per frame
  /*void Update()
  {
    if (Input.GetKeyDown(KeyCode.Return)) {
      DisplayNextSentence();
    }
    if (Input.GetKeyDown(KeyCode.Space)) {
      DisplayNextSentence();
    }
  }*/
}
