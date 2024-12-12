using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioUIElemets : MonoBehaviour
{
    [SerializeField] private Slider _sliderForMasterVolume;
    [SerializeField] private Slider _sliderForBackgroundVolume;
    [SerializeField] private Slider _sliderForButtonsVolume;
    [SerializeField] private Toggle _toggleForTotalSound;

    public event Action<SliderData> SliderValueChanged; 
    public event Action<ToggleData> ToggleValueChanged; 

    private void OnEnable()
    {
        _sliderForMasterVolume.onValueChanged.AddListener(OnChangedMasterVolume);
        _sliderForBackgroundVolume.onValueChanged.AddListener(OnChangedBackgroundVolume);
        _sliderForButtonsVolume.onValueChanged.AddListener(OnChangedButtonsVolume);
        _toggleForTotalSound.onValueChanged.AddListener(OnChangedTotalSound);
    }

    private void Start()
    {
        _sliderForMasterVolume.value = 0.5f;
        _sliderForBackgroundVolume.value = 0.5f;
        _sliderForButtonsVolume.value = 0.5f;
    }

    private void OnDisable()
    {
        _sliderForMasterVolume.onValueChanged.RemoveListener(OnChangedMasterVolume);
        _sliderForBackgroundVolume.onValueChanged.RemoveListener(OnChangedBackgroundVolume);
        _sliderForButtonsVolume.onValueChanged.RemoveListener(OnChangedButtonsVolume);
        _toggleForTotalSound.onValueChanged.RemoveListener(OnChangedTotalSound);
    }

    private void OnChangedMasterVolume(float volume)
    {
        float correctedVolume = GetCorrectVolume(volume);
        SliderValueChanged?.Invoke(new SliderData(AudioInfo.MasterVolume, correctedVolume));
    }

    private void OnChangedBackgroundVolume(float volume)
    {
        float correctedVolume = GetCorrectVolume(volume);
        SliderValueChanged?.Invoke(new SliderData(AudioInfo.BackgroundVolume, correctedVolume));
    }

    private void OnChangedButtonsVolume(float volume)
    {
        float correctedVolume = GetCorrectVolume(volume);
        SliderValueChanged?.Invoke(new SliderData(AudioInfo.ButtonsVolume, correctedVolume));
    }

    private void OnChangedTotalSound(bool isPlaying)
    {
        ToggleValueChanged?.Invoke(new ToggleData(AudioInfo.MasterVolume, isPlaying));
    }

    private float GetCorrectVolume(float volume)
    {
        return Mathf.Log10(volume) * AudioInfo.Multiplier;
    }
}