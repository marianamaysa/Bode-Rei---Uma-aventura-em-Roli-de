using UnityEngine;

public class Clouds_Background : MonoBehaviour
{
    private Renderer cloudsRenderer;
    private float acceleration = 0.5f;

    void Start()
    {
        cloudsRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        //aumenta a velocidade
        Game_Manager.instance.closeMountains_BG_Speed += acceleration * Time.deltaTime;

        //atualiza a textura conforme a velocidade
        cloudsRenderer.material.mainTextureOffset += new Vector2(Game_Manager.instance.clouds_BG_Speed * Time.deltaTime, 0f); // Scrolling loop.
    }
}
