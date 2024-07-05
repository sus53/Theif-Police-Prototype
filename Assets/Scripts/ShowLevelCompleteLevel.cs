using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShowLevelCompleteLevel : MonoBehaviour
{
    public GameObject levelCompleteUI;
    public AudioSource gameBackgroundAudio;
     public AudioSource levelComplete;
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
levelCompleteUI.SetActive(true);
gameBackgroundAudio.Stop();
levelComplete.Play();
    }


    }
}
