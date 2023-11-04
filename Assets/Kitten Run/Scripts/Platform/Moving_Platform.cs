using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    private int moveVertical;

    void Start()
    {
        moveVertical = Random.Range(1, 3);
    }

    void Update()
    {
        if (transform.position.y < -8f)
        {
            moveVertical = 1; // Move platform up.
        }
        else if (transform.position.y > -3f)
        {
            moveVertical = 2; // Move platform down.
        }

        if (transform.position.x <= -20 || transform.position.y <= -12)
        {
            Destroy(gameObject);
        }

        if (Game_Manager.instance.gameOver == true)
        {
            moveVertical = 0;
        }

        transform.position = new Vector2(transform.position.x - Game_Manager.instance.platformSpeed * Time.deltaTime, transform.position.y);

        if (moveVertical == 1)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 5 * Time.deltaTime);
        }
        else if (moveVertical == 2)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 5 * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) // When the player lands on platform, they need to become a child of the platform to prevent them from being affected by the platform's movement velocity.
    {
        if (collision.gameObject.name == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
