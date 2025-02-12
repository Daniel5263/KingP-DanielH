using UnityEngine;
using UnityEngine.UIElements;

public class SpawningBehaviour : MonoBehaviour
{
    public GameObject[] ballVariants;
    public GameObject[] targetObject;
    GameObject newObject;
    public float startTime;
    public float spawnRatio = 1.0f;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void Start() {
        
    }

    void Update() {
        
    }

    void spawnBall() {
        int numVariants = ballVariants.Length;
        if (numVariants > 0)
        {
            int selection = Random.Range(0, numVariants);
            newObject = Instantiate(ballVariants[selection], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
    }
}
