using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager instance = null;
    [SerializeField] SongDataBase dataBase;
    private AudioSource audio;
    private AudioClip music;

    public string songName;
    private bool played;

    // dspTimeで管理するための基準
    private double songStartDspTime;

    void Start() {
        instance = this;
        GameManager.instance.start = false;

        songName = dataBase.songData[GameManager.instance.songID].songName;
        audio = GetComponent<AudioSource>();
        music = Resources.Load<AudioClip>("Musics/" + songName);

        played = false;
    }

    void Update() {
        // スタート操作があったら曲を予約して開始
        if (Input.anyKeyDown && !played) {
            GameManager.instance.start = true;

            // 現在のdspTimeを基準に曲の再生を予約
            double startTime = AudioSettings.dspTime + (GameManager.instance.settingData.songOffset / 1000.0);
            audio.clip = music;
            audio.volume = GameManager.instance.settingData.BGMVolume / 100f;
            audio.PlayScheduled(startTime);

            // dsp基準で開始時刻を保存
            songStartDspTime = startTime;
            played = true;
        }
    }

    // 曲の経過時間をdsp基準で返す
    public double GetSongTime() {
        return AudioSettings.dspTime - songStartDspTime;
    }
}
