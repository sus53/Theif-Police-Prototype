using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTouch : MonoBehaviour
{
    public Joystick joystick;
    public float runSpeed = 10f;
    [SerializeField] CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {

        Vector3 move = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
        if (move.magnitude >= 0.1f)
        {
            characterController.Move(move * runSpeed * Time.deltaTime);
        }


    }

}
