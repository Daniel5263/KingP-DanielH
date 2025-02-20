using UnityEngine;
using UnityEngine.UIElements;

public class SpawningBehaviour : MonoBehaviour
{
    public GameObject[] ballVariants;
    public GameObject targetObject;
    GameObject newObject;
    public float startTime;
    public Pins pinsDB;

    public float minSpawn;
    public float maxSpawn;
    private float spawnRatio;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void Start()
    {
        SetRandomSpawnRatio();
        spawnBall();
        spawnPin();
    }

    void Update()
    {
        float currentTime = Time.time;
        float timeElapsed = currentTime - startTime;
        if (timeElapsed > spawnRatio)
        {
            spawnBall();
        }
    }

    void spawnBall()
    {
        int numVariants = ballVariants.Length;
        if (numVariants > 0)
        {
            int selection = Random.Range(0, numVariants);
            newObject = Instantiate(ballVariants[selection], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            ballBehavior ballBehavior = newObject.GetComponent<ballBehavior>();
            ballBehavior.setBounds(minX, maxX, minY, maxY);
            ballBehavior.setTarget(targetObject);
            ballBehavior.initialPosition();
        }
        SetRandomSpawnRatio();
        startTime = Time.time;
    }

    void spawnPin()
    {
        targetObject = Instantiate(pinsDB.getPin(CharacterManager.selection).prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
    }

    void SetRandomSpawnRatio()
    {
        spawnRatio = Random.Range(minSpawn, maxSpawn);
    }
}
