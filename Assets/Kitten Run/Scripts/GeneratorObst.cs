using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorObst : MonoBehaviour
{

    public GameObject obstaclePrefab;

    public float delayInitial;
    public float intervale;
    void Start()
    {
        InvokeRepeating("GenerateObstacle", delayInitial, intervale);
    }
    void GenerateObstacle()
    {
        Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
    }
}
