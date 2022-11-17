using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject helpPanel;

    private void Start()
    {
        Time.timeScale = 1f;

        if (SceneManager.GetActiveScene().name == "win")
        {
            Time.timeScale = 0.10f;
        }
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadGame()
    {
        PlayClickSound();
        SceneManager.LoadScene("main");
    }

    public void LoadTitle()
    {
        PlayClickSound();
        SceneManager.LoadScene("title");
    }

    public void ToggleHelp()
    {
        if (helpPanel.activeInHierarchy)
        {
            helpPanel.SetActive(false);
        }
        else
        {
            helpPanel.SetActive(true);
        }

        PlayClickSound();
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
