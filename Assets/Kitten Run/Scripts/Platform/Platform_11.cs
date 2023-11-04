using UnityEngine;

public class Platform_11 : MonoBehaviour
{
    // To create randomness, an object's active state is decided when platform prefab is instantiated.

    public GameObject object_Obstacle_Single;
    public GameObject object_Obstacle_Double;

    private int objectActive;
    private int objectXPos;

    void Start()
    {
        objectActive = Random.Range(0, 2);
        objectXPos = Random.Range(-1, 2);

        if (objectActive == 0)
        {
            object_Obstacle_Single.SetActive(true);

            object_Obstacle_Single.transform.position = new Vector2(object_Obstacle_Single.transform.position.x, object_Obstacle_Single.transform.position.y);
        }
        else if (objectActive == 1)
        {
            object_Obstacle_Double.SetActive(true);

            object_Obstacle_Double.transform.position = new Vector2(object_Obstacle_Double.transform.position.x, object_Obstacle_Double.transform.position.y);
        }
    }
}
