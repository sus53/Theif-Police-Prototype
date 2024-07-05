using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour
{
    private GameObject playerComponent;
    public AudioSource audioSource;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerComponent = other.gameObject;
            playerComponent.GetComponent<MoveByTouch>().Invisible();
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}
