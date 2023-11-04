using System.Collections;
using UnityEngine;

public class CoinsCount : MonoBehaviour
{
    // This script is used for tracking how many heart coins are collected in order to regain 1 bar of health in game.

    public static CoinsCount instance;

    public static int coinsCount = 0;

    private GameObject child0_GameObject;
    private GameObject child1_GameObject;
    private GameObject child2_GameObject;

    private Animation child0_Animation;
    private Animation child1_Animation;
    private Animation child2_Animation;

    void Awake()
    {
        coinsCount = 0;

        child0_GameObject = transform.GetChild(0).gameObject;
        child1_GameObject = transform.GetChild(1).gameObject;
        child2_GameObject = transform.GetChild(2).gameObject;

        child0_Animation = transform.GetChild(0).gameObject.GetComponent<Animation>();
        child1_Animation = transform.GetChild(1).gameObject.GetComponent<Animation>();
        child2_Animation = transform.GetChild(2).gameObject.GetComponent<Animation>();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void CheckCoins()
    {
        if (Player.player_Health < 3)
        {
            if (coinsCount == 1)
            {
                child0_GameObject.SetActive(true);
                child0_Animation.Play("Health Coin (Add)");
            }
            else if (coinsCount == 2)
            {
                child1_GameObject.SetActive(true);
                child1_Animation.Play("Health Coin (Add)");
            }
            else if (coinsCount == 3)
            {
                child2_GameObject.SetActive(true);
                child2_Animation.Play("Health Coin (Add)");

                if (Player.player_Health == 2) // Will check that the player has 2 health and animate the last health bar.
                {
                    Game_Manager.instance.healthBar1Animation.Play("Health Bar (Add)");
                    Player.player_Health++;
                }
                else if (Player.player_Health == 1) // Will check that the player has 1 health and animate the middle health bar only.
                {
                    Game_Manager.instance.healthBar2Animation.Play("Health Bar (Add)");
                    Player.player_Health++;
                }

                StartCoroutine(CoinsCountReset());
            }
        }

        if (coinsCount > 3)
        {
            coinsCount = 0;
        }
    }

    public IEnumerator ResetCoins() // Used in the Player script when game over is triggered to reset the amount of heart coins collected.
    {
        coinsCount = 0;

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy == true)
            {
                child.GetComponent<Animation>().Play("Health Coin (Lose)");
            }
        }

        yield return new WaitForSeconds(0.4f);

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy == true)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator CoinsCountReset() // Reset the coins count once 3 heart coins are collected. A delay is used for asthetic purposes to view animation when the health coin disappears.
    {
        yield return new WaitForSeconds(0.4f);

        coinsCount = 0;

        foreach (Transform child in transform)
        {
            child.GetComponent<Animation>().Play("Health Coin (Lose)");
        }

        yield return new WaitForSeconds(0.4f);

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
