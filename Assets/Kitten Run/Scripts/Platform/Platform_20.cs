using UnityEngine;

public class Platform_20 : MonoBehaviour
{
    // To create randomness, an object's active state is decided when platform prefab is instantiated.

    public GameObject object_Single_Left;
    public GameObject object_Double_Left;
    public GameObject object_TNT_Right;
    public GameObject object_Double_Right;

    private int objectActive_Left;
    private int objectActive_Right;

    void Start()
    {
        objectActive_Left = Random.Range(0, 2);
        objectActive_Right = Random.Range(0, 2);

        if (objectActive_Left == 0)
        {
            object_Single_Left.SetActive(true);

            object_Single_Left.transform.position = new Vector2(object_Single_Left.transform.position.x, object_Single_Left.transform.position.y);
        }
        else
        {
            object_Double_Left.SetActive(true);

            object_Double_Left.transform.position = new Vector2(object_Double_Left.transform.position.x, object_Double_Left.transform.position.y);
        }

        if (objectActive_Right == 0)
        {
            object_TNT_Right.SetActive(true);

            object_TNT_Right.transform.position = new Vector2(object_TNT_Right.transform.position.x, object_TNT_Right.transform.position.y);
        }
        else
        {
            object_Double_Right.SetActive(true);

            object_Double_Right.transform.position = new Vector2(object_Double_Right.transform.position.x, object_Double_Right.transform.position.y);
        }
    }
}
