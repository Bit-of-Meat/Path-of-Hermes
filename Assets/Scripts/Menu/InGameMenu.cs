using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {
    [SerializeField] private GameObject _ui;
    public void Show(InputAction.CallbackContext context) => Show();

    public void Show() {
        bool _uiState = _ui.activeSelf;
        PlayerInput.SetCursorLock(_uiState);
        _ui.SetActive(!_uiState);
        Time.timeScale = _uiState ? 1f : 0.1f;
    }

    public void BackToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit() {
        Application.Quit();
    }
}
