using UnityEngine;

public class Coins_Heart_Pickup_Effect : MonoBehaviour
{
    private AudioSource heartCoinSFX;

    void Start()
    {
        heartCoinSFX = GetComponent<AudioSource>();

        if (Settings.soundSettings == true)
        {
            heartCoinSFX.Play();
        }
    }

    void Update()
    {
        transform.position -= transform.right * (Time.deltaTime * Game_Manager.instance.platformSpeed);

        if (transform.position.x <= -15)
        {
            Destroy(gameObject);
        }
    }
}
