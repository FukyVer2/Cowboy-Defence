using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoSingleton<AudioManager> {

    public AudioSource audioSource;
    public List<AudioConfig> audioResources;
    private Dictionary<BaseAudioType, AudioClip> dicAudioReSources;
    private bool isPause;
    void Awake()
    {
        dicAudioReSources = new Dictionary<BaseAudioType, AudioClip>();
        ConvertFromListToDictionary();
    }

    private void ConvertFromListToDictionary()
    {
        if (audioResources != null)
        {
            foreach (var audioConfig in audioResources)
            {
                dicAudioReSources.Add(audioConfig.audioType, audioConfig.audioSound);
            }
        }
    }

    public AudioClip GetSoundByType(BaseAudioType _audioType)
    {
        if(dicAudioReSources.ContainsKey(_audioType))
        {
            return dicAudioReSources[_audioType];
        }
        return null;
    }

    public void Play(BaseAudioType _audioType, bool loop)
    {
        AudioClip audio = GetSoundByType(_audioType);
        if (audio != null)
        {
            audioSource.loop = loop;
            audioSource.clip = audio;
            //audioSource.volume = 0.3f;
            audioSource.Play();
        }
    }

    public void Stop(BaseAudioType _audioType)
    {
        AudioClip audio = GetSoundByType(_audioType);
        if(audio!= null)
        {
            audioSource.clip = audio;
            audioSource.Stop();
        }
    }
    public void PlayOneShot(BaseAudioType _audioType)
    {
        AudioClip audio = GetSoundByType(_audioType);
        if (audio != null)
        {
            audioSource.PlayOneShot(audio, 0.6f);
        }
    }

    public void Pause()
    {
        isPause = true;
        audioSource.Pause();
    }

    public void Resume()
    {
        if (isPause)
        {
            audioSource.Play();
        }
    }
}

[System.Serializable]
public class AudioConfig
{
    public BaseAudioType audioType;
    public AudioClip audioSound;
}
