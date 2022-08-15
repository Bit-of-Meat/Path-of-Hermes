using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {
    [SerializeField] private GameObject _settings;
    public void Show(InputAction.CallbackContext context) => Show();

    public void Show() {
        bool _uiState = gameObject.activeSelf;
        PlayerInput.SetCursorLock(_uiState);
        gameObject.SetActive(!_uiState);
        Time.timeScale = _uiState ? 1f : 0f;
    }

    public void Settings() {
        _settings.SetActive(true);
    }

    public void BackToMainMenu() {
        Time.timeScale = 1f;
        SceneLoader.LoadScene("MainMenu");
    }

    public void Quit() {
        Application.Quit();
    }
}
