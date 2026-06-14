using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    public Transform packageTarget;
    public Transform deliveryTarget;
    public Package packageScript;

    void Update()
    {
        Transform target = packageTarget;

        if (packageScript != null && packageScript.IsPickedUp())
        {
            target = deliveryTarget;
        }

        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }
}