using UnityEngine;

public class EggBehavior : MonoBehaviour
{
    public GameObject eggPrefab;
    public float spawnInterval = 1f;
    public float destroyDelay = 2f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        InvokeRepeating("SpawnEgg", 0f, spawnInterval);
    }

    void SpawnEgg()
    {
        if (!gameManager.IsGamePaused())
        {
            float cameraHalfWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
            float cameraHalfHeight = Camera.main.orthographicSize;

            Vector3 spawnPosition = new Vector3(Random.Range(-cameraHalfWidth, cameraHalfWidth), Random.Range(-cameraHalfHeight, cameraHalfHeight), 0f);
            GameObject spawnedEgg = Instantiate(eggPrefab, spawnPosition, Quaternion.identity);

            Collider2D eggCollider = spawnedEgg.GetComponent<Collider2D>();
            if (eggCollider == null)
            {
                eggCollider = spawnedEgg.AddComponent<BoxCollider2D>();
            }

            spawnedEgg.tag = "Egg";

            Destroy(spawnedEgg, destroyDelay);
        }
    }
}
