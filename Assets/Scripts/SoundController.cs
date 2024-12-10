using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    private const string MasterVolume = nameof(MasterVolume);
    private const string ButtonsVolume = nameof(ButtonsVolume);
    private const string BackgroundVolume = nameof(BackgroundVolume);

    [SerializeField] private AudioMixerGroup _audioMasterGroup;

    public void ChangeTotalVolume(float volume)
    {
        _audioMasterGroup.audioMixer.SetFloat(MasterVolume, Mathf.Lerp(-80, 0, volume));
    }

    public void ChangeBackgroundVolume(float volume)
    {
        _audioMasterGroup.audioMixer.SetFloat(BackgroundVolume, Mathf.Lerp(-80, 0, volume));
    }

    public void ChangeButtonsVolume(float volume)
    {
        _audioMasterGroup.audioMixer.SetFloat(ButtonsVolume, Mathf.Lerp(-80, 0, volume));
    }

    public void SetTotalSound(bool isPlaying)
    {
        if (isPlaying)
            _audioMasterGroup.audioMixer.SetFloat(MasterVolume, 0);
        else
            _audioMasterGroup.audioMixer.SetFloat(MasterVolume, -80);
    }
}