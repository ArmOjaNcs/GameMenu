using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    private const float MinVolume = -80;

    [SerializeField] private Toggle _toggle;
    [SerializeField] private AudioSlider _slider;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    private string _parameterName;
    private bool _isStart = true;

    private void Awake()
    {
        if(_isStart)
            _parameterName = _audioMixerGroup.name;
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(SetFloat);
    }

    private void Start()
    {
        _isStart = false;
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(SetFloat);
    }

    private void SetFloat(bool isEnabled)
    {
        float volume = isEnabled ? _slider.CurrentVolume : MinVolume;
        _audioMixerGroup.audioMixer.SetFloat(_parameterName, volume);
    }
}