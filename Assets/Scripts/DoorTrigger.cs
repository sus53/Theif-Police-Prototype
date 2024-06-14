using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform door;
    public Vector3 closedRotation = new Vector3(0, 0, 0);
    public Vector3 openedRotation = new Vector3(0, 90, 0);
    public float openSpeed = 2f;
    private bool open = false;

    private void Update()
    {
        Quaternion closedQuaternion = Quaternion.Euler(closedRotation);
        Quaternion openedQuaternion = Quaternion.Euler(openedRotation);

        if (open)
        {
            door.rotation = Quaternion.Lerp(door.rotation, openedQuaternion, Time.deltaTime * openSpeed);
        }
        else
        {
            door.rotation = Quaternion.Lerp(door.rotation, closedQuaternion, Time.deltaTime * openSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        open = true;
    }

    private void CloseDoor()
    {
        open = false;
    }
}
