using UnityEngine;

public class Platform_13 : MonoBehaviour
{
    // To create randomness, an object's active state is decided when platform prefab is instantiated.

    public GameObject object_Single_Left;
    public GameObject object_Double_Left;
    public GameObject object_Single_Right;
    public GameObject object_Double_Right;
    public GameObject coin_Red;

    private int coinActive;
    private int objectActive_Left;
    private int objectActive_Right;

    void Start()
    {
        coinActive = Random.Range(0, 2);
        objectActive_Left = Random.Range(0, 3);
        objectActive_Right = Random.Range(0, 2);

        if (objectActive_Left == 0)
        {
            object_Single_Left.SetActive(true);

            object_Single_Left.transform.position = new Vector2(object_Single_Left.transform.position.x, object_Single_Left.transform.position.y);
        }
        else if (objectActive_Left == 1)
        {
            object_Double_Left.SetActive(true);

            object_Double_Left.transform.position = new Vector2(object_Double_Left.transform.position.x, object_Double_Left.transform.position.y);
        }

        if (objectActive_Right == 0)
        {
            object_Single_Right.SetActive(true);

            object_Single_Right.transform.position = new Vector2(object_Single_Right.transform.position.x, object_Single_Right.transform.position.y);
        }
        else
        {
            object_Double_Right.SetActive(true);

            object_Double_Right.transform.position = new Vector2(object_Double_Right.transform.position.x, object_Double_Right.transform.position.y);
        }

        if (coinActive == 0)
        {
            coin_Red.SetActive(true);
        }
        else
        {
            coin_Red.SetActive(true);
        }
    }
}
