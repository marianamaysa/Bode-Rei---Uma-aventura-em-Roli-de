using UnityEngine;

public class Far_Mountains_Background : MonoBehaviour
{
    private Renderer farMountainsRenderer;

    void Start()
    {
        farMountainsRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        farMountainsRenderer.material.mainTextureOffset += new Vector2(Game_Manager.instance.farMountains_BG_Speed * Time.deltaTime, 0f); // Scrolling loop.
    }
}
