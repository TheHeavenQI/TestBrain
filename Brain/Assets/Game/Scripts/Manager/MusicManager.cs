using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    public static MusicManager Instance;
    public MusicModel musicModel;
    private AudioSource loopAudio;
    private AudioSource onceAudio;
    
    private void Awake() {
        Instance = this;
        EventCenter.AddListener<bool>(UtilsEventType.BgSoundSwitch, BgSoundSwitch);
    }

    private void OnDestroy() {
        EventCenter.RemoveListener<bool>(UtilsEventType.BgSoundSwitch, BgSoundSwitch);
    }

    private void BgSoundSwitch(bool isOpen) {
        if (isOpen) {
            loopAudio.Play();
        }
        else {
            loopAudio.Stop();
        }
    }
    private void Start() {
        loopAudio = gameObject.AddComponent<AudioSource>();
        onceAudio = gameObject.AddComponent<AudioSource>();
        loopAudio.loop = true;
        onceAudio.loop = false;
        loopAudio.playOnAwake = false;
        onceAudio.playOnAwake = false;
    }

    public void PlayBgMusic() {
        if (UserModel.Get().bgSound) {
            loopAudio.clip = musicModel.bg;
            loopAudio.Play();
        }
    }

    public void PlaySuccessMusic() {
        if (UserModel.Get().sound) {
            onceAudio.clip = musicModel.success;
            onceAudio.Play();
        }
    }
    public void PlayErrorMusic() {
        if (UserModel.Get().sound) {
            onceAudio.clip = musicModel.error;
            onceAudio.Play();
        }
    }
    public void PlayChristmasMusic() {
        if (UserModel.Get().bgSound) {
            loopAudio.clip = musicModel.christmas;
            loopAudio.Play();
        }
    }

    public void StopTipMusic() {
        onceAudio.Stop();
    }
    
}
