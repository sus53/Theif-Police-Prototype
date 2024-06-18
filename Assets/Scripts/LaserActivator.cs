using System.Collections;
using UnityEngine;

public class LaserActivator : MonoBehaviour
{
    public GameObject laser;
    public GameObject laserBody;
    // public Vector3 finalPosition;

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
        Vector3 targetPosition = new Vector3(-13.32f, -0.08f, -1f);
        float moveTime = 5f;

        while (elapsedTime < moveTime)
        {
            laserBody.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        laserBody.transform.position = targetPosition;
    }
}