using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 4f, -8f);
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    private void LateUpdate()
    {
        if (target == null) return;

        // Gebruik alleen de Y-rotatie van de auto zodat de camera achter de auto blijft,
        // maar niet meetrilt met kleine physics-bewegingen.
        Quaternion targetYawRotation = Quaternion.Euler(0f, target.eulerAngles.y, 0f);

        Vector3 wantedPosition = target.position + targetYawRotation * offset;

        transform.position = Vector3.Lerp(
            transform.position,
            wantedPosition,
            followSpeed * Time.deltaTime
        );

        Vector3 lookTarget = target.position + Vector3.up * 1.5f;
        Quaternion wantedRotation = Quaternion.LookRotation(lookTarget - transform.position);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            wantedRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}