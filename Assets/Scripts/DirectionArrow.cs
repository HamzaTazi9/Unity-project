using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    public Package[] packages;
    public Transform deliveryTarget;

    void Update()
    {
        Package currentPackage = GetCurrentPackage();

        if (currentPackage == null)
        {
            return;
        }

        Transform target;

        if (currentPackage.IsPickedUp())
        {
            target = deliveryTarget;
        }
        else
        {
            target = currentPackage.transform;
        }

        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public Package GetCurrentPackage()
    {
        for (int i = 0; i < packages.Length; i++)
        {
            if (packages[i] != null && packages[i].gameObject.activeSelf)
            {
                return packages[i];
            }
        }

        return null;
    }
}