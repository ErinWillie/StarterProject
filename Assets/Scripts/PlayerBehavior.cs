using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {

        if (!gameManager.IsGamePaused())
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);

            movement.Normalize();

            Vector3 newPosition = transform.position + movement * moveSpeed * Time.deltaTime;

            float cameraHalfWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
            float cameraHalfHeight = Camera.main.orthographicSize;

            newPosition.x = Mathf.Clamp(newPosition.x, -cameraHalfWidth, cameraHalfWidth);
            newPosition.y = Mathf.Clamp(newPosition.y, -cameraHalfHeight, cameraHalfHeight);

            transform.position = newPosition;

            if (horizontalInput > 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (horizontalInput < 0)
            {
                spriteRenderer.flipX = false;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Egg"))
        {
            Destroy(other.gameObject);

            gameManager.IncreaseScore();
        }
    }
}