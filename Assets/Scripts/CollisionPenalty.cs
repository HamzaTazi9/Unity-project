using UnityEngine;

public class CollisionPenalty : MonoBehaviour
{
    public float hitCooldown = 1f;
    public float obstacleCheckRadius = 3f;

    private float lastHitTime = 0f;

    //audio 
    private AudioSource[] audioSources;

    void Start()
{
    audioSources = GetComponents<AudioSource>();
}
// end audio 
    void Update()
    {
        if (Time.time - lastHitTime < hitCooldown)
        {
            return;
        }

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles)
        {
            float distance = Vector3.Distance(transform.position, obstacle.transform.position);

            if (distance <= obstacleCheckRadius)
            {
                lastHitTime = Time.time;
                FindFirstObjectByType<GameManager>().HitObstacle();
                //audio 
                audioSources[1].Play();
                Debug.Log("Obstacle nearby! -10 points");
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastHitTime < hitCooldown)
        {
            return;
        }

        if (other.CompareTag("MovingObstacle"))
        {
            lastHitTime = Time.time;
            FindFirstObjectByType<GameManager>().HitMovingObstacle();
            //audio 
            audioSources[2].Play();
            Debug.Log("Traffic collision! -25 points");
        }
    }
}