using System.Collections;
using UnityEngine;

public class LaserActivator : MonoBehaviour
{
    public GameObject laser;
    public Vector3 targetPosition;
    public GameObject laserBody;
    // public Vector3 finalPosition;
    public float moveTime = 1f;
    void Start()
    {
        laser.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ActivateLaser());
        }
    }

    IEnumerator ActivateLaser()
    {
        laser.SetActive(true);

        yield return new WaitForSeconds(0.5f);


        float elapsedTime = 0f;
        Vector3 startPosition = laserBody.transform.position;

        while (elapsedTime < moveTime)
        {
            laserBody.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        laserBody.transform.position = targetPosition;
    }
}