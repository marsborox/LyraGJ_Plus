using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

using FMODUnity;

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

    private FMOD.Studio.EventInstance _enemyHitInstance;
    private FMOD.Studio.EventInstance _guitarHitInstance;
    private FMOD.Studio.EventInstance _pianoHitInstance;
    private FMOD.Studio.EventInstance _saxophoneHitInstance;
    private FMOD.Studio.EventInstance _footstepsEnemyInstance;
    private FMOD.Studio.EventInstance _footstepsLyraInstance;
    private FMOD.Studio.EventInstance _jazzMusicInstance;
    private FMOD.Studio.EventInstance _lobbyMusicInstance;

    private Instrument _lastPlayedInstrument;

    void Start()
    {
        _enemyHitInstance = FMODUnity.RuntimeManager.CreateInstance("event:/enemy_hit");
        _guitarHitInstance = FMODUnity.RuntimeManager.CreateInstance("event:/guitar_hit");
        _pianoHitInstance = FMODUnity.RuntimeManager.CreateInstance("event:/piano_hit");
        _saxophoneHitInstance = FMODUnity.RuntimeManager.CreateInstance("event:/sax_hit");

        _footstepsEnemyInstance = FMODUnity.RuntimeManager.CreateInstance("event:/footsteps_enemy");
        _footstepsLyraInstance = FMODUnity.RuntimeManager.CreateInstance("event:/footsteps_Lyra");

        _jazzMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/main_hudba_jazz");
        _lobbyMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/divadlo_hudba");

        ChangeMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1f));
        ChangeSoundEffectsVolume(PlayerPrefs.GetFloat("SoundEffectsVolume", 1f));
    }

    void OnDestroy()
    {
        _enemyHitInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _enemyHitInstance.release();
        _guitarHitInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _guitarHitInstance.release();
        _pianoHitInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _pianoHitInstance.release();
        _saxophoneHitInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _saxophoneHitInstance.release();

        _footstepsEnemyInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _footstepsEnemyInstance.release();
        _footstepsLyraInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _footstepsLyraInstance.release();

        _jazzMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _jazzMusicInstance.release();
        _lobbyMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _lobbyMusicInstance.release();
    }

    // General settings

    public void ChangeSoundEffectsVolume([UnityEngine.Internal.DefaultValue("1.0F")] float volume)
    {
        if (soundEffectsVolume != volume)
        {
            Debug.Log("Setting sounds to " + volume);

            soundEffectsVolume = volume;
            PlayerPrefs.SetFloat("SoundEffectsVolume", volume);

            _enemyHitInstance.setVolume(volume);
            _guitarHitInstance.setVolume(volume);
            _pianoHitInstance.setVolume(volume);
            _saxophoneHitInstance.setVolume(volume);

            _footstepsEnemyInstance.setVolume(volume);
            _footstepsLyraInstance.setVolume(volume);
        }
    }
    public void ChangeMusicVolume([UnityEngine.Internal.DefaultValue("1.0F")] float volume)
    {
        if (musicVolume != volume)
        {
            Debug.Log("Setting music to " + volume);

            musicVolume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);

            _jazzMusicInstance.setVolume(volume);
            _lobbyMusicInstance.setVolume(volume);
        }
    }

    // Sound clips

    public void ButtonClick()
    {
        // TODO
    }
    public void PlayEnemyFootsteps()
    {
        FMOD.Studio.PLAYBACK_STATE state;
        _footstepsEnemyInstance.getPlaybackState(out state);
        
        if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            _footstepsEnemyInstance.start();
        }
    }
    public void PlayLyraFootsteps()
    {
        FMOD.Studio.PLAYBACK_STATE state;
        _footstepsLyraInstance.getPlaybackState(out state);
        
        if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            _footstepsLyraInstance.start();
        }
    }
    public void PlayEnemyHit()
    {
        _enemyHitInstance.start();
    }
    public void PlayLyraHit()
    {
        switch (_lastPlayedInstrument)
        {
            case Instrument.Guitar: {
                _guitarHitInstance.start();
                break;
            }
            case Instrument.Piano: {
                _pianoHitInstance.start();
                break;
            }
            case Instrument.Saxophone: {
                _saxophoneHitInstance.start();
                break;
            }
        }
    }
    public void PlayInstrument(Instrument instrument)
    {
        _lastPlayedInstrument = instrument;

        StopAllInstrumentSounds();

        switch (instrument)
        {
            case Instrument.Guitar: {
                _jazzMusicInstance.setParameterByName("Guitar_attack", 1);
                break;
            }
            case Instrument.Piano: {
                _jazzMusicInstance.setParameterByName("Piano_attack", 1);
                break;
            }
            case Instrument.Saxophone: {
                _jazzMusicInstance.setParameterByName("Saxophone_attack", 1);
                break;
            }
        }
    }

    // Music

    public void StopMusic()
    {
        FMOD.Studio.PLAYBACK_STATE state;

        _jazzMusicInstance.getPlaybackState(out state);
        if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            _jazzMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        _lobbyMusicInstance.getPlaybackState(out state);
        if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            _lobbyMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    public void PlayLobbyMusic()
    {
        StopMusic();
        _lobbyMusicInstance.start();
    }

    public void PlayJazzMusic()
    {
        StopMusic();
        _jazzMusicInstance.start();
    }

    // Helpers

    private void StopAllInstrumentSounds()
    {
        _jazzMusicInstance.setParameterByName("Guitar_attack", 0);
        _jazzMusicInstance.setParameterByName("Piano_attack", 0);
        _jazzMusicInstance.setParameterByName("Saxophone_attack", 0);
    }
}
