using UnityEngine;

public class Platform_12 : MonoBehaviour
{
    // To create randomness, an object's active state & position (if active) is decided when platform prefab is instantiated.

    public GameObject object_TNT;
    public GameObject object_Obstacle_Double;
    public GameObject coin_Red;

    private int coinActive;
    private int objectActive;
    private int objectXPos;

    void Start()
    {
        coinActive = Random.Range(0, 2);
        objectActive = Random.Range(0, 3);
        objectXPos = Random.Range(-1, 3);

        if (objectActive == 0)
        {
            object_TNT.SetActive(true);

            object_TNT.transform.position = new Vector2(object_TNT.transform.position.x + objectXPos, object_TNT.transform.position.y);
        }
        else if (objectActive == 1)
        {
            object_Obstacle_Double.SetActive(true);

            object_Obstacle_Double.transform.position = new Vector2(object_Obstacle_Double.transform.position.x + objectXPos, object_Obstacle_Double.transform.position.y);
        }

        if (coinActive == 1)
        {
            coin_Red.SetActive(true);

            coin_Red.transform.position = new Vector2(coin_Red.transform.position.x, coin_Red.transform.position.y);
        }
    }
}
