using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Late update is called after player update
    void LateUpdate()
    {
        if (transform.position != target.position) {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            // Finds distance between target and transform and moves a % of that distance
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
