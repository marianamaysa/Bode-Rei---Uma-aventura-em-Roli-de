using UnityEngine;

public class Platform_19 : MonoBehaviour
{
    // To create randomness, an object's active state is decided when platform prefab is instantiated.

    public GameObject coin_Star;
    private int coinActive;

    void Start()
    {
        coinActive = Random.Range(0, 2);

        if (coinActive == 1)
        {
            coin_Star.SetActive(true);

            coin_Star.transform.position = new Vector2(coin_Star.transform.position.x, coin_Star.transform.position.y);
        }
    }
}
