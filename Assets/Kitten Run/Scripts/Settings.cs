using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public static bool musicSettings = true;
    public static bool soundSettings = true;

    public GameObject musicButton;
    public GameObject soundButton;

    public Sprite musicOn;
    public Sprite musicOff;
    public Sprite soundOn;
    public Sprite soundOff;

    public TextMeshProUGUI musicText;
    public TextMeshProUGUI soundText;

    private AudioSource bgAudioSource;

    private Image musicImage;
    private Image soundImage;

    private Animation musicAnimation;
    private Animation soundAnimation;

    private Toggle musicToggle;
    private Toggle soundToggle;

    void Awake()
    {
        musicImage = musicButton.GetComponent<Image>();
        soundImage = soundButton.GetComponent<Image>();

        musicAnimation = musicButton.GetComponent<Animation>();
        soundAnimation = soundButton.GetComponent<Animation>();

        musicToggle = musicButton.GetComponent<Toggle>();
        soundToggle = soundButton.GetComponent<Toggle>();
    }

    void Start()
    {
        bgAudioSource = BGAudio.instance.GetComponent<AudioSource>(); // Create reference to BGAudio script to play music when loaded.

        if (musicSettings == true)
        {
            musicToggle.isOn = true;
        }
        else
        {
            musicToggle.isOn = false;
        }

        if (soundSettings == true)
        {
            soundToggle.isOn = true;
        }
        else
        {
            soundToggle.isOn = false;
        }

        musicToggle.onValueChanged.AddListener(delegate { MusicValueChanged(musicToggle); });
        soundToggle.onValueChanged.AddListener(delegate { SoundValueChanged(soundToggle); });
    }

    public void MusicSetting()
    {
        if (musicToggle.isOn)
        {
            MusicOn();
            musicSettings = true;
        }
        else
        {
            MusicOff();
            musicSettings = false;
        }

        PlayerPrefs.SetInt("MusicSettings", (musicSettings ? 1 : 0)); // Save music settings.
    }

    public void MusicOn()
    {
        musicImage.sprite = musicOn;
        //musicText.text = "Music On";

        if (bgAudioSource.isPlaying == false)
        {
            bgAudioSource.Play();
        }
    }

    public void MusicOff()
    {
        musicImage.sprite = musicOff;
        //musicText.text = "Music Off";

        if (bgAudioSource.isPlaying == true)
        {
            bgAudioSource.Pause();
        }
    }

    public void MusicValueChanged(Toggle musicToggle)
    {
        musicAnimation.Play("Button Toggle");
    }

    public void SoundSetting()
    {
        if (soundToggle.isOn)
        {
            SoundOn();
            soundSettings = true;
        }
        else
        {
            SoundOff();
            soundSettings = false;
        }

        PlayerPrefs.SetInt("SoundSettings", (soundSettings ? 1 : 0)); // Save Sound settings.
    }

    public void SoundOn()
    {
        soundImage.sprite = soundOn;
        //soundText.text = "Sound On";

        if (bgAudioSource.isPlaying == false)
        {
            bgAudioSource.Play();
        }
    }

    public void SoundOff()
    {
        soundImage.sprite = soundOff;
        //soundText.text = "Sound Off";

        if (bgAudioSource.isPlaying == true)
        {
            bgAudioSource.Pause();
        }

    }

    public void SoundValueChanged(Toggle soundToggle)
    {
        soundAnimation.Play("Button Toggle");
    }
}
