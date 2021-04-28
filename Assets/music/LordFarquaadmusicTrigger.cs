using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordFarquaadmusicTrigger : MonoBehaviour
{
  public AudioSource mainHubMusic;
  public AudioSource lordFarquaadMusic;

  void OnTriggerEnter(Collider triggerObject) {
    StartCoroutine(audioFader.StartFade(mainHubMusic, 1, 0));
    lordFarquaadMusic.Play();
    StartCoroutine(audioFader.StartFade(lordFarquaadMusic, 1, 1));
  }

  void OnTriggerExit(Collider triggerObject) {
    StartCoroutine(audioFader.StartFade(mainHubMusic, 1, 1));
    StartCoroutine(audioFader.StartFade(lordFarquaadMusic, 1, 0));
    //lordFarquaadMusic.Stop();
  }
}
