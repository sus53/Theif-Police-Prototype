using System.Collections;
using UnityEngine;

public class MovePolice : MonoBehaviour
{
    [SerializeField] private float startPosition = 8.5f;
    [SerializeField] private float endPosition = 2.5f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool isXAxis;

    private bool movingToEnd = true;
    void Start()
    {
        if (isXAxis)
        {
            StartCoroutine(MoveXAxix());
        }
        else
        {
            StartCoroutine(MoveZAxis());
        }
    }

    IEnumerator MoveXAxix()
    {
        while (true)
        {
            float targetPosition = movingToEnd ? endPosition : startPosition;
            while (Mathf.Abs(transform.position.x - targetPosition) > 0.01f)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition, transform.position.y, transform.position.z), step);
                yield return null;
            }

            movingToEnd = !movingToEnd;

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator MoveZAxis()
    {
        while (true)
        {
            float targetPosition = movingToEnd ? endPosition : startPosition;
            while (Mathf.Abs(transform.position.z - targetPosition) > 0.01f)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, targetPosition), step);
                yield return null;
            }

            movingToEnd = !movingToEnd;

            yield return new WaitForSeconds(1f);
        }
    }
}
