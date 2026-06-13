using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float turnSpeed = 20f;

    void Update()
    {
        float moveInput = 0f;
        float turnInput = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveInput = 1f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveInput = -1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            turnInput = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            turnInput = 1f;
        }

        if (moveInput != 0f)
        {
            transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime, Space.Self);

            // Tijdens achteruit sturen draait de auto omgekeerd, zoals een echte auto.
            float reverseSteering = moveInput > 0f ? 1f : -1f;
            transform.Rotate(Vector3.up * turnInput * turnSpeed * reverseSteering * Time.deltaTime, Space.Self);
        }
    }
}
