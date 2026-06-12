using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float speed = 5f;
    public float moveTime = 10f;

    private float timer = 0f;
    private bool isMoving = true;

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            timer += Time.deltaTime;

            if (timer >= moveTime)
            {
                isMoving = false;
            }
        }
    }
}