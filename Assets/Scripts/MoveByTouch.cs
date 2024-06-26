using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoveByTouch : MonoBehaviour
{
    public Joystick joystick;
    public float runSpeed = 4f;
    public GameObject gameOver;
    private Material material;
    private bool isBreakablePower = false;
    private int breakableCount = 0;
    [SerializeField] CharacterController characterController;
    private Animator animator;
    private Quaternion playerTargetRotation;
    private Transform child;
    void Start()
    {
        material = GetComponent<Renderer>().material;
        characterController = GetComponent<CharacterController>();
        child = transform.GetChild(0);
        animator = child.GetComponent<Animator>();
    }
    void Update()
    {

        Vector3 move = new Vector3(joystick.Horizontal, 0f, joystick.Vertical).normalized;
        animator.SetFloat("move_speed", move.magnitude);
        if (move.magnitude >= 0.1f)
        {
            playerTargetRotation = Quaternion.LookRotation(move);
            child.rotation = Quaternion.Slerp(child.rotation, playerTargetRotation, Time.deltaTime * 5f);
            characterController.Move(move * runSpeed * Time.deltaTime);

        }




    }

    public void Invisible()
    {
        material.color = new Color(0.5f, 0.5f, 0.5f, 0.1f);
        gameObject.layer = 7;
        StartCoroutine(RevertInvisibleAfterDelay());
    }

    public void SpeedBoost()
    {
        runSpeed = 10f;
        StartCoroutine(RevertSpeedAfterDelay());
    }
    public void BreakablePower()
    {
        isBreakablePower = true;
    }

    // public void OnTriggerEnter(Collider other)
    // {
    //     if (isBreakablePower)
    //     {
    //         if (other.gameObject.name == "Breakable")
    //         {
    //             Destroy(other.gameObject);
    //             breakableCount++;
    //             if (breakableCount >= 2)
    //             {
    //                 StartCoroutine(BreakablePowerAfterDelay());
    //             }
    //         }
    //     }
    // }


    private void OnControllerColliderHit(ControllerColliderHit other)
    {

        if (isBreakablePower)
        {
            if (other.gameObject.CompareTag("Breakable"))
            {
                Destroy(other.gameObject);
                breakableCount++;
                Debug.Log(breakableCount);

                if (breakableCount >= 1)
                {
                    breakableCount = 0;
                    isBreakablePower = false;
                }
            }
        }
    }
    // private void OnCollisionEnter(Collision other)
    // {
    //     Debug.Log("Collided");
    //     if (isBreakablePower)
    //     {
    //         if (other.gameObject.CompareTag("Breakable"))
    //         {
    //             Destroy(other.gameObject);
    //             breakableCount++;
    //             if (breakableCount >= 2)
    //             {
    //                 breakableCount = 0;
    //                 isBreakablePower = false;
    //             }
    //         }
    //     }
    // }

    IEnumerator RevertInvisibleAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        material.color = new Color(1f, 1f, 1f, 1f);
        gameObject.layer = 6;
    }
    IEnumerator RevertSpeedAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        runSpeed = 4f;
    }

    public void Die()
    {
        Time.timeScale = 0.1f;
        gameOver.SetActive(true);
    }

}
