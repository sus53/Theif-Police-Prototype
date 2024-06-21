using UnityEngine;

public class RotatePolice : MonoBehaviour
{
    public float initial_x;
    public float initial_z;
    public float final_x;
    public float final_z;
    public float rotationSpeed = 3f;
    public float tolerance = 0.1f;
    public bool invertRotation;
    public bool rotateFromLeft;
    private Quaternion targetRotation;
    private bool isRotating = false;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        Vector3 initialPosition = new Vector3(initial_x, transform.position.y, initial_z);
        Vector3 finalPosition = new Vector3(final_x, transform.position.y, final_z);

        if (IsApproximatelyEqual(transform.position, initialPosition))
        {
            targetRotation = Quaternion.Euler(0f, invertRotation ? rotateFromLeft ? 0f : 360f : 180f, 0f);
            isRotating = true;
        }
        else if (IsApproximatelyEqual(transform.position, finalPosition))
        {
            targetRotation = Quaternion.Euler(0f, invertRotation ? 180f : rotateFromLeft ? 0f : 360f, 0f);
            isRotating = true;
        }

        if (isRotating)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }

    private bool IsApproximatelyEqual(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b) < tolerance;
    }
}
