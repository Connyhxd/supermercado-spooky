using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("SLIDER")]
    public AudioMixer mixer;
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SfxSlider;

    [Header("AUDIO SOURCE")]
    public AudioSource sfxSound;

    [Header("AUDIO RESOURCE")]
    public AudioResource pickupSound;
    public AudioResource buySound;
    public AudioResource pauseOpenSound;
    public AudioResource pauseCloseSound;
    public AudioResource listOpenSound;
    public AudioResource listCloseSound;
    public AudioResource dialogueStartSound;
    public AudioResource dialogueEndSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            return;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        mixer.SetFloat("MASTERSOUND", MasterSlider.value);
        mixer.SetFloat("MUSICSOUND", MusicSlider.value);
        mixer.SetFloat("SFXSOUND", SfxSlider.value);
    }
}
