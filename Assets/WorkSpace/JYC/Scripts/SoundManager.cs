using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;



    
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { }
    // 각 효과음에 맞게 만들기

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        // 배경음 오브젝트 생성 및 초기화
        GameObject bgmObject = new GameObject("BGM Player");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        // 효과음 오브젝트 생성 및 초기화
        GameObject sfxObject = new GameObject("SFX Player");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            // sfxPlayers[index].bypassListenerEffects = true;
            sfxPlayers[index].volume = sfxVolume;
        }

    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
            bgmPlayer.Play();
        else
            bgmPlayer.Stop();
    }

    // 이펙트 BGM.. 추가?

    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (channelIndex + i) % sfxPlayers.Length;

            if(sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }

    }
}
