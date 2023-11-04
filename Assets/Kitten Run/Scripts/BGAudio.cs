using UnityEngine;

public class BGAudio : MonoBehaviour
{
    // This script is used to allow Audio to be played without interruption while changing scenes.

    public static BGAudio instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (Settings.musicSettings == true)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
