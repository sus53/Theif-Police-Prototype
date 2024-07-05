using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLaser : MonoBehaviour
{
    public GameObject laser;
    public AudioSource buttonAudio;
    public AudioSource laserAudio;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            buttonAudio.Play();
            laserAudio.Stop();
            Destroy(laser);
        }
    }
}
