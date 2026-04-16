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
    private FMOD.Studio.EventInstance _menuMusicInstance;
    private FMOD.Studio.EventInstance _jazzMusicInstance;
    private FMOD.Studio.EventInstance _lobbyMusicInstance;
    private FMOD.Studio.EventInstance _bossBadJazzInstance;
    private FMOD.Studio.EventInstance _bossJazzInstance;
    
    // other sounds
    
    private FMOD.Studio.EventInstance _elevatorInstance;
    private FMOD.Studio.EventInstance _lyraDeathInstance;

    private Instrument _lastPlayedInstrument;
    private float _musicVolumeModifier = 0.1f;
    private float _soundVolumeModifier = 0.1f;

    void Start()
    {
        _enemyHitInstance = FMODUnity.RuntimeManager.CreateInstance("event:/enemy_hit");
        _guitarHitInstance = FMODUnity.RuntimeManager.CreateInstance("event:/guitar_hit");
        _pianoHitInstance = FMODUnity.RuntimeManager.CreateInstance("event:/piano_hit");
        _saxophoneHitInstance = FMODUnity.RuntimeManager.CreateInstance("event:/sax_hit");

        _footstepsEnemyInstance = FMODUnity.RuntimeManager.CreateInstance("event:/footsteps_enemy");
        _footstepsLyraInstance = FMODUnity.RuntimeManager.CreateInstance("event:/footsteps_Lyra");

        _menuMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/menu_hudba");
        _jazzMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/main_hudba_jazz");
        _lobbyMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/divadlo_hudba");
        _bossBadJazzInstance = FMODUnity.RuntimeManager.CreateInstance("event:/boss_falosne_noty");
        _bossJazzInstance = FMODUnity.RuntimeManager.CreateInstance("event:/boss_hudba");

        _elevatorInstance = FMODUnity.RuntimeManager.CreateInstance("event:/elevator_sound");
        _lyraDeathInstance = FMODUnity.RuntimeManager.CreateInstance("event:/death_sound");
        ChangeMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1f), true);
        ChangeSoundEffectsVolume(PlayerPrefs.GetFloat("SoundEffectsVolume", 1f), true);
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
        _elevatorInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _elevatorInstance.release();
        _lyraDeathInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _lyraDeathInstance.release();

        _menuMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _menuMusicInstance.release();
        _jazzMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _jazzMusicInstance.release();
        _lobbyMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _lobbyMusicInstance.release();
        _bossBadJazzInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _bossBadJazzInstance.release();
        _bossJazzInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _bossJazzInstance.release();
    }

    // General settings

    public void ChangeSoundEffectsVolume([UnityEngine.Internal.DefaultValue("1.0F")] float volume, bool force = false)
    {
        if (soundEffectsVolume != volume || force)
        {
            soundEffectsVolume = volume;
            PlayerPrefs.SetFloat("SoundEffectsVolume", volume);

            float limitedVolume = volume * _soundVolumeModifier;
            Debug.Log("Real sound volume: " + limitedVolume);

            _enemyHitInstance.setVolume(limitedVolume);
            _guitarHitInstance.setVolume(limitedVolume);
            _pianoHitInstance.setVolume(limitedVolume);
            _saxophoneHitInstance.setVolume(limitedVolume);

            _footstepsEnemyInstance.setVolume(limitedVolume);
            _footstepsLyraInstance.setVolume(limitedVolume);

            _elevatorInstance.setVolume(limitedVolume);
            _lyraDeathInstance.setVolume(limitedVolume);
        }
    }
    public void ChangeMusicVolume([UnityEngine.Internal.DefaultValue("1.0F")] float volume, bool force = false)
    {
        if (musicVolume != volume || force)
        {
            musicVolume = volume ;
            PlayerPrefs.SetFloat("MusicVolume", volume);

            float limitedVolume = volume * _musicVolumeModifier;
            Debug.Log("Real music volume: " + limitedVolume);

            _menuMusicInstance.setVolume(limitedVolume );
            _jazzMusicInstance.setVolume(limitedVolume );
            _lobbyMusicInstance.setVolume(limitedVolume);
            _bossBadJazzInstance.setVolume(limitedVolume);
            _bossJazzInstance.setVolume(limitedVolume);
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

        FMOD.Studio.PLAYBACK_STATE state;
        _jazzMusicInstance.getPlaybackState(out state);
        switch (instrument)
        {
            case Instrument.Guitar: {
                if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING) {
                    _jazzMusicInstance.setParameterByName("Guitar_attack", 1);
                } 
                else
                {
                    PlayLyraHit();
                }
                break;
            }
            case Instrument.Piano: {
                if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING) {
                    _jazzMusicInstance.setParameterByName("Piano_attack", 1);
                } 
                else
                {
                    PlayLyraHit();
                }
                break;
            }
            case Instrument.Saxophone: {
                if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING) {
                    _jazzMusicInstance.setParameterByName("Saxophone_attack", 1);
                }
                else
                {
                    PlayLyraHit();
                }
                break;
            }
        }
    }

    // Other sounds

    public void PlayElevatorSound()
    {
        _elevatorInstance.start();
    }

    public void PlayLyraDeathSound()
    {
        _lyraDeathInstance.start();
    }

    // Music

    public void StopMusic()
    {
        FMOD.Studio.PLAYBACK_STATE state;

        _menuMusicInstance.getPlaybackState(out state);
        if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            _menuMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

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

        _bossBadJazzInstance.getPlaybackState(out state);
        if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            _bossBadJazzInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        _bossJazzInstance.getPlaybackState(out state);
        if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            _bossJazzInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    public void PlayMenuMusic()
    {
        FMOD.Studio.PLAYBACK_STATE state;
        _menuMusicInstance.getPlaybackState(out state);
        if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING) return;

        StopMusic();
        _menuMusicInstance.start();
    }

    public void PlayLobbyMusic()
    {
        FMOD.Studio.PLAYBACK_STATE state;
        _lobbyMusicInstance.getPlaybackState(out state);
        if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING) return;

        StopMusic();
        _lobbyMusicInstance.start();
    }

    public void PlayJazzMusic()
    {
        StopMusic();
        _jazzMusicInstance.start();
    }

    public void PlayBadJazzBossMusic()
    {
        StopMusic();
        _bossBadJazzInstance.start();
    }

    public void PlayFinalJazzBossSong()
    {
        StopMusic();
        _bossJazzInstance.setTimelinePosition(2000);
        _bossJazzInstance.start();
    }

    // Helpers

    private void StopAllInstrumentSounds()
    {
        _jazzMusicInstance.setParameterByName("Guitar_attack", 0);
        _jazzMusicInstance.setParameterByName("Piano_attack", 0);
        _jazzMusicInstance.setParameterByName("Saxophone_attack", 0);
    }
}
