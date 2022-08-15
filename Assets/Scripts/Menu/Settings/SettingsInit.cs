using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

// class for initialize settings
public class SettingsInit : MonoBehaviour {
    public static SettingsInit Instance { get; private set; }
    public static Resolution[] Resolutions { get; private set; }
    public static List<string> ResolutionsOptions { get; private set; }
    public static int CurrentResolutionIndex { get; private set; }

    [SerializeField] private AudioMixer _audioMixer;
    
    private void Awake() {
        if (!Instance) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            Load();
        } else {
            Destroy(gameObject);
            Debug.LogWarning($"More than one {this} instance found.");
        }
    }

    private void Load() {
        AudioInit(_audioMixer);
        ResolutionInit();
    }

    // Set audio settings
    private static void AudioInit(AudioMixer audioMixer) {
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("volume"));
    }

    // Get resolutions
    private static void ResolutionInit() {
        Resolutions = Screen.resolutions;
        ResolutionsOptions = new List<string>();

        for (int i = 0; i < Resolutions.Length; i++) {
            Resolution _resolution = Resolutions[i];

            ResolutionsOptions.Add(_resolution.width + " x " + _resolution.height + " @" + _resolution.refreshRate);
            if (_resolution.width == Screen.currentResolution.width && _resolution.height == Screen.currentResolution.height)
                CurrentResolutionIndex = i;
        }
    }
}
