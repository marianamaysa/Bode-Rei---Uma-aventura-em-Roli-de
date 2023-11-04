using UnityEngine;

public class Bird_Background : MonoBehaviour
{
    private int randomSpeedValue;

    void Start()
    {
        randomSpeedValue = Game_Manager.instance.bird_BG_Speed + Random.Range(0, 1);
        Vector3 initialPosition = new Vector3(8.88f, -1.39f, 0f);
        transform.position = initialPosition;
    }

    void Update()
    {
        transform.position -= transform.right * (Game_Manager.instance.bird_BG_Speed * Time.deltaTime);

        if (transform.position.x <= -15f)
        {
            Destroy(gameObject);
        }
    }
}
