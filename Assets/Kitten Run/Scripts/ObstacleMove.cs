using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public Vector2 direction;
    [SerializeField] float velocityInitial;
    [SerializeField] float maxVelocity = 15f;
    [SerializeField] float rateSpeed = 0.05f;
    [SerializeField] float atualVelocity;

    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
        }
    }

    void Update()
    {
        if (atualVelocity < maxVelocity)
        {
            atualVelocity += rateSpeed * Time.deltaTime;
        }

        Vector3 move = new Vector3(atualVelocity * Time.deltaTime, 0, 0);
        transform.Translate(move);

        if (Game_Manager.instance.gameOver)
        {
            atualVelocity = 0;
        }
        else
        {
            // Move o objeto na direção da esquerda com a velocidade especificada
            transform.Translate(direction * atualVelocity * Time.deltaTime);
        }

    }
}
