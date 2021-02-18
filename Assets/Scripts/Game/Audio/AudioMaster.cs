using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMaster : MonoBehaviour
{
    private AudioMixer audioMixer;
    public static AudioMaster instance { get; private set; }
    public AudioSource audioSource;
    public AudioClip[] loginAudioClips;
    public AudioClip[] gameAudioClips;

    // Use this for initialization
    void Start()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        var audioSourceObject = Instantiate(Resources.Load("Audio/Prefab/MusicController"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        audioSource = audioSourceObject.GetComponent<AudioSource>();
        audioMixer = audioSource.outputAudioMixerGroup.audioMixer;
        GetAllTracks();
        ChangeTrack(loginAudioClips[0]);
        UpdateMusicVolume(-25);
        UpdateEffectVolume(-25);
    }

    public void GetAllTracks()
    {
        loginAudioClips = Resources.LoadAll<AudioClip>("Audio/Music/Login");
        gameAudioClips = Resources.LoadAll<AudioClip>("Audio/Music/Game");
    }

    public void ChangeTrack(AudioClip track)
    {
        audioSource.Stop();
        audioSource.clip = track;
        audioSource.Play();
    }

    public void ChangeTrackRandomGame()
    {
        var rand = Random.Range(0, gameAudioClips.Length);
        audioSource.Stop();
        audioSource.clip = gameAudioClips[rand];
        audioSource.Play();
    }

    public void ChangeTrackLogin()
    {
        var rand = Random.Range(0, loginAudioClips.Length);
        audioSource.Stop();
        audioSource.clip = loginAudioClips[rand];
        audioSource.Play();
    }


    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void UpdateEffectVolume(float volume)
    {
        audioMixer.SetFloat("EffectVolume", volume);
    }
}
