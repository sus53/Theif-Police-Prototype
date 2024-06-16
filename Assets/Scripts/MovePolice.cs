using System.Collections;
using UnityEngine;

public class MovePolice : MonoBehaviour
{
    [SerializeField] private float startPosition = 8.5f; // Starting position along the x-axis
    [SerializeField] private float endPosition = 2.5f; // Ending position along the x-axis
    [SerializeField] private float moveSpeed = 5f; // Speed of movement

    private bool movingToEnd = true; // Flag to track movement direction

    void Start()
    {
        // Start the coroutine that moves the police back and forth indefinitely
        StartCoroutine(MoveBackAndForth());
    }

    IEnumerator MoveBackAndForth()
    {
        while (true) // Loop indefinitely
        {
            // Calculate the journey length (distance between start and end position)
            float journeyLength = Mathf.Abs(endPosition - startPosition);

            // Calculate the target position based on current movement direction
            float targetPosition = movingToEnd ? endPosition : startPosition;
            // Move towards the target position
            while (Mathf.Abs(transform.position.x - targetPosition) > 0.01f)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition, transform.position.y, transform.position.z), step);
                yield return null;
            }

            // Toggle movement direction
            movingToEnd = !movingToEnd;

            // Wait a moment before switching direction (optional)
            yield return new WaitForSeconds(1f);
        }
    }
}
