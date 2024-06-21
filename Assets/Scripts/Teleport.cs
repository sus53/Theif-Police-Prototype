using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    private bool isTeleport = false;
    private GameObject player;
    void Update()
    {

        if (isTeleport)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(0, 0, 0), 0.1f);
            isTeleport = false;
            Debug.Log("sdasdas");
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            player = collider.gameObject;
            isTeleport = true;
        }
    }
}
