using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 4f, -8f);
    [SerializeField] private float followSpeed = 5f;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 wantedPosition = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, followSpeed * Time.deltaTime);
        transform.LookAt(target);
    }
}