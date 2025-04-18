using UnityEngine;
using UnityEngine.Animations;

public class UILazyFollow : MonoBehaviour
{
    public Transform target; // The object to follow (e.g., the player head in VR)
    public Vector3 offset = new Vector3(0, 1.5f, 2f); // Adjustable XYZ offset
    public float followSpeed = 5f; // Speed of movement
    public float rotationSpeed = 5f; // Speed of rotation

    void LateUpdate()
    {
        if (target == null) return;

        // Compute the desired position with offset in world space
        Vector3 targetPosition = target.position + target.TransformDirection(offset);

        // Smoothly interpolate towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

        // Make the canvas look at the target smoothly
        Quaternion lookRotation = Quaternion.LookRotation(transform.position - target.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
