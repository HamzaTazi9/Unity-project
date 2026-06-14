using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public DirectionArrow directionArrow;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
            Package currentPackage = directionArrow.GetCurrentPackage();

            if (currentPackage != null && currentPackage.IsPickedUp())
            {
                FindFirstObjectByType<GameManager>().DeliverPackage();

                currentPackage.gameObject.SetActive(false);

                Debug.Log("Package delivered!");
            }
            
        
    }
}