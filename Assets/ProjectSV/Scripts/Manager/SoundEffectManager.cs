using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : SingletonBase<SoundEffectManager>
{
    [SerializeField] private GameObject audioSourcePrefab;
    [SerializeField] private int audioSourceCount;

    List<AudioSource> audioSources;

    // 오디오소스 한 곳에서 관리하기
    [SerializeField] private AudioClip chestOpenSound;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        audioSources = new List<AudioSource>();

        for(int i = 0; i < audioSourceCount; i++)
        {
            GameObject go = Instantiate(audioSourcePrefab, transform);
            go.transform.localPosition = Vector3.zero;
            audioSources.Add(go.GetComponent<AudioSource>());
        }
    }

    public void Play(AudioClip audioClip)
    {
        if (audioClip == null) return;

        AudioSource audioSource = GetFreeAudioSource();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private AudioSource GetFreeAudioSource()
    {
        for(int i = 0; i < audioSources.Count;i++)
        {
            if (!audioSources[i].isPlaying)
            {
                return audioSources[i];
            }
        }

        return audioSources[0];
    }
}
