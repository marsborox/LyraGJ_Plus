using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class MySoundManager : SingletonPersistent<MySoundManager>
{
    public static new MySoundManager instance => SingletonPersistent<MySoundManager>.instance;
    [Header("GeneralVariables")]
    [Range(0f, 1f)] public float musicVolume = 0.2f;
    [Range(0f, 1f)] public float soundEffectVolume = 1f;

    [Header("General Sounds")]
    public AudioClip buttonClickSound;

    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip lobbyMusic;
    public AudioClip gameMusic;
    public AudioClip postGameMusic;
    public AudioClip creditsMusic;

    public AudioSource currentMusic;

    public List<AudioClip> pianoSounds;
    public List<AudioClip> saxofonSounds;
    public List<AudioClip> guitarSounds;

    private void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        //PLayMusicClipOverScenes(creditsMusic,musicVolume);
    }
    private void Update()
    {
        PlaySceneMusic();

    }
    public void ButtonClick()
    {
        PlayClip(buttonClickSound, soundEffectVolume);

    }
    public void PlayMenuMusic()
    {
        PlayMusicClip(menuMusic, musicVolume);
        //Debug.Log("playing music");
    }
    public void PlayLobbyMusic()
    {
        PlayMusicClip(lobbyMusic, musicVolume);
        //Debug.Log("playing music");
    }
    public void PlayGameMusic()
    {
        PlayMusicClip(gameMusic, musicVolume);
        //Debug.Log("playing music");
    }
    public void PlayPostGameMusic()
    {
        PlayMusicClip(postGameMusic, musicVolume);
        //Debug.Log("playing music");
    }
    public void PlayCreditsMusic()
    {
        PlayMusicClip(creditsMusic, musicVolume);
        //Debug.Log("playing music");
    }
    public void StopCurrentMusic()
    {
        Destroy(currentMusic.gameObject);
    }
    public void ChangeSoundEffectsVolume([UnityEngine.Internal.DefaultValue("1.0F")] float volume)
    {
        if (soundEffectVolume != volume)
        {
            soundEffectVolume = volume;
            //Debug.Log("soundEffect Volume = " + volume);
        }
    }
    public void ChangeMusicVolume([UnityEngine.Internal.DefaultValue("1.0F")] float volume)
    {
        if (musicVolume != volume)
        {
            musicVolume = volume;
            currentMusic.volume = volume;
            //Debug.Log("music Volume = "+volume);
        }
    }
    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
    void PlayMusicClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            //AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
            currentMusic = PlayAndReturnClipAtPoint(clip, cameraPos, volume);
        }
    }

    void PLayMusicClipOverScenes(AudioClip clip, float volume)
    {
        if (clip == null)
        {
            return;
        }
        /*
        MyPersistentAudioSource myAudioSource = Instantiate(myPersistentAudioSource);
        myAudioSource.gameObject.name= ("One shot audio persistent");
        myAudioSource.transform.position = Camera.main.transform.position;
        AudioSource audioSource = (AudioSource)myAudioSource.gameObject.AddComponent(typeof(AudioSource));
        */
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = Camera.main.transform.position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        gameObject.AddComponent(typeof(Persistent));

        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;
        audioSource.volume = volume;
        audioSource.Play();
        Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));

        if (currentMusic != null)
        {
            currentMusic.Stop();
        }
        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;
        audioSource.volume = volume;
        audioSource.Play();
        currentMusic = audioSource;
        Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
        currentMusic = audioSource;
    }

    void PlaySceneMusic()
    {
        if (currentMusic != null)
            return;
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "MainMenu":
                PlayMenuMusic();
                break;
            case "LobbyScene":
                PlayLobbyMusic();
                break;
            case "GameScene":
                PlayGameMusic();
                break;
            case "PostGame":
                PlayPostGameMusic();
                break;
            default:
                Debug.Log("Scene must be added");
                break;
        }
    }

    public void PlayClipAtPoint(AudioClip clip, Vector3 position, [UnityEngine.Internal.DefaultValue("1.0F")] float volume)
    {
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;
        audioSource.volume = volume;
        audioSource.Play();
        Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
    }
    public AudioSource PlayAndReturnClipAtPoint(AudioClip clip, Vector3 position, [UnityEngine.Internal.DefaultValue("1.0F")] float volume)
    {
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;
        audioSource.volume = volume;
        audioSource.Play();
        Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
        return audioSource;
    }

    public void PlayGuitar()
    {
        int index = Random.Range(0, guitarSounds.Count);
        PlayClip(guitarSounds[index], soundEffectVolume);
    }

    public void PlaySaxofone()
    {
        int index = Random.Range(0, saxofonSounds.Count);
        PlayClip(saxofonSounds[index], soundEffectVolume);
    }

    public void PlayPiano()
    {
        int index = Random.Range(0, pianoSounds.Count);
        PlayClip(pianoSounds[index], soundEffectVolume);
    }

}
