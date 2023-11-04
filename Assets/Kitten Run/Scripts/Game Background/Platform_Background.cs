using UnityEngine;

public class Platform_Background : MonoBehaviour
{
    private Renderer PlatformRenderer;
    private float acceleration = 0.1f;

    void Start()
    {
        PlatformRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        //aumenta a velocidade
        Game_Manager.instance.Platform_BG_Speed += acceleration * Time.deltaTime;

        //atualiza a textura conforme a velocidade
        PlatformRenderer.material.mainTextureOffset += new Vector2(Game_Manager.instance.Platform_BG_Speed * Time.deltaTime, 0f); // Scrolling loop.
    }
}
