using System.Collections;
using TMPro;
using UnityEngine;

public class PinBehaviour : MonoBehaviour{
    public float normalSpeed = 2.0f;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    Camera cam;
    public AudioSource[] audioSources;

    public float dashSpeed;
    public bool dashing;
    public float baseSpeed = 2.0f;
    public float speed;
    public float dashDuration;
    public float timedashStart;

    public static float invincibilityDuration = 2.0f;
    public float invincibilityCooldownRate = 5.0f;
    private float invincibilityStartTime;
    private float invincibilityCooldown;
    private bool isInvincible = false;
    public static float invincibilityTimeLeft = 0f;

    public static float cooldownRate = 5.0f;
    public static float cooldown;
    public float timelastDashEnded;

    Rigidbody2D body;

    void Start(){
        cam = Camera.main;
        body = GetComponent<Rigidbody2D>();
        dashing = false;
        audioSources = GetComponents<AudioSource>();
    }

    void Update(){
        Dash();
        Invincibility();
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
        if (isInvincible) return;

        string collided = collision.gameObject.tag;
        // Debug.Log("Collided with" + collided);
        if (collided == "Ball" || collided == "Wall")
        {
            StartCoroutine(WaitForSoundAndTransition());
        }
    }

    private IEnumerator WaitForSoundAndTransition()
    {
        audioSources[0].Play();
        yield return new WaitForSeconds(audioSources[0].clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
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

                if (audioSources[1].isPlaying)
                {
                    audioSources[1].Stop();
                }
                audioSources[1].Play();
            }
        }
    }

    private void Invincibility()
    {
        if (isInvincible)
        {
            float elapsed = Time.time - invincibilityStartTime;
            float remain = invincibilityDuration - elapsed;

            if (remain > 0)
            {
                invincibilityTimeLeft = remain;
            }
            else
            {
                isInvincible = false;
                invincibilityCooldown = invincibilityCooldownRate;
                invincibilityTimeLeft = 0f;
            }
        }
        else
        {
            invincibilityCooldown -= Time.deltaTime;
            if (invincibilityCooldown < 0.0f)
                invincibilityCooldown = 0.0f;

            if (Input.GetKeyDown(KeyCode.Space) && invincibilityCooldown <= 0.0f)
            {
                isInvincible = true;
                invincibilityStartTime = Time.time;
                invincibilityTimeLeft = invincibilityDuration;

                if (audioSources.Length > 2 && audioSources[2].clip != null)
                {
                    if (audioSources[2].isPlaying)
                        audioSources[2].Stop();

                    audioSources[2].Play();
                }
            }
            else
            {
                invincibilityTimeLeft = 0f;
            }
        }
    }
}
