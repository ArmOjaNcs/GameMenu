using UnityEngine;
using UnityEngine.Audio;

public class MainAudioMixer : MonoBehaviour
{
    private const float MinVolume = -80;

    [SerializeField] private AudioMixerGroup _masterAudioGroup;

    private float _currentMasterVolume;
    
    public void SetFloatBySliderValue(string nameOfSource, float volume)
    {
        if(nameOfSource == AudioSourceTypes.MasterVolume.ToString())
            _currentMasterVolume = volume;

        SetFloat(nameOfSource, volume);
    }

    public void SetFloatByToggle(bool isEnabled)
    {
        float volume = isEnabled ? _currentMasterVolume : MinVolume;
        SetFloat(AudioSourceTypes.MasterVolume.ToString(), volume);
    }

    private void SetFloat(string nameOfSource, float volume)
    {
        _masterAudioGroup.audioMixer.SetFloat(nameOfSource, volume);
    }
}