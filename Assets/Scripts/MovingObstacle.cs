using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float speed = 5f;
    public float moveTime = 10f;

    private float timer = 0f;
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= moveTime)
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
            timer = 0f;
        }
    }
}