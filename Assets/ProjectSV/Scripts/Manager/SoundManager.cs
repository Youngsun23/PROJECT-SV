using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonBase<SoundManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipBefore;
    [SerializeField] private AudioClip audioClipAfter;

    private float volume = 1f;

    private void Start()
    {
        Play(audioClipBefore, false);  
    }

    //private void Update()
    //{
    //    if(Input.GetKeyUp(KeyCode.F1))
    //    {
    //        Play(audioClipBefore, true);
    //    }
    //    if (Input.GetKeyUp(KeyCode.F2))
    //    {
    //        Play(audioClipAfter, true);
    //    }
    //}

    public void Play(AudioClip audioToPlay, bool smoothSwitch = true)
    {
        if (audioToPlay == null)
            return;

        if(smoothSwitch == true)
        {
            audioClipAfter = audioToPlay;
            StartCoroutine(SwitchMusic());
        }
        else
        {
            audioSource.volume = 1f;
            audioSource.clip = audioToPlay;
            audioSource.Play();
        }

    }

    IEnumerator SwitchMusic()
    {
        volume = 1f;
        while(volume > 0f)
        {
            volume -= Time.deltaTime;
            if(volume < 0f) { volume = 0f; }
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        Play(audioClipAfter, false);
    }
}
