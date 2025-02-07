using TMPro;
using UnityEngine;

public class PinBehaviour : MonoBehaviour{
    public float speed;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    Camera cam;

    Rigidbody2D body;

    void Start(){
        cam = Camera.main;

    }

    void Update(){
        
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
}
