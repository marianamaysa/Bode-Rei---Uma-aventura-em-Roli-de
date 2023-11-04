using UnityEngine;

public class Coins_Heart : MonoBehaviour
{
    public GameObject _500Prefab;
    public GameObject pickupEffect;

    private GameObject coinsCount;
    private CoinsCount coinsCheck;
    private CircleCollider2D coinCollider;
    private Animator coinsAnimator;

    void Awake()
    {
        coinsCount = GameObject.Find("Heart Coins Count"); // Find the gameobject in the editor that has the Coins Count script attached.

        coinsCheck = coinsCount.GetComponent<CoinsCount>(); // Create a reference to the Coins Count script.
        coinCollider = GetComponent<CircleCollider2D>();
        coinsAnimator = GetComponent<Animator>();

        coinsAnimator.SetBool("Heart Coin", true);
    }

    void Update()
    {
        transform.position -= transform.right * (Time.deltaTime * Game_Manager.instance.platformSpeed);

        if (transform.position.x <= -15)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player other_Player;
        Collider2D other_collider;

        other_Player = other.GetComponent<Player>();
        other_collider = other.GetComponent<Collider2D>();

        if (other_Player != null)
        {
            CollectCoin();
        }

        if (other.gameObject.tag == "Obstacles" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Coins")
        {
            Destroy(gameObject);
        }
    }

    void CollectCoin()
    {
        coinCollider.enabled = false;

        Game_Manager.score = Game_Manager.score + 500; // Add 500 to your score.
        Game_Manager.instance.coinsCollected++; // Add 1 to your coins collected value.

        coinsAnimator.SetBool("Collected", true);

        Instantiate(pickupEffect, transform.position, transform.rotation);

        GameObject new_500 = Instantiate(_500Prefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

        Destroy(new_500, 1);
        Destroy(gameObject, 1);

        if (Player.player_Health < 3) // If the player's health is less than 3 (full health), then this will count as a health coin to regain 1 health bar.
        {
            CoinsCount.coinsCount++;
            coinsCheck.CheckCoins();
        }
    }
}
