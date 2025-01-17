using Unity.VisualScripting;
using UnityEngine;

public class ballBehavior : MonoBehaviour
{
    public float minX = -8.01f;
    public float minY = -4.16f;
    public float maxX = 7.8f;
    public float maxY = 4.34f;
    public float minSpeed;
    public float maxSpeed;
    public Vector2 targetPosition;

    public int secondToMaxSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        secondToMaxSpeed = 30;
        minSpeed = 0.75f;
        maxSpeed = 2.0f;
        targetPosition = getRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = gameObject.GetComponent<Transform>().position;

        if(targetPosition != currentPos) {
            float currentSpeed = minSpeed;
            Vector2 newPosition = Vector2.MoveTowards(currentPos, targetPosition, currentSpeed);
            transform.position = newPosition;
        } else {
            targetPosition = getRandomPosition();
        }

        getRandomPosition();
    }
    Vector2 getRandomPosition()
    {
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);

        Vector2 v = new Vector2(randX, randY);
        return v;
    }
}
