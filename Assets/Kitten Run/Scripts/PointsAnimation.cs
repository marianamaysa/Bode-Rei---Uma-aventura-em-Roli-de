using UnityEngine;

public class PointsAnimation : MonoBehaviour
{
    // A simple animation when points appear after collecting a coin.

    private float moveSpeed = 0;

    void Start()
    {
        moveSpeed = -1f;
    }

    void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
}
