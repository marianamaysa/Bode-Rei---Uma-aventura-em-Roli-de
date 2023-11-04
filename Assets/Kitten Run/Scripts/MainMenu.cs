using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public static int abilityActive; // Check if ability is active and available to use. Ability is not active unless it equals 1.
    public static int abilityCount = 0; // Used to store how many ability uses remaining if player exits games, so they can continue where they left off. Incremented in the Awake function of the Game Manager script.
    public static bool abilityPurchased = false;

    public GameObject abilityActiveCheckMark;

    public Animator instructionsPopUpAnimation;
    public Animation titleAnimation;
    public Animation instructionsButtonAnimation;
    public Animation abilityStorePopUpAnimation;
    public Animation messagePopUpAnimation;

    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI coinsCountText;
    public TextMeshProUGUI messagePopUpText;
    public TextMeshProUGUI abilityUsesText;

    private int coinsTotal;
    private int coinsNeeded;
    private int abilityUsesLeft; // Check how many more ability uses are remaining.

    private float currentCoinsCount; // Current amount coins player has available.

    private bool instructionsToggle = false;
    private bool abilityStoreToggle = false;
    private bool updateCoinsText = false;
    private bool messagePopUpOpen = false;

    void Awake()
    {
        highscoreText.text = "Highscore - " + PlayerPrefs.GetFloat("Highscore", 0).ToString("n0");
        coinsCountText.text = PlayerPrefs.GetInt("CoinsTotal", 0).ToString();

        // Load amount of coins Player has earned.
        coinsTotal = PlayerPrefs.GetInt("CoinsTotal", coinsTotal);
        currentCoinsCount = PlayerPrefs.GetInt("CoinsTotal", coinsTotal);

        // Load ability uses value the player has, if they have any remaining.
        abilityCount = PlayerPrefs.GetInt("AbilityCount", abilityCount);
        abilityActive = PlayerPrefs.GetInt("Ability", abilityActive);

        coinsNeeded = 50 - PlayerPrefs.GetInt("CoinsTotal"); // 50 is amount of coins needed to purchase ability. This can be changed with any number required.

        titleAnimation.Play();

        if (abilityCount > 0 && abilityCount < 4) // Load ability if an ability is active and available and abilityCount is between 0 - 4.
        {
            if (abilityActive == 1)
            {
                ShieldAbility();
            }

            abilityPurchased = true;

            AbilityUses();
        }
        else
        {
            abilityCount = 0;
            abilityActive = 0;
            abilityPurchased = false;

            abilityActiveCheckMark.SetActive(false);

            PlayerPrefs.SetInt("AbilityCount", abilityCount);
            PlayerPrefs.SetInt("Ability", abilityActive);
        }
    }

    void Update()
    {
        if (updateCoinsText == true) // Update text displaying how many coins the player has.
        {
            if (currentCoinsCount >= coinsTotal)
            {
                currentCoinsCount -= 50 * Time.deltaTime;

                coinsCountText.text = currentCoinsCount.ToString("0");
            }
            else
            {
                currentCoinsCount = coinsTotal;
                updateCoinsText = false;

                coinsCountText.text = currentCoinsCount.ToString("0");
            }
        }
    }

    public void Instructions()
    {
         if (instructionsToggle == true)
        {
            instructionsToggle = false;
            InstructionsClose();
        }
        else
        {
            instructionsToggle = true;
            InstructionsOpen();
        }

        instructionsButtonAnimation.Play("Button Toggle");
    }

    public void InstructionsOpen()
    {
        if (abilityStoreToggle == false)
        {
            instructionsPopUpAnimation.Play("Instructions (Open)");
        }
        else
        {
            instructionsToggle = false;
        }
    }

    public void InstructionsClose()
    {
        instructionsPopUpAnimation.Play("Instructions (Close)");

        instructionsToggle = false;
    }

    public void AbilityStoreToggle()
    {
        if (abilityStoreToggle == true)
        {
            abilityStoreToggle = false;
            AbilityStoreClose();
        }
        else
        {
            abilityStoreToggle = true;
            AbilityStoreOpen();
        }
    }

    public void AbilityStoreOpen()
    {
        if (instructionsToggle == false)
        {
            abilityStorePopUpAnimation.Play("Ability Store (Open)");
        }
        else
        {
            abilityStoreToggle = false;
        }
    }

    public void AbilityStoreClose()
    {
        abilityStorePopUpAnimation.Play("Ability Store (Close)");

        abilityStoreToggle = false;

        if (messagePopUpOpen == true)
        {
            MessagePopUpClose();
        }
    }

    void MessagePopUp()
    {
        messagePopUpAnimation.Play("Message Pop Up (Open)");

        messagePopUpText.text = "Not enough coins..." + "\n" + "\n" + "You need  " + coinsNeeded + "  more coins.";

        messagePopUpOpen = true;
    }

    public void MessagePopUpClose()
    {
        messagePopUpAnimation.Play("Message Pop Up (Close)");

        messagePopUpOpen = false;
    }

    public void BuyAbility()
    {
        if (abilityPurchased == false)
        {
            if (PlayerPrefs.GetInt("CoinsTotal") >= 50)
            {
                abilityPurchased = true;
                updateCoinsText = true;
                abilityCount = 1;

                coinsTotal = PlayerPrefs.GetInt("CoinsTotal", coinsTotal) - 50;

                PlayerPrefs.SetInt("CoinsTotal", coinsTotal);
                PlayerPrefs.SetInt("AbilityCount", abilityCount);

                AbilityUses();
            }
            else
            {
                abilityActiveCheckMark.SetActive(false);

                MessagePopUp();

                abilityActive = 0;

                PlayerPrefs.SetInt("Ability", abilityActive);
            }
        }
        else
        {
            messagePopUpAnimation.Play("Message Pop Up (Open)");

            messagePopUpText.text = "You have an ability in use." + "\n" + "\n" + "You can buy another ability once" + "\n" + "your current one finishes.";

            messagePopUpOpen = true;
        }
    }

    public void ShieldAbility()
    {
        if (abilityPurchased == false)
        {
            abilityActiveCheckMark.SetActive(true);

            abilityActive = 1;

            PlayerPrefs.SetInt("Ability", abilityActive);
        }

        if (abilityActive == 1)
        {
            abilityActiveCheckMark.SetActive(true);
        }
    }

    public void AbilityUses() // abilityCount is incremented in the Awake function of the Game Manager script.
    {                         // When this increases, the ability uses value decreases. Thus, decresing how much more times you can use your ability.
        if (abilityCount == 1)
        {
            abilityUsesLeft = 3;
        }
        else if (abilityCount == 2)
        {
            abilityUsesLeft = 2;
        }
        else if (abilityCount == 3)
        {
            abilityUsesLeft = 1;
        }
        else
        {
            abilityUsesLeft = 0;
        }

        if (abilityCount > 0)
        {
            abilityUsesText.text = "Ability uses left - " + abilityUsesLeft;
        }
    }
}
