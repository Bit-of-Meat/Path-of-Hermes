using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour {
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private Slider _audioSlider;

    private void Start() {
        _audioSlider.value = PlayerPrefs.GetFloat("volume");
        
        _resolutionDropdown.ClearOptions();
        _resolutionDropdown.AddOptions(SettingsInit.instance.ResolutionsOptions);
        _resolutionDropdown.value = SettingsInit.instance.CurrentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex) {
        Resolution _resolution = SettingsInit.instance.Resolutions[resolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume) {
        _audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void Back() {
        gameObject.SetActive(false);
    }
}
