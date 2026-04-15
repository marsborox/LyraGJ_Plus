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
    [Range(0f, 1f)] public float musicVolume = 0.5f;
    [Range(0f, 1f)] public float soundEffectsVolume = 1f;

    private FMOD.Studio.EventInstance _enemyHitInstance;
    private FMOD.Studio.EventInstance _guitarHitInstance;
    private FMOD.Studio.EventInstance _pianoHitInstance;
    private FMOD.Studio.EventInstance _saxophoneHitInstance;
    private FMOD.Studio.EventInstance _footstepsEnemyInstance;
    private FMOD.Studio.EventInstance _footstepsLyraInstance;
    private FMOD.Studio.EventInstance _jazzMusicInstance;
    private FMOD.Studio.EventInstance _lobbyMusicInstance;
    private FMOD.Studio.EventInstance _bossBadJazzInstance;
    private FMOD.Studio.EventInstance _bossJazzInstance;
    
    // other sounds
    
    private FMOD.Studio.EventInstance _elevatorInstance;
    private FMOD.Studio.EventInstance _lyraDeathInstance;

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
        _bossBadJazzInstance = FMODUnity.RuntimeManager.CreateInstance("event:/boss_falosne_noty");
        _bossJazzInstance = FMODUnity.RuntimeManager.CreateInstance("event:/boss_hudba");

        _elevatorInstance = FMODUnity.RuntimeManager.CreateInstance("event:/elevator_sound");
        _lyraDeathInstance = FMODUnity.RuntimeManager.CreateInstance("event:/death_sound");
        ChangeMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0.5f));
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
        _elevatorInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _elevatorInstance.release();
        _lyraDeathInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _lyraDeathInstance.release();

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

    public void ChangeSoundEffectsVolume([UnityEngine.Internal.DefaultValue("1.0F")] float volume)
    {
        if (soundEffectsVolume != volume)
        {
            soundEffectsVolume = volume;
            PlayerPrefs.SetFloat("SoundEffectsVolume", volume);

            _enemyHitInstance.setVolume(volume);
            _guitarHitInstance.setVolume(volume);
            _pianoHitInstance.setVolume(volume);
            _saxophoneHitInstance.setVolume(volume);

            _footstepsEnemyInstance.setVolume(volume);
            _footstepsLyraInstance.setVolume(volume);

            _elevatorInstance.setVolume(volume);
            _lyraDeathInstance.setVolume(volume);
        }
    }
    public void ChangeMusicVolume([UnityEngine.Internal.DefaultValue("1.0F")] float volume)
    {
        if (musicVolume != volume)
        {
            musicVolume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);

            _jazzMusicInstance.setVolume(volume);
            _lobbyMusicInstance.setVolume(volume);
            _bossBadJazzInstance.setVolume(volume);
            _bossJazzInstance.setVolume(volume);
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
