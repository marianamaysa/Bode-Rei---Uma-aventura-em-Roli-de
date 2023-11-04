using UnityEngine;

public class Water_Background : MonoBehaviour
{
    private Renderer waterRenderer;

    void Start()
    {
        waterRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        waterRenderer.material.mainTextureOffset += new Vector2(Game_Manager.instance.water_BG_Speed * Time.deltaTime, 0f); // Scrolling loop.
    }
}
