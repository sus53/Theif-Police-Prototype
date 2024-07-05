using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EatCoin : MonoBehaviour
{
    public TextMeshProUGUI score;
    public AudioSource coinSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            score.text = (int.Parse(score.text) + 1) + "";
            Destroy(gameObject);
            coinSound.Play();
        }
    }


}
