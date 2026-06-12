using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float turnSpeed = 20f;

    void Update()
    {
        // Vooruit
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.up * -turnSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
            }
        }

        // Achteruit
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.up * -turnSpeed * Time.deltaTime);
            }
        }
    }
}