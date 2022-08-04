using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour {
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    private Resolution[] _resolutions;

    public void Start() {
        _resolutions = Screen.resolutions;

        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int _currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++) {
            Resolution _resolution = _resolutions[i];

            options.Add(_resolution.width + " x " + _resolution.height + " @" + _resolution.refreshRate);
            if (_resolution.width == Screen.currentResolution.width && _resolution.height == Screen.currentResolution.height)
                _currentResolutionIndex = i;
        }
        
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = _currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex) {
        Resolution _resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume) {
        _audioMixer.SetFloat("volume", volume);
    }

    public void Back() {
        gameObject.SetActive(false);
    }
}
