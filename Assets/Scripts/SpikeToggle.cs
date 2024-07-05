using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeToggle : MonoBehaviour
{
    public GameObject spike;
    public AudioSource spikeAudio;
    private float openSpeed = 0.3f;

    void Start()
    {
        spike.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // spikeAudio.Play();
            StartCoroutine(DestroySpike());
        }
    }

    IEnumerator DestroySpike()
    {
        yield return new WaitForSeconds(openSpeed);
        
        spike.SetActive(true);
        Destroy(GetComponent<BoxCollider>());
    }
}
