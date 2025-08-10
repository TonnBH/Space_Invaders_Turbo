using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance { get; private set; }

    static AudioSource audioSource;
    static SoundEffectLibrary soundEffectLibrary;
    [SerializeField] Slider sfxSlider;
    [SerializeField] bool soundOn = true;    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.S))
        {
            ToggleAllAudio();            
        }
    }
    public void ToggleAllAudio()
    {
        soundOn = !soundOn;
        AudioListener.volume = soundOn ? 0f : 1f;
        Debug.Log("Audio " + (soundOn ? "Muted" : "Unmuted"));
    }

    public static void Play(string soundName)
    {
        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName);

        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }       
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public static void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void OnValueChanged()
    {
        SetVolume(sfxSlider.value);
    }
}
