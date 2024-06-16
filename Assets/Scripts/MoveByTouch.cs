using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    void Start()
    {
        material = GetComponent<Renderer>().material;
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

    IEnumerator BreakablePowerAfterDelay()
    {
        yield return null;
        breakableCount = 0;
        isBreakablePower = false;
    }
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
