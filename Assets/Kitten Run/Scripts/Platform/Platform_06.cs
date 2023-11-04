using UnityEngine;

public class Platform_06 : MonoBehaviour
{
    // To create randomness, an object's active state is decided when platform prefab is instantiated.

    public GameObject object_Obstacle;
    public GameObject coin_Ring;

    private int coinActive;
    private int objectActive;

    void Start()
    {
        coinActive = Random.Range(0, 2);
        objectActive = Random.Range(0, 2);

        if (objectActive == 0)
        {
            object_Obstacle.SetActive(true);

            object_Obstacle.transform.position = new Vector2(object_Obstacle.transform.position.x, object_Obstacle.transform.position.y);
        }

        if (coinActive == 0)
        {
            coin_Ring.SetActive(true);

            coin_Ring.transform.position = new Vector2(coin_Ring.transform.position.x, coin_Ring.transform.position.y);
        }
    }
}
