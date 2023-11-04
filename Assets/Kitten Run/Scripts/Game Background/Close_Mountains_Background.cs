using UnityEngine;

public class Close_Mountains_Background : MonoBehaviour
{
    public float initialVelocity;// Velocidade de deslocamento, pode ser definida no Inspector.
    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed = 0.1f;

    private Renderer closeMountainsRenderer;

    void Start()
    {
        closeMountainsRenderer = GetComponent<Renderer>();
    }

    void Update()
    {

        if (Game_Manager.instance.gameOver)
        {
            initialVelocity = 0;
        }
        else
        {
            //aumenta a velocidade
            initialVelocity += acceleration * Time.deltaTime;

            //limitar a velocidade
            initialVelocity = Mathf.Min(initialVelocity, maxSpeed);

            Vector2 offset = new Vector2(Time.time * initialVelocity, 0f);
            closeMountainsRenderer.material.mainTextureOffset = offset;
        }
    }
}
