using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayerManager : MonoBehaviour
{
    [Header("Audio Clip")]
    [SerializeField] private AudioClip mainMenuBGM;
    [SerializeField] private AudioClip levelBGM;

    private static AudioPlayerManager instance = null;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu" && audioSource.clip != mainMenuBGM)
        {
            audioSource.clip = mainMenuBGM;
            PlaySceneBGM();
        }
        if (SceneManager.GetActiveScene().name == "Lv 1" && audioSource.clip != levelBGM)
        {
            audioSource.clip = levelBGM;
            PlaySceneBGM();
        }
        if (SceneManager.GetActiveScene().name == "Lv 2" && audioSource.clip != levelBGM)
        {
            audioSource.clip = levelBGM;
            PlaySceneBGM();
        }
    }

    void PlaySceneBGM()
    {
        audioSource.Stop();
        audioSource.Play();
    }
}
