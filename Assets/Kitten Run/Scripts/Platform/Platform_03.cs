using UnityEngine;

public class Platform_03 : MonoBehaviour
{
    // To create randomness, an object's active state is decided when platform prefab is instantiated.

    public GameObject object_TNT;
    public GameObject object_Obstacle;

    private int objectActive;

    void Start()
    {
        objectActive = Random.Range(0, 2);

        if (objectActive == 0)
        {
            object_TNT.SetActive(true);
        }
        else
        {
            object_Obstacle.SetActive(true);
        }
    }
}
