using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarazDoor : MonoBehaviour
{
    public GameObject RightDoor;
    public GameObject LeftDoor;

    private Vector3 rdInitialPosition;
    private Vector3 ldInitialPosition;
    void Start()
    {
        rdInitialPosition = RightDoor.transform.position;
        ldInitialPosition = LeftDoor.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RightDoor.transform.position = new Vector3(rdInitialPosition.x - 0.35f, 1.09f, rdInitialPosition.z - 0.30f);
            LeftDoor.transform.position = new Vector3(ldInitialPosition.x - 0.35f, 1.09f, ldInitialPosition.z + 0.37f);
            RightDoor.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            LeftDoor.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }


    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            RightDoor.transform.position = rdInitialPosition;
            LeftDoor.transform.position = ldInitialPosition;
            RightDoor.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            LeftDoor.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
