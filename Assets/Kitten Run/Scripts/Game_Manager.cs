using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public static bool startCheck;
    public static float score;

    [SerializeField] GameObject[] platformPrefabs;

    [SerializeField] float[] platformYPos;
    [SerializeField] float[] platformSpawnTime;
    [SerializeField] GameObject[] objectsToSpawn; // Adição da matriz de objetos com MoveAndDestroy.
    [SerializeField] GameObject[] obstaculosToSpawn;

    public AudioSource highscoreSFX;
    public AudioSource shieldSFX;
    public AudioSource playerHitSFX;

    public GameObject instructionsWindow;
    public GameObject tapToStart;
    public GameObject newHighscoreText;
    public GameObject heartCoin;
    public GameObject bird;
    public GameObject objectsSpawn;
    public GameObject obstaculosSpawn;

    public Animator transition;
    public Animator player_Animator;
    public Animator shield_Animator;

    public Animation gameOverAnimation;
    public Animation healthBar1Animation;
    public Animation healthBar2Animation;
    public Animation healthBar3Animation;
    public Animation cameraAnimation;
    public Animation canvasAnimation;

    public TextMeshProUGUI coinsCollectedText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI yourScoreText;
    public TextMeshProUGUI abilityUsesText;

    public int coinsCollected;
    public int difficulty;
    public int bird_BG_Speed;

    public float platformSpeed;
    public float closeMountains_BG_Speed;
    public float farMountains_BG_Speed;
    public float Platform_BG_Speed;
    public float clouds_BG_Speed;
    public float water_BG_Speed;

    public bool gameOver = false;
    public bool player_Invincible = false;

    public float modifierVelocity = 1f;
    public float velocity = 3.5f;
    public float maxVelocity = 15.5f;

    private Animator instructionsWindowAnimation;
    private Animator tapToStartAnimation;

    private Animation newHighscoreAnimation;

    private int instructionStart;
    private int abilityUses;
    private int coinsTotal;
    private int obstacleType; // Used to randomise spawning platforms in PlatformSpawner().

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        score = 0;
        difficulty = 0;

        transition.Play("Transition Out");

        tapToStartAnimation = tapToStart.GetComponent<Animator>();
        instructionsWindowAnimation = instructionsWindow.GetComponent<Animator>();
        newHighscoreAnimation = newHighscoreText.GetComponent<Animation>();

        coinsTotal = PlayerPrefs.GetInt("CoinsTotal", coinsTotal);
        instructionStart = PlayerPrefs.GetInt("Instructions");
        MainMenu.abilityActive = PlayerPrefs.GetInt("Ability", MainMenu.abilityActive);

        highscoreText.text = "Highscore - " + PlayerPrefs.GetFloat("Highscore", 0).ToString("n0");

        if (MainMenu.abilityPurchased == true)
        {
            if (MainMenu.abilityCount > 0) // abilityCount is how much ability uses the player has remaining.
            {                              // This value increments by 1 if ability is in use. That value is then stored.
                MainMenu.abilityCount++;

                PlayerPrefs.SetInt("AbilityCount", MainMenu.abilityCount);
            }
        }

        if (MainMenu.abilityCount > 4) // Resets ability value back to normal when ability is used 3 times.
        {
            MainMenu.abilityCount = 0;
            MainMenu.abilityActive = 0;
            MainMenu.abilityPurchased = false;

            PlayerPrefs.SetInt("AbilityCount", MainMenu.abilityCount);
            PlayerPrefs.SetInt("Ability", MainMenu.abilityActive);
        }
    }

    void Start()
    {
        startCheck = false;

        platformSpeed = 8.5f;
        clouds_BG_Speed = 0.1f;
        farMountains_BG_Speed = 0.2f;
        Platform_BG_Speed = 0.2f;
        closeMountains_BG_Speed = 0.3f;
        water_BG_Speed = 0.6f;
        bird_BG_Speed = 6;

        coinsCollected = 0;

        Time.timeScale = 0;

        if (instructionStart == 0) // Instructions is only displayed once on the game screen, when user is playing for the first time.
        {                          // Once the instructions window is closed, the instructionStart value is updated and stored to prevent this being shown again.
            instructionsWindow.SetActive(true);
            instructionsWindowAnimation.Play("Instructions (Open)");
        }
        else
        {
            Destroy(instructionsWindow);
        }


    }

    void Update()
    {

        velocity = Mathf.Clamp(
            velocity + modifierVelocity * Time.deltaTime,
            0,
            maxVelocity

            );

        if (startCheck == false && instructionStart != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                tapToStartAnimation.SetBool("Tapped", true);
                tapToStartAnimation.SetBool("Tapped", true);
                Destroy(tapToStart, 0.3f);

                StartCoroutine(StartDelay());
            }
        }

        if (gameOver == false && startCheck == true) // Increment score.
        {
            score += 250f * Time.deltaTime;
        }

        if (gameOver == false) // When player reaches a certain score, it triggers the next difficulty - which increases the platform speed.
        {                      // Backgrounds scrolling speeds are also increased.
            if (score >= 10000 && score < 20000)
            {
                difficulty = 1;

                platformSpeed = 9f;
                clouds_BG_Speed = 0.12f;
                farMountains_BG_Speed = 0.22f;
                Platform_BG_Speed = 0.22f;
                closeMountains_BG_Speed = 0.32f;
                water_BG_Speed = 0.62f;
            }
            else if (score >= 20000 && score < 30000)
            {
                difficulty = 2;

                platformSpeed = 9.5f;
                clouds_BG_Speed = 0.14f;
                farMountains_BG_Speed = 0.24f;
                Platform_BG_Speed = 0.24f;
                closeMountains_BG_Speed = 0.34f;
                water_BG_Speed = 0.64f;
            }
            else if (score >= 30000 && score < 40000)
            {
                difficulty = 3;

                platformSpeed = 10f;
                clouds_BG_Speed = 0.16f;
                farMountains_BG_Speed = 0.26f;
                Platform_BG_Speed = 0.26f;
                closeMountains_BG_Speed = 0.36f;
                water_BG_Speed = 0.66f;
            }
            else if (score >= 40000 && score < 50000)
            {
                difficulty = 4;

                platformSpeed = 10.5f;
                clouds_BG_Speed = 0.18f;
                farMountains_BG_Speed = 0.28f;
                Platform_BG_Speed = 0.28f;
                closeMountains_BG_Speed = 0.38f;
                water_BG_Speed = 0.68f;
            }
            else if (score >= 50000 && score < 60000)
            {
                difficulty = 5;

                platformSpeed = 11f;
                clouds_BG_Speed = 0.2f;
                farMountains_BG_Speed = 0.3f;
                Platform_BG_Speed = 0.3f;
                closeMountains_BG_Speed = 0.4f;
                water_BG_Speed = 0.7f;
            }
        }


        coinsCollectedText.text = coinsCollected.ToString();
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSecondsRealtime(0.3f);

        Time.timeScale = 1;
        startCheck = true;


        StartCoroutine(HeartCoinSpawner());
        StartCoroutine(SpawnObjects());
        StartCoroutine(Obstacspawn());

        yield return new WaitForSecondsRealtime(0.6f);

        Player.fallingAnimation = true; // Changed to true to that its parameters can be used from the Player script after the game starts and not before.
    }

    public void InstructionsClose()
    {
        instructionsWindowAnimation.Play("Instructions (Close)");
        StartCoroutine(InstructionsDelay());

        Destroy(instructionsWindow, 0.3f);
    }

    IEnumerator InstructionsDelay()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        instructionStart = 1; // Value updated and stored so that instructions window is only displayed once on the game screen - when user is playing for the first time.

        PlayerPrefs.SetInt("Instructions", instructionStart);
    }

    public void GameOver()
    {
        gameOverAnimation.Play("Game Over");

        gameOver = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        StopBackgroundSpeed();

        if (score > PlayerPrefs.GetFloat("Highscore", 0)) // If player beats highscore, highscore is updated and stored.
        {
            PlayerPrefs.SetFloat("Highscore", score);
            highscoreText.text = "Highscore - " + score.ToString("n0");

            newHighscoreText.SetActive(true);
            newHighscoreAnimation.Play();

            if (Settings.soundSettings == true)
            {
                highscoreSFX.PlayDelayed(0.7f);
            }
        }

        if (coinsCollected > 0) // When player collects coins, their coins total value will be updated and stored.
        {
            coinsTotal = PlayerPrefs.GetInt("CoinsTotal", coinsTotal) + coinsCollected;

            PlayerPrefs.SetInt("CoinsTotal", coinsTotal);
        }

        if (MainMenu.abilityCount == 2) // Updates how many abilityUses the player has left if an ability has been purchased and enabled.
        {
            abilityUses = 2;
        }
        else if (MainMenu.abilityCount == 3)
        {
            abilityUses = 1;
        }
        else
        {
            abilityUses = 0;
        }

        if (MainMenu.abilityCount >= 4) // Resets ability value back to normal when ability is used 3 times.
        {
            MainMenu.abilityCount = 0;
            MainMenu.abilityActive = 0;

            PlayerPrefs.SetInt("AbilityCount", MainMenu.abilityCount);
            PlayerPrefs.SetInt("Ability", MainMenu.abilityActive);
        }

        abilityUsesText.text = abilityUses + " Ability uses left";
        yourScoreText.text = "Your Score - " + score.ToString("n0");
    }

    public void StopBackgroundSpeed()
    {
        platformSpeed = 0f;
        clouds_BG_Speed = 0f;
        farMountains_BG_Speed = 0f;
        Platform_BG_Speed = 0f;
        closeMountains_BG_Speed = 0f;
        water_BG_Speed = 0f;
    }


    IEnumerator HeartCoinSpawner() // Heart coins are instantiated at random intervals, unlike the other coins - which are spawned with their respective platform prefabs.
    {
        if (startCheck == true)
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(5, 15));

                if (gameOver == false)
                {
                    GameObject newHeartCoin = Instantiate(heartCoin, new Vector2(17.5f, Random.Range(3, -1)), Quaternion.identity);
                }
            }
        }
    }

    public float spawnInterval = 5.0f; // Intervalo entre instâncias
    public float distanceBetweenObjects = 20.0f; // Distância entre os objetos

    IEnumerator SpawnObjects()
    {
        if (startCheck == true)
        {
            int index = 0; // Variável para rastrear a posição atual na matriz.
            float nextSpawnX = 30.0f; // Posição inicial X do primeiro objeto a ser instanciado

            while (true)
            {
                yield return new WaitForSeconds(spawnInterval);

                if (gameOver == false)
                {
                    if (index >= objectsToSpawn.Length)
                    {
                        index = 0; // Reinicia a posição na matriz quando chega ao final.
                    }

                    // Calcula a posição do próximo objeto com base na distância entre objetos.
                    Vector2 spawnPosition = new Vector2(nextSpawnX, -3.5f);
                    GameObject newObject = Instantiate(objectsToSpawn[index], spawnPosition, Quaternion.identity);

                    // Atualiza a próxima posição X para o próximo objeto.
                    nextSpawnX += distanceBetweenObjects;

                    index++; // Avança para o próximo objeto na próxima iteração.
                }
            }
        }
    }

    public float spawnInterval2 = 2.0f; // Intervalo entre instâncias
    public float distanceBetweenObstaculos = 15.0f; // Distância entre os objetos

    IEnumerator Obstacspawn()
    {
        if (startCheck == true)
        {
            int index = 0; // Variável para rastrear a posição atual na matriz.
            float nextSpawnX = 15.0f; // Posição inicial X do primeiro objeto a ser instanciado

            while (true)
            {
                yield return new WaitForSeconds(spawnInterval2);

                if (gameOver == false)
                {
                    if (index >= obstaculosToSpawn.Length)
                    {
                        index = 0; // Reinicia a posição na matriz quando chega ao final.
                    }

                    // Calcula a posição do próximo objeto com base na distância entre objetos.
                    Vector2 spawnPosition = new Vector2(nextSpawnX, -3.6f);
                    GameObject newObject = Instantiate(obstaculosToSpawn[index], spawnPosition, Quaternion.identity);

                    // Atualiza a próxima posição X para o próximo objeto.
                    nextSpawnX += distanceBetweenObstaculos;

                    index++; // Avança para o próximo objeto na próxima iteração.
                }
            }
        }
    }
}