using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float turnSpeed = 20f;

    private Rigidbody rb;
    private float moveInput = 0f;
    private float turnInput = 0f;
    private float fixedY;
    private AudioSource engineAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fixedY = transform.position.y;
        // audio
        rb = GetComponent<Rigidbody>();
        fixedY = transform.position.y;

        engineAudio = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        moveInput = 0f;
        turnInput = 0f;

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

        // audio 
        if (moveInput != 0f)
{
    if (!engineAudio.isPlaying)
    {
        engineAudio.Play();
    }
}
else
{
    if (engineAudio.isPlaying)
    {
        engineAudio.Stop();
    }
}
    }

    void FixedUpdate()
    {
        if (moveInput == 0f)
        {
            return;
        }

        Vector3 moveDirection = transform.forward * moveInput;
        float moveDistance = moveSpeed * Time.fixedDeltaTime;

        RaycastHit hit;

        bool blocked = rb.SweepTest(
            moveDirection,
            out hit,
            moveDistance,
            QueryTriggerInteraction.Ignore
        );

        if (!blocked)
        {
            Vector3 movement = moveDirection * moveDistance;
            movement.y = 0f;

            Vector3 newPosition = rb.position + movement;
            newPosition.y = fixedY;

            rb.MovePosition(newPosition);
        }

        if (turnInput != 0f)
        {
            float reverseSteering = moveInput > 0f ? 1f : -1f;

            Quaternion turnRotation = Quaternion.Euler(
                0f,
                turnInput * turnSpeed * reverseSteering * Time.fixedDeltaTime,
                0f
            );

            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}
