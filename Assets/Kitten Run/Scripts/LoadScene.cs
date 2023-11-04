using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SplashScreenEnd());

        // Detect and load music & sound settings which have been saved on the Main Menu screen.
        Settings.musicSettings = (PlayerPrefs.GetInt("MusicSettings", 1) != 0);
        Settings.soundSettings = (PlayerPrefs.GetInt("SoundSettings", 1) != 0);
    }

    IEnumerator SplashScreenEnd()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        SceneManager.LoadScene("MenuScene");
    }
}
