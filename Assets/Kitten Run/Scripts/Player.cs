using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static int player_Health = 3;
    public static int heathlCurrent;
    public static int numberOfJumpsLeft;

    public GameObject retryButton;

    public static bool fallingAnimation = false;
    public static bool isGrounded;

    public AudioSource jumpSFX;

    public GameObject shield;
    public GameObject body;

    public ParticleSystem runningDust;

    public Transform groundCheck;
    public LayerMask theGround;

    public float groundCheckRadius = 0.2f;
    public bool invincible = false; // This is triggered from the Player's 'Lose Life' animation to replicate post-hit invincibility.

    private Rigidbody2D player_RB;

    private int numberOfJumpsAllowed = 2;

    private float speed;
    private float jumpForce = 20f;
    private float JumpReleaseSpeed = 150f;

    private bool jumping;
    private bool onMovingPlatform = false;

    public GameObject gameOver;

    void Awake()
    {
        player_RB = GetComponent<Rigidbody2D>();

        speed = 0;
        player_Health = 3;
    }

    void Start()
    {
        numberOfJumpsLeft = numberOfJumpsAllowed;

        if (MainMenu.abilityActive == 1) // Check if an ability is active. If so, enable ability.
        {
            shield.SetActive(true);
        }
        else
        {
            Destroy(shield);
        }
    }

    void Update()
    {
        transform.position += transform.right * (Time.deltaTime * speed); // Always moves player to the right.

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, theGround);

        if (Game_Manager.instance.gameOver == false && Game_Manager.startCheck == true)
        {
            if (Input.GetMouseButtonDown(0) && numberOfJumpsLeft > 0)
            {
                StartCoroutine(Jump());

                Game_Manager.instance.player_Animator.SetBool("Falling", false);
            }

            if (Input.GetMouseButtonUp(0)) // When player releases the jump button, the player starts to descend. This allows you to hold the Jump button for a longer jump.
            {
                StartCoroutine(JumpDescend());
            }

            if (isGrounded == true)
            {
                ParticleSystem.EmissionModule runningDustEmission = runningDust.emission;
                runningDustEmission.rateOverTime = 15;

                jumping = false;
                numberOfJumpsLeft = numberOfJumpsAllowed;

                Game_Manager.instance.player_Animator.SetBool("Jumping", false);
                Game_Manager.instance.player_Animator.SetBool("Falling", false);
                Game_Manager.instance.player_Animator.SetBool("Is Grounded", true);

                if (transform.position.x != 6.5f) // This will ensure the player will be moved back into the correct X position if they have been moved from this position.
                {
                    transform.position = Vector2.Lerp(transform.position, new Vector2(-6.5f, transform.position.y), 0.025f);
                }
            }
            else
            {
                ParticleSystem.EmissionModule runningDustEmission = runningDust.emission;
                runningDustEmission.rateOverTime = 0;

                if (fallingAnimation == true && player_RB.velocity.y < 0)
                {
                    Game_Manager.instance.player_Animator.SetBool("Falling", true);
                }

                Game_Manager.instance.player_Animator.SetBool("Is Grounded", false);
                Game_Manager.instance.player_Animator.SetBool("Jumping", true);
            }

            if (player_RB.velocity.y < 0 && jumping == true)
            {
                if (fallingAnimation == true)
                {
                    Game_Manager.instance.player_Animator.SetBool("Falling", true);
                }
            }
        }

        if (onMovingPlatform == true)
        {
            ParticleSystem.EmissionModule runningDustEmission = runningDust.emission;
            runningDustEmission.rateOverTime = 0;
        }

        if (transform.position.x <= -14.5f || transform.position.y <= -12) // If player moves too far left or falls down, game over triggers.
        {
            Destroy(gameObject);
            Game_Manager.instance.GameOver();
        }

        if (player_Health <= 0)
        {
            Game_Manager.instance.gameOver = true;
            Game_Manager.instance.StopBackgroundSpeed();

            runningDust.Stop();

            Game_Manager.instance.player_Animator.SetBool("Game Over", true);
            retryButton.SetActive(true);
            gameOver.SetActive(true);

          //Time.timeScale = 0
        }

        Game_Manager.instance.player_Invincible = invincible;
    }

    public void Damage()
    {
        player_Health -= 1;
    }

    public IEnumerator Jump()
    {
        player_RB.velocity = new Vector2(player_RB.velocity.x, jumpForce);

        if (Settings.soundSettings == true)
        {
            jumpSFX.Play();
        }

        if (player_RB.velocity.y < 0)
        {
            player_RB.velocity = Vector2.zero;
        }

        yield return new WaitForSeconds(0.1f);

        jumping = true;
        numberOfJumpsLeft--;
    }

    public IEnumerator JumpDescend()
    {
        while (player_RB.velocity.y > 0)
        {
            Vector2 newGravity = Vector2.up * (player_RB.velocity.y - JumpReleaseSpeed * Time.deltaTime);
            player_RB.velocity = new Vector2(player_RB.velocity.x, newGravity.y);

            yield return 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "MovingPlatform") // This is used to ensure that when player lands on the moving platform, they maintain their speed. 
        {                                               // Without this, the player will move along with platform to the left as they become a child of this platform when they land on it (check MovingPlatform script).
            onMovingPlatform = true;

            if (Game_Manager.instance.difficulty == 0)
            {
                speed = 8.5f;
            }
            else if (Game_Manager.instance.difficulty == 1)
            {
                speed = 9f;
            }
            else if (Game_Manager.instance.difficulty == 2)
            {
                speed = 9.5f;
            }
            else if (Game_Manager.instance.difficulty == 3)
            {
                speed = 10f;
            }
            else if (Game_Manager.instance.difficulty == 4)
            {
                speed = 10.5f;
            }
            else if (Game_Manager.instance.difficulty == 5)
            {
                speed = 11f;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "MovingPlatform")
        {
            StartCoroutine(SlowSpeed());

            onMovingPlatform = false;
        }
    }

    public IEnumerator SlowSpeed() // Resets player's speed to normal. 
    {                              // This is only used when exiting a moving platform as the player's speed is altered when landing a moving platform. (See OnCollisionEnter2D).
        yield return new WaitForSeconds(0.01f);

        speed = 0;
    }
}
