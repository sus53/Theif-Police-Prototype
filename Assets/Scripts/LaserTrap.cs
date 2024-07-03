using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    public float blinkTime = 1.0f;
    private Renderer laserRenderer;
    private Collider laserCollider;

    private void Start()
    {
        laserRenderer = GetComponent<Renderer>();
        laserCollider = GetComponent<Collider>();

        if (laserRenderer == null || laserCollider == null)
        {
            enabled = false; // Disable this script if prerequisites are not met
            return;
        }
        if (blinkTime != -1)
        {
            StartCoroutine(BlinkLaser());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<MoveByTouch>().Die();
        }
    }

    IEnumerator BlinkLaser()
    {
        while (true)
        {
            // Turn off the laser
            laserRenderer.enabled = false;
            laserCollider.enabled = false;
            yield return new WaitForSeconds(blinkTime);

            // Turn on the laser
            laserRenderer.enabled = true;
            laserCollider.enabled = true;
            yield return new WaitForSeconds(blinkTime);
        }
    }
}
