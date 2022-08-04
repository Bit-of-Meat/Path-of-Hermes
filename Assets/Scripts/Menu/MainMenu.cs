using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField] private GameObject _settings;

    public void Play() {
        SceneManager.LoadScene("Game");
    }

    public void Quit() {
        Application.Quit();
    }

    public void Settings() {
        _settings.SetActive(true);
    }
}
