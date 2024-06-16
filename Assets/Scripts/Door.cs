using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Transform block;
    public Vector3 closedPosition;
    public Vector3 openedPosition;
    public float openSpeed = 0.5f;
    private bool open = false;


    void Update()
    {
        if (open == true)
        {
            block.position = Vector3.Lerp(block.position, openedPosition, Time.deltaTime * openSpeed);
        }
        else
        {
            block.position = Vector3.Lerp(block.position, closedPosition, Time.deltaTime * openSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OpenBlock();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CloseBlock();
        }
    }
    public void OpenBlock()
    {
        open = true;
    }
    public void CloseBlock()
    {
        open = false;
    }
}
