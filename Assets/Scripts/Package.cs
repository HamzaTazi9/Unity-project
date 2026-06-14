using UnityEngine;

public class Package : MonoBehaviour
{
    public Transform packageHolder;

    private bool isPickedUp = false;
    // audio 
    private AudioSource audioSource; 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger geraakt door: " + other.name);

        if (other.CompareTag("Player"))
        {
            isPickedUp = true;
            //audio 
            if (audioSource != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }

            transform.SetParent(packageHolder);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            

            Debug.Log("Package picked up!");
        }
    }

    public bool IsPickedUp()
{
    return isPickedUp;
}

}