using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{

    private GameObject player;
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Player")
        {
            player = other.gameObject;
            player.GetComponent<MoveByTouch>().SpeedBoost();
            Destroy(gameObject);
        }
    }


}
