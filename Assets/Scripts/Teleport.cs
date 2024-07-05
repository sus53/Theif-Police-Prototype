using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    public Transform teleportPlace;
    private GameObject player;
    public AudioSource teleportAudio;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            player = collider.gameObject;
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = teleportPlace.position;
            player.GetComponent<CharacterController>().enabled = true;
            teleportAudio.Play();
        }
    }
}
