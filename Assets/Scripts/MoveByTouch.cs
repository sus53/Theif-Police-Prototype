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
    private List<Material> defaultPlayerMaterial = new List<Material>();
    public AudioSource ObstacleBreak;
    public AudioSource Footstep;
    	public AudioSource gameoverSound;
        public AudioSource bgAudio;

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
            
            if (!Footstep.isPlaying)
            {
                Footstep.Play();
            }

            Footstep.volume = Mathf.Lerp(Footstep.volume, 1f, Time.deltaTime * 5f); 
        }
        else
        {
            if (Footstep.isPlaying)
            {
                StartCoroutine(FadeOutFootstep(Footstep, 0.5f)); 
            }
        }

        Footstep.pitch = Random.Range(0.8f, 1.2f);
    }

    IEnumerator FadeOutFootstep(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; 
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
        ChangePlayerMaterial(new Color(0f, 0f, 0f), 1.5f);
        StartCoroutine(RevertInvisibleAfterDelay());
    }

    public void SpeedBoost()
    {
        runSpeed = 10f;
        // ChangePlayerMaterial(new Color(0.0f, 0.922f, 0.384f), 1.5f);
transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine(RevertSpeedAfterDelay());
    }

    public void BreakablePower()
    {
        isBreakablePower = true;
        StartCoroutine(ChangeScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f));
    }

    IEnumerator ChangeScale(Vector3 targetScale, float duration)
    {
        Vector3 originalScale = transform.localScale;
        float currentTime = 0f;

        while (currentTime <= duration)
        {
            child.transform.localScale = Vector3.Lerp(originalScale, targetScale, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        child.transform.localScale = targetScale;
    }

    public void ChangePlayerMaterial(Color finalColor, float duration)
    {
        for (int i = 1; i < child.childCount; i++)
        {
            SkinnedMeshRenderer renderer = child.GetChild(i).GetComponent<SkinnedMeshRenderer>();
            StartCoroutine(SlowlyDissolve(renderer, renderer.material.color, finalColor, duration, 0f, 1f));
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


    IEnumerator SlowlyDissolve(Renderer renderer, Color initialColor, Color finalColor, float duration, float initialStrength, float targetStrength)
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
                invisibleDissolveMaterial.SetFloat("_DissolveStrength", dissolveStrength);
                renderer.material = invisibleDissolveMaterial;
            yield return null;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (isBreakablePower)
        {
            if (other.gameObject.CompareTag("Breakable"))
            {
                isBreakablePower = false;
                StartCoroutine(ChangeScale(new Vector3(1f, 1f, 1f), 1f));
                ObstacleBreak.Play();
                Destroy(other.gameObject);


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
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void Die()
    {
        gameoverSound.Play();
        Time.timeScale = 0.1f;
        gameOver.SetActive(true);
        bgAudio.Stop();
    }
}
