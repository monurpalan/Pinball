using UnityEngine;

public class BallController : MonoBehaviour
{

    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;

    private Rigidbody2D rb;
    private Vector3 initialPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = gameObject.AddComponent<AudioSource>();
        rb.simulated = false;
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.instance.isGameStarted)
            return;

        HandlePointCollision(collision);
        HandleLoseCollision(collision);
    }

    private void HandlePointCollision(Collision2D collision)
    {
        var point = collision.gameObject.GetComponent<PointCenterController>();
        if (point != null)
        {
            point.OnHit();
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void HandleLoseCollision(Collision2D collision)
    {
        if (collision.transform.CompareTag("Lose"))
        {
            GameManager.instance.OnGameOver();
        }
    }

    public void StartPhysics()
    {
        rb.simulated = true;
    }

    public void ResetBall()
    {
        transform.position = initialPosition;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.simulated = true;
    }
}