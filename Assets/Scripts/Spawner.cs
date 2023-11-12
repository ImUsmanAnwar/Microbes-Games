using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;


    public float speed = 5f;  // Adjust the speed
    public float leftLimit = -5f;  // Adjust the left limit
    public float rightLimit = 5f;  // Adjust the right limit

    private int direction = 1;  // 1 for moving right, -1 for moving left

    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;
    [Range(0f, 1f)] public float bombChance = 0.05f;

    public float minSpawnDelay = 0.9f;
    public float maxSpawnDelay = 1.1f;
    public float timeToDecrease = 10f;
    public float decreaseAmount = 0.04f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    

    private float elapsedTime = 0f;

    public float maxLifetime = 5f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }


    private void Update()
    {

        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // Check if it's time to decrease the delay values
        if (elapsedTime >= timeToDecrease)
        {
            // Decrease the minDelay and adjust the maxDelay to maintain the gap
            minSpawnDelay -= decreaseAmount;
            maxSpawnDelay = minSpawnDelay + 0.2f;

            // Reset the elapsed time for the next adjustment
            elapsedTime = 0f;
        }

        // Calculate the translation based on the speed and direction
        float translation = direction * speed * Time.deltaTime;

        // Apply the translation along the X-axis
        transform.Translate(Vector3.right * translation);

        // Clamp the position to stay within the specified limits
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            transform.position.y,
            transform.position.z
        );

        // Update the object's position
        transform.position = clampedPosition;

        // Change direction when reaching the limits
        if (transform.position.x >= rightLimit || transform.position.x <= leftLimit)
        {
            direction *= -1;
        }
    }

    public void RestartGame()
    {
        Debug.Log("Button clicked!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

            if (Random.value < bombChance) {
                prefab = bombPrefab;
            }

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject fruit = Instantiate(prefab, position, rotation);
            Destroy(fruit, maxLifetime);

            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

}
