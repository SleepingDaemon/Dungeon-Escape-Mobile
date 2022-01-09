using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("AudioManager is null");
                _instance = (AudioManager)FindObjectOfType(typeof(AudioManager));
            }
            return _instance;
        }
    }

    [SerializeField] private AudioMixer _mixer = null;
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private AudioSource _playerSource = null;
    [SerializeField] private AudioSource _uiSource = null;
    [SerializeField] private AudioSource _musicSource = null;
    [SerializeField] private AudioSource _sceneSource = null;
    [SerializeField] private AudioClip _gemSound;
    [SerializeField] private AudioClip _dragonSound;

    public AudioMixer Mixer { get => _mixer; set => _mixer = value; }
    public AudioSource MusicSource { get => _musicSource; set => _musicSource = value; }

    private void Awake()
    {
        _instance = this;
    }

    public void PlaySound(AudioSource source, AudioClip clip)
    {
        if(clip != null)
        {
            source.PlayOneShot(clip);
        }
        else
            Debug.Log("Audio clip is null");
    }

    public void PlayGemSound() => _sceneSource.PlayOneShot(_gemSound);
    public void PlayDragonSound()
    {
        StartCoroutine(DelayDragonSoundRoutine());
    }

    IEnumerator DelayDragonSoundRoutine()
    {
        yield return new WaitForSeconds(3f);
        _sceneSource.PlayOneShot(_dragonSound);
    }
}
