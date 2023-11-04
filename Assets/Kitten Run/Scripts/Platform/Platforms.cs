using UnityEngine;

public class Platforms : MonoBehaviour
{
    void Update()
    {
        transform.position -= transform.right * (Game_Manager.instance.platformSpeed * Time.deltaTime);

        if (transform.position.x <= -22.5f)
        {
            Destroy (gameObject);
        }
    }
}
