using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ButtonSoundManager : MonoBehaviour
{
    public static ButtonSoundManager Instance;

    public AudioSource audioSource;
    public AudioClip clickSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }
    void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }

    void Start() { ConfigurarBotoes(); }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) { ConfigurarBotoes(); }

    private void ConfigurarBotoes()
    {
        Button[] allButtons = Resources.FindObjectsOfTypeAll<Button>();
        foreach (Button btn in allButtons)
        {
            btn.onClick.RemoveListener(PlaySound);
            btn.onClick.AddListener(PlaySound);
        }
    }

    private void PlaySound()
    {
        // SEGURANÇA TOTAL: Se o audioSource sumiu, tenta achar ou recriar
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogWarning("Som não configurado no SoundManager!");
        }
    }
}