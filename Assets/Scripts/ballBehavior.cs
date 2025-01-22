using Unity.VisualScripting;
using UnityEngine;

public class ballBehavior : MonoBehaviour
{
    public float minX = -10.102f;
    public float minY = -4.219f;
    public float maxX = 10.102f;
    public float maxY = 4.219f;
    public float minSpeed;
    public float maxSpeed;
    public Vector2 targetPosition;

    public int secondToMaxSpeed;

    public GameObject target;
    public float minLaunchSpeed;
    public float maxLaunchSpeed;
    public float minTimeToLaunch;
    public float maxTimeToLaunch;
    public float cooldown;
    public bool launching;
    public float launchDuration;
    public float timeLastLaunch;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // secondToMaxSpeed = 30;
       // minSpeed = 0.001f;
       // maxSpeed = 2.0f;
        targetPosition = getRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = gameObject.GetComponent<Transform>().position;
        float distance = Vector2.Distance(currentPos, targetPosition);

        if(distance > 0.1) {
            float difficulty = getDifficultypercentage();
            float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, difficulty);
            currentSpeed = currentSpeed * Time.deltaTime;
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

    public float getDifficultypercentage()
    {
        float difficulty = Mathf.Clamp01(Time.timeSinceLevelLoad / secondToMaxSpeed);
        return difficulty;
    }

    public void launch()
    {

    }
}
