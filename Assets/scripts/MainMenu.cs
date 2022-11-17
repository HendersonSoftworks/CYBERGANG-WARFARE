using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject helpPanel;

    public void LoadGame()
    {
        PlayClickSound();
        SceneManager.LoadScene("main");
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
