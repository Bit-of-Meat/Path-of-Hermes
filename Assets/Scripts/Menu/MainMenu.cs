using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField] private GameObject _settings;

    public void Play() {
        SceneLoader.LoadScene("Level01");
    }

    public void Quit() {
        Application.Quit();
    }

    public void Settings() {
        _settings.SetActive(true);
    }
}
