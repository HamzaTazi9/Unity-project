using UnityEngine;

public class Package : MonoBehaviour
{
    public Transform packageHolder;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger geraakt door: " + other.name);

        if (other.CompareTag("Player"))
        {
            transform.SetParent(packageHolder);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            FindFirstObjectByType<GameManager>().DeliverPackage();

            Debug.Log("Package picked up!");
        }
    }
}