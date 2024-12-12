using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    private const float Multiplier = 20;

    [SerializeField] private Slider _slider;
    [SerializeField] private MainAudioMixer _audioMixer;
    [SerializeField] private AudioSourceTypes _audioSourceType;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void Start()
    {
        _slider.value = 0.5f;
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(float volume)
    {
        float correctedVolume = GetCorrectVolume(volume);
        string nameOfSource = _audioSourceType.ToString();
        _audioMixer.SetFloatBySliderValue(nameOfSource, correctedVolume);
    }

    private float GetCorrectVolume(float volume)
    {
        return Mathf.Log10(volume) * Multiplier;
    }
}