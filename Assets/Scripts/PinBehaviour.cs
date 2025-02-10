using TMPro;
using UnityEngine;

public class PinBehaviour : MonoBehaviour{
    public float normalSpeed = 2.0f;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    Camera cam;

    public float dashSpeed;
    public bool dashing;
    public float baseSpeed = 2.0f;
    public float speed;
    public float dashDuration;
    public float timedashStart;

    public static float cooldownRate = 5.0f;
    public static float cooldown;
    public float timelastDashEnded;

    Rigidbody2D body;

    void Start(){
        cam = Camera.main;
        body = GetComponent<Rigidbody2D>();
        dashing = false;
    }

    void Update(){
        Dash();
    }

    private void FixedUpdate()
    {
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(transform.position, mousePosG, speed * Time.fixedDeltaTime);
        transform.position = newPosition;

        body = GetComponent<Rigidbody2D>();
        Vector2 currentPostion = body.position;

        body.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collided = collision.gameObject.tag;
        Debug.Log("Collided with" + collided);
        if (collided == "Ball" || collided == "Wall")
        {
            Debug.Log("Game Over");
        }
    }

    public void Dash(){
        if (dashing == true){
            float currentTime = Time.time;
            float howLong = currentTime - timedashStart;
            if (howLong > dashDuration){
                dashing = false;
                speed = baseSpeed;

                timelastDashEnded = Time.time;
                cooldown = cooldownRate;
            }
        }
        else{
            cooldown = cooldown - Time.deltaTime;

            if (cooldown < 0.0){
                cooldown = 0.0f;
            }

            if (Input.GetMouseButtonDown(0) == true && cooldown == 0.0){
                dashing = true;
                speed = dashSpeed;
                timedashStart = Time.time;
            }
        }
    }
}
