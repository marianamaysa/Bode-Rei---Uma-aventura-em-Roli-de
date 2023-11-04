using UnityEngine;

public class Platform_17 : MonoBehaviour
{
    // To create randomness, an object's active state is decided when platform prefab is instantiated.

    public GameObject object_Obstacle;
    private int objectActive;

    void Start()
    {
        objectActive = Random.Range(0, 2);

        if (objectActive == 1)
        {
            object_Obstacle.SetActive(true);
        }
    }
}
