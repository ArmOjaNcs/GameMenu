using UnityEngine;
using UnityEngine.Audio;

public class MainAudioMixer : MonoBehaviour
{
    private const float MinVolume = -80;

    [SerializeField] private AudioMixerGroup _masterAudioGroup;

    private float _currentMasterVolume;
    
    public void SetFloatBySliderValue(string nameOfSource, float volume)
    {
        if(nameOfSource == MixerTypes.MasterVolume.ToString())
            _currentMasterVolume = volume;

        SetFloat(nameOfSource, volume);
    }

    public void SetFloatByToggle(bool isEnabled)
    {
        float volume = isEnabled ? _currentMasterVolume : MinVolume;
        SetFloat(MixerTypes.MasterVolume.ToString(), volume);
    }

    private void SetFloat(string nameOfSource, float volume)
    {
        _masterAudioGroup.audioMixer.SetFloat(nameOfSource, volume);
    }
}