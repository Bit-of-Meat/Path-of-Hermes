using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

// class for initialize settings
public class SettingsInit : MonoBehaviour {
    public Resolution[] Resolutions { get => _resolutions; }
    public List<string> ResolutionsOptions { get => _resolutionsOptions; }
    public int CurrentResolutionIndex { get => _currentResolutionIndex; }

    [SerializeField] private AudioMixer _audioMixer;
    
    private Resolution[] _resolutions;
    private List<string> _resolutionsOptions = new List<string>();
    private int _currentResolutionIndex = 0;

    public static SettingsInit instance;

    public void Awake() {
        instance = this;
    }

    public void Start() {
        AudioInit();
        ResolutionInit();
    }

    // Set audio settings
    private void AudioInit() {
        _audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("volume"));
    }

    // Get resolutions
    private void ResolutionInit() {
        _resolutions = Screen.resolutions;

        for (int i = 0; i < _resolutions.Length; i++) {
            Resolution _resolution = _resolutions[i];

            _resolutionsOptions.Add(_resolution.width + " x " + _resolution.height + " @" + _resolution.refreshRate);
            if (_resolution.width == Screen.currentResolution.width && _resolution.height == Screen.currentResolution.height)
                _currentResolutionIndex = i;
        }
    }
}
