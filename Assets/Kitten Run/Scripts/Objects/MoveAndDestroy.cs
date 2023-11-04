using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDestroy : MonoBehaviour
{
    public static MoveAndDestroy instance;
    public float objectSpeed = 0.2f;
    public float destroyOffset = 15.0f; // Distância após a qual o objeto será destruído
    public Vector3 moveDirection = Vector3.left; // Direção na qual o objeto se move

    void Update()
    {
        if (Game_Manager.instance.gameOver)
        {
            objectSpeed = 0;
        }
        else
        {
            // Move o objeto na direção especificada com a velocidade especificada
            transform.position += moveDirection * objectSpeed * Time.deltaTime;
        }
    }
}
