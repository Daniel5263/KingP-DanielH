using UnityEngine;

public class ballBehavior : MonoBehaviour
{
    public float minX = -4.84f;
    public float minY = -3.76f;
    public float maxX = 9.6f;
    public float maxY = 3.83f;
    public float minSpeed;
    public float maxSpeed;
    public Vector2 targetPosition;

    public GameObject target;
    public bool launching;
    public float timeLastLaunch;
    public float launchDuration;
    public float timeLaunchStart;
    public float minLaunchSpeed;
    public float maxLaunchSpeed;
    public float cooldown;

    public int secondsToMaxSpeed;

    Rigidbody2D body;
    public bool rerouting;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //secondsToMaxSpeed = 30;
        //minSpeed = 0.001f;
        //maxSpeed = 2.0f;
        targetPosition = getRandomPosition();
        initialPosition();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate()
    {
        Vector2 currentPos = gameObject.GetComponent<Transform>().position;

        body = GetComponent<Rigidbody2D>();
        Vector2 currentPostion = body.position;

        if (onCooldown() == false)
        {
            if (launching == true)
            {
                float currentLaunchTime = Time.time - timeLaunchStart;
                if (currentLaunchTime > launchDuration)
                {
                    startCooldown();
                }
            }
            else
            {
                Debug.Log("unim");
                launch();
            }
        }

        float distance = Vector2.Distance(currentPos, targetPosition);
        if (distance > 0.1f)
        {
            float difficulty = getDifficultyPercentage();
            float currentSpeed;

            if (launching == true)
            {
                float launchingForHowLong = Time.time - timeLaunchStart;
                if (launchingForHowLong > launchDuration)
                {
                    startCooldown();
                }
                currentSpeed = Mathf.Lerp(minLaunchSpeed, maxLaunchSpeed, difficulty);
            }
            else
            {
                currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, difficulty);
            }
            currentSpeed = currentSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(currentPos, targetPosition, currentSpeed);
            body.MovePosition(newPosition);
            transform.position = newPosition;
        }
        else
        { // You are at target
            if (launching == true)
            {
                startCooldown();
            }
            targetPosition = getRandomPosition();

            Debug.Log(targetPosition + " on t");
        }
    }

    Vector2 getRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 v = new Vector2(randomX, randomY);
        return v;
    }

    public float getDifficultyPercentage()
    {
        float difficulty = Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
        return difficulty;
    }

    public void initialPosition()
    {
        body = GetComponent<Rigidbody2D>();
        body.position = getRandomPosition();
        transform.position = getRandomPosition();
        targetPosition = getRandomPosition();
        launching = false;
        rerouting = true;
    }

    public void setBounds(float miX, float maX, float miY, float maY)
    {
        minX = miX;
        maxX = maX;
        minY = miY;
        maxY = maY;
    }

    public void Reroute(Collision2D collision)
    {
        GameObject otherBall = collision.gameObject;
        if (rerouting == true)
        {
            otherBall.GetComponent<ballBehavior>().rerouting = false;
            Rigidbody2D ballBody = otherBall.GetComponent<Rigidbody2D>();
            Vector2 contact = collision.GetContact(0).normal;
            targetPosition = Vector2.Reflect(targetPosition, contact).normalized;

            launching = false;

            float separationDistance = 0.1f;
            ballBody.position += contact * separationDistance;
        } else
        {
            rerouting = true;
        }
    }

    public void launch()
    {
        Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
        targetPosition = target.transform.position;

        if (launching == false)
        {
            timeLaunchStart = Time.time;
            launching = true;
            Debug.Log(launching + " on L");
        }
    }

    public bool onCooldown()
    {
        float timeSinceLastLaunch = Time.time - timeLastLaunch;
        if (timeSinceLastLaunch < cooldown)
        {
            return true;
        }
        return false;
    }


    public void startCooldown()
    {
        timeLastLaunch = Time.time;
        launching = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            targetPosition = getRandomPosition();
        }

        if (collision.gameObject.tag == "Ball")
        {
            Reroute(collision);
        }
       // Debug.Log(this + "Collided with: " + collision.gameObject.name);
    }

    public void setTarget(GameObject pin)
    {
        target = pin;
    }
}