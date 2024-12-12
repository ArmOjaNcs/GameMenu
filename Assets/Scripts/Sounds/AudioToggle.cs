using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private MainAudioMixer _audioMixer;

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(_audioMixer.SetFloatByToggle);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(_audioMixer.SetFloatByToggle);
    }
}