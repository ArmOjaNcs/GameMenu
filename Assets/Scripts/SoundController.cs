using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    private const string MasterVolume = nameof(MasterVolume);
    private const string ButtonsVolume = nameof(ButtonsVolume);
    private const string BackgroundVolume = nameof(BackgroundVolume);
    private const float MaxVolume = 0;
    private const float MinVolume = -80;
    private const float LowVolume = -50;

    [SerializeField] private AudioMixerGroup _audioMasterGroup;
    [SerializeField] private Slider _sliderForTotalVolume;
    [SerializeField] private Slider _sliderForBackgroundVolume;
    [SerializeField] private Slider _sliderForButtonsVolume;
    [SerializeField] private Toggle _toggleForTotalSound;

    private float _currentTotalVolume;

    private void OnEnable()
    {
        _sliderForTotalVolume.onValueChanged.AddListener(OnChangedTotalVolume);
        _sliderForBackgroundVolume.onValueChanged.AddListener(OnChangedBackgroundVolume);
        _sliderForButtonsVolume.onValueChanged.AddListener(OnChangedButtonsVolume);
        _toggleForTotalSound.onValueChanged.AddListener(OnChangedTotalSound); 
    }

    private void OnDisable()
    {
        _sliderForTotalVolume.onValueChanged.RemoveListener(OnChangedTotalVolume);
        _sliderForBackgroundVolume.onValueChanged.RemoveListener(OnChangedBackgroundVolume);
        _sliderForButtonsVolume.onValueChanged.RemoveListener(OnChangedButtonsVolume);
        _toggleForTotalSound.onValueChanged.RemoveListener(OnChangedTotalSound);
    }

    private void OnChangedTotalVolume(float volume)
    {
        _audioMasterGroup.audioMixer.SetFloat(MasterVolume, Mathf.Lerp(LowVolume, MaxVolume, volume));
        _audioMasterGroup.audioMixer.GetFloat(MasterVolume, out float currentVolume);
        _currentTotalVolume = currentVolume;
    }

    private void OnChangedBackgroundVolume(float volume)
    {
        _audioMasterGroup.audioMixer.SetFloat(BackgroundVolume, Mathf.Lerp(LowVolume, MaxVolume, volume));
    }

    private void OnChangedButtonsVolume(float volume)
    {
        _audioMasterGroup.audioMixer.SetFloat(ButtonsVolume, Mathf.Lerp(MinVolume, MaxVolume, volume));
    }

    private void OnChangedTotalSound(bool isPlaying)
    {
        if (isPlaying)
            _audioMasterGroup.audioMixer.SetFloat(MasterVolume, _currentTotalVolume);
        else
            _audioMasterGroup.audioMixer.SetFloat(MasterVolume, MinVolume);
    }
}