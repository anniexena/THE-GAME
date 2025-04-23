using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public Transform player;
    public float smoothing; 

    // Map bounds
    public float minBoundX;
    public float maxBoundX;
    public float minBoundY; 
    public float maxBoundY; 

    // Find camera width and height
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    void Start()
    {
        // Calculate camera dimensions
        Camera mainCamera = Camera.main;
        cameraHalfHeight = mainCamera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * mainCamera.aspect;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothing);

        // Make sure camera doesn't go outside of map
        float clampedX = Mathf.Clamp(smoothedPosition.x, minBoundX + cameraHalfWidth, maxBoundX - cameraHalfWidth);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minBoundY + cameraHalfHeight, maxBoundY - cameraHalfHeight);

        // Update camera position
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
