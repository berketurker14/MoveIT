using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;    // The target the camera follows
    public float smoothTime = 0.3f;    // The time it takes for the camera to smoothly move to the target
    private Vector3 velocity = Vector3.zero;    // The current velocity of the camera

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the position the camera should be at
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            // Move the camera towards the target position smoothly
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
