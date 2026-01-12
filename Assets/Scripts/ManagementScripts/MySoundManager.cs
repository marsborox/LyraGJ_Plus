using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MySoundManager : SingletonPersistent<MySoundManager>
{
    public enum Instrument
    {
        Guitar,
        Piano,
        Saxophone
    }

    public static new MySoundManager instance => SingletonPersistent<MySoundManager>.instance;

    [Header("Volume")]
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float soundEffectsVolume = 1f;

    [Header("Instruments Fade Settings")]
    public float fadeInTime = 0.05f;
    public float fadeOutTime = 1f;
    public float instrumentPlayDuration = 0.6f;

    [Header("Sounds")]
    [SerializeField] private AudioClip buttonClickClip;
    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField] private AudioClip[] enemyHitClips;

    [Header("Music")]
    [SerializeField] private AudioClip jazzMusic;
    [SerializeField] private AudioClip lobbyMusic;


    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioSource guitarSource;
    [SerializeField] private AudioSource pianoSource;
    [SerializeField] private AudioSource saxophoneSource;
    [SerializeField] private AudioSource footstepsSource;
    
    private int _lastFootstepsIndex = -1;
    private int _lastEnemyHitsIndex = -1;

    private double _startDspTime;
    private Coroutine _guitarRoutine;
    private Coroutine _pianoRoutine;
    private Coroutine _saxophoneRoutine;

    // General settings

    public void ChangeSoundEffectsVolume([UnityEngine.Internal.DefaultValue("1.0F")] float volume)
    {
        if (soundEffectsVolume != volume)
        {
            soundEffectsVolume = volume;
        }
    }
    public void ChangeMusicVolume([UnityEngine.Internal.DefaultValue("1.0F")] float volume)
    {
        if (musicVolume != volume)
        {
            musicVolume = volume;

            if (backgroundSource != null) backgroundSource.volume = volume;
        }
    }

    // Sound clips

    public void ButtonClick()
    {
        if (buttonClickClip != null) 
        {
            PlayClip(buttonClickClip, soundEffectsVolume);
        }
    }
    public void PlayFootsteps()
    {
        if (footstepClips.Length == 0 || footstepsSource.isPlaying) return;

        int index = Random.Range(0, footstepClips.Length);

        if (index == _lastFootstepsIndex)
        {
            index = (index + 1) % footstepClips.Length;
        }

        _lastFootstepsIndex = index;
        footstepsSource.pitch = Random.Range(0.95f, 1.05f);
        footstepsSource.PlayOneShot(footstepClips[index], soundEffectsVolume * 0.1f);
    }
    public void PlayEnemyHit()
    {
        if (enemyHitClips.Length == 0) return;

        int index = Random.Range(0, enemyHitClips.Length);

        if (index == _lastEnemyHitsIndex)
        {
            index = (index + 1) % enemyHitClips.Length;
        }

        _lastEnemyHitsIndex = index;
        PlayClip(enemyHitClips[index], soundEffectsVolume * 0.1f);
    }

    public void HandleInstrument(Instrument instrument)
    {
        FadeInstrument(instrument, musicVolume * 1.5f); // a bit more, so it's noticeable
    }

    // Music

    public void StopMusic()
    {
        if (backgroundSource == null) return;

        backgroundSource.Stop();
    }

    public void PlayLobbyMusic()
    {
        if (backgroundSource == null) return;

        if (lobbyMusic != null)
        {
            backgroundSource.clip = lobbyMusic;
        }

        backgroundSource.volume = musicVolume;
        backgroundSource.Play();
    }

    public void PlayJazzMusic()
    {
        if (backgroundSource == null) return;

        if (jazzMusic != null)
        {
            backgroundSource.clip = jazzMusic;
        }

        _startDspTime = AudioSettings.dspTime + 0.1f;

        if (backgroundSource != null)
        {
            backgroundSource.volume = musicVolume;
            backgroundSource.PlayScheduled(_startDspTime);
        }
        if (guitarSource != null)
        {
            guitarSource.volume = 0;
            guitarSource.PlayScheduled(_startDspTime);
        }
        if (pianoSource != null)
        {
            pianoSource.volume = 0;
            pianoSource.PlayScheduled(_startDspTime);
        }
        if (saxophoneSource != null)
        {
            saxophoneSource.volume = 0;
            saxophoneSource.PlayScheduled(_startDspTime);
        }
    }

    // Helpers

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }

    // Mixing background music & instruments

    private void FadeInstrument(Instrument instrument, float targetVolume)
    {
        if (instrument == Instrument.Guitar)
        {
            if (_guitarRoutine != null) StopCoroutine(_guitarRoutine);
            _guitarRoutine = StartCoroutine(FadeRoutine(guitarSource, targetVolume));
        }
        else if (instrument == Instrument.Piano)
        {
            if (_pianoRoutine != null) StopCoroutine(_pianoRoutine);
            _pianoRoutine = StartCoroutine(FadeRoutine(pianoSource, targetVolume));
        }
        else if (instrument == Instrument.Saxophone)
        {
            if (_saxophoneRoutine != null) StopCoroutine(_saxophoneRoutine);
            _saxophoneRoutine = StartCoroutine(FadeRoutine(saxophoneSource, targetVolume));
        }
    }

    IEnumerator FadeRoutine(AudioSource source, float targetVolume)
    {
        float startVolume = source.volume;

        // ATTACK — go to max quickly
        float attackDuration = Mathf.Lerp(
            fadeInTime * 0.2f, // faster if already loud
            fadeInTime,
            1f - startVolume / targetVolume
        );

        float t = 0f;
        while (t < attackDuration)
        {
            t += Time.unscaledDeltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, t / attackDuration);
            yield return null;
        }

        source.volume = targetVolume;

        // HOLD (stay at max volume)
        yield return new WaitForSeconds(instrumentPlayDuration);

        // RELEASE — fade out slowly
        t = 0f;
        while (t < fadeOutTime)
        {
            t += Time.unscaledDeltaTime;
            source.volume = Mathf.Lerp(targetVolume, 0f, t / fadeOutTime);
            yield return null;
        }

        source.volume = 0f;
    }
}
