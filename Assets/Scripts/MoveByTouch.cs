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
    [SerializeField] CharacterController characterController;
    private Rigidbody rb;
    private Animator animator;
    private Quaternion playerTargetRotation;
    private Transform child;
    private Transform grandChild;
    public Material dissolveMaterial;
    public Material invisibleDissolveMaterial;
    public List<Material> defaultPlayerMaterial = new List<Material>();

    void Start()
    {

        material = GetComponent<Renderer>().material;
        characterController = GetComponent<CharacterController>();
        child = transform.GetChild(0);
        grandChild = child.GetChild(0);
        animator = child.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        GetTheifMaterials();
        // dissolveMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
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
            // rb.AddForce(move * runSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }


    }
    private void GetTheifMaterials()
    {
        for (int i = 1; i < child.childCount; i++)
        {
            defaultPlayerMaterial.Add(child.GetChild(i).GetComponent<SkinnedMeshRenderer>().material);
        }
    }
    public void Invisible()
    {
        gameObject.layer = 7;
        ChangePlayerMaterial(new Color(0f, 0f, 0f), true, 1.5f);
        StartCoroutine(RevertInvisibleAfterDelay());
    }

    public void SpeedBoost()
    {
        runSpeed = 10f;
        ChangePlayerMaterial(new Color(0.0f, 0.922f, 0.384f), false, 1.5f);
        StartCoroutine(RevertSpeedAfterDelay());
    }

    public void BreakablePower()
    {
        isBreakablePower = true;
        ChangePlayerMaterial(new Color(0.1f, 0.0f, 0.2f), false, 1f);
    }

    public void ChangePlayerMaterial(Color finalColor, bool isAlpha, float duration)
    {
        for (int i = 1; i < child.childCount; i++)
        {
            SkinnedMeshRenderer renderer = child.GetChild(i).GetComponent<SkinnedMeshRenderer>();
            StartCoroutine(SlowlyDissolve(renderer, renderer.material.color, finalColor, duration, 0f, 1f, isAlpha));
        }
    }

    private void ChangePlayerMaterialToDefault()
    {
        for (int i = 0; i < defaultPlayerMaterial.Count; i++)
        {
            SkinnedMeshRenderer renderer = child.GetChild(i + 1).GetComponent<SkinnedMeshRenderer>();
            renderer.material = defaultPlayerMaterial[i];
        }
    }


    IEnumerator SlowlyDissolve(Renderer renderer, Color initialColor, Color finalColor, float duration, float initialStrength, float targetStrength, bool isAlpha)
    {
        float elapsedTime = 0.0f;
        Debug.Log("color " + initialColor);
        dissolveMaterial.SetColor("_Initial", initialColor);
        dissolveMaterial.SetColor("_Final", finalColor);
        float dissolveStrength;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            dissolveStrength = Mathf.Lerp(initialStrength, targetStrength, elapsedTime / duration);
            if (isAlpha)
            {
                invisibleDissolveMaterial.SetFloat("_DissolveStrength", dissolveStrength);
                renderer.material = invisibleDissolveMaterial;
            }
            else
            {
                dissolveMaterial.SetFloat("_DissolveStrength", dissolveStrength);
                renderer.material = dissolveMaterial;

            }
            yield return null;
        }


    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (isBreakablePower)
        {
            if (other.gameObject.CompareTag("Breakable"))
            {
                Destroy(other.gameObject);
                isBreakablePower = false;
                StartCoroutine(RevertBreakablePowerAfterDelay());


            }
        }
    }

    IEnumerator RevertInvisibleAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        gameObject.layer = 6;
        ChangePlayerMaterialToDefault();
    }

    IEnumerator RevertSpeedAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        runSpeed = 4f;
        ChangePlayerMaterialToDefault();
    }

    IEnumerator RevertBreakablePowerAfterDelay()
    {
        yield return null;
        ChangePlayerMaterialToDefault();

    }

    public void Die()
    {
        Time.timeScale = 0.1f;
        gameOver.SetActive(true);
    }
}
