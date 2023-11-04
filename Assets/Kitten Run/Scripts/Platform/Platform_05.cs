using UnityEngine;

public class Platform_05 : MonoBehaviour
{
    // To create randomness, an object's active state & position (if active) is decided when platform prefab is instantiated.

    public GameObject object_Obstacle_Single;
    public GameObject object_Obstacle_Double;

    private int objectActive;
    private int objectXPos;

    void Start()
    {
        objectActive = Random.Range(0, 2);
        objectXPos = Random.Range(-1, 3);

        if (objectActive == 0)
        {
            object_Obstacle_Single.SetActive(true);

            object_Obstacle_Single.transform.position = new Vector2(object_Obstacle_Single.transform.position.x + objectXPos, object_Obstacle_Single.transform.position.y);
        }
        else
        {
            object_Obstacle_Double.SetActive(true);

            object_Obstacle_Double.transform.position = new Vector2(object_Obstacle_Double.transform.position.x + objectXPos, object_Obstacle_Double.transform.position.y);
        }
    }
}
