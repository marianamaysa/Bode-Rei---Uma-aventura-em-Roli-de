using UnityEngine;

public class Coins_Red : MonoBehaviour
{
    public GameObject _150Prefab;

    private CircleCollider2D coinCollider;
    private AudioSource coinSFX;
    private Animator coinsAnimator;

    void Start()
    {
        coinCollider = GetComponent<CircleCollider2D>();
        coinSFX = GetComponent<AudioSource>();
        coinsAnimator = GetComponent<Animator>();

        coinsAnimator.SetBool("Red Coin", true);
    }

    void Update()
    {
        if (transform.position.x <= -15 || transform.position.y > 4.2f)
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

        if (other.gameObject.tag == "Obstacles")
        {
            Destroy(gameObject);
        }
    }

    void CollectCoin()
    {
        coinCollider.enabled = false;

        Game_Manager.score = Game_Manager.score + 150; // Add 150 to your score.
        Game_Manager.instance.coinsCollected++; // Add 1 to your coins collected value.

        coinsAnimator.SetBool("Collected", true);

        GameObject new_150 = Instantiate(_150Prefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

        if (Settings.soundSettings == true)
        {
            coinSFX.Play();
        }

        Destroy(new_150, 1);
        Destroy(gameObject, 0.45f);
    }
}
