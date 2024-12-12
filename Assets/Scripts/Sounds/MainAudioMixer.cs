using UnityEngine;
using UnityEngine.Audio;

public class MainAudioMixer : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _masterAudioGroup;
    [SerializeField] private AudioUIElemets _audioUIElements;

    private float _currentMasterVolume;

    private void OnEnable()
    {
        _audioUIElements.SliderValueChanged += OnSliderValueChanged;
        _audioUIElements.ToggleValueChanged += OnToggleValueChanged;
    }

    private void OnDisable()
    {
        _audioUIElements.SliderValueChanged -= OnSliderValueChanged;
        _audioUIElements.ToggleValueChanged -= OnToggleValueChanged;
    }

    private void SetFloat(string nameOfSource, float volume)
    {
        _masterAudioGroup.audioMixer.SetFloat(nameOfSource, volume);
    }

    private void OnSliderValueChanged(SliderData sliderData)
    {
        if(sliderData.SourceName == AudioInfo.MasterVolume)
            _currentMasterVolume = sliderData.Value;

        SetFloat(sliderData.SourceName, sliderData.Value);
    }

    private void OnToggleValueChanged(ToggleData toggleData)
    {
        float volume = toggleData.IsEnabled ? _currentMasterVolume : AudioInfo.MinVolume;
        SetFloat(toggleData.SourceName, volume);
    }
}