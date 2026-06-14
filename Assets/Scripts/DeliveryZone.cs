using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public DirectionArrow directionArrow;

    // audio 
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
            Package currentPackage = directionArrow.GetCurrentPackage();

            if (currentPackage != null && currentPackage.IsPickedUp())
            { 
                //audio 
                 if (audioSource != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }

                FindFirstObjectByType<GameManager>().DeliverPackage();

                currentPackage.gameObject.SetActive(false);

                Debug.Log("Package delivered!");
            }
            
        
    }
}