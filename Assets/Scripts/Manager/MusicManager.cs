using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour{

    public static MusicManager instance = null;
    [SerializeField] SongDataBase dataBase;
    new AudioSource audio;
    AudioClip Music;
    public string songName;
    bool played;
    float time = 0;
    float autoTime = 0.0f;
    float autoStartTime = 1.0f;

    void Start(){
        instance = this;
        GameManager.instance.start = false;
        songName = dataBase.songData[GameManager.instance.songID].songName;   //読み込むファイル名
        audio = GetComponent<AudioSource>();    //オーディオファイルを入れる
        Music = (AudioClip)Resources.Load("Musics/" + songName);
        audio.clip = Music;
        played = false;
    }

    void Update() {
        //if (Input.anyKeyDown && !played) {
        //    GameManager.instance.start = true;
        //    GameManager.instance.startTime = Time.time;
        //}
        if(autoTime < autoStartTime){
            autoTime += Time.deltaTime;
        } else if(GameManager.instance.start == false) {
            GameManager.instance.start = true;
            GameManager.instance.startTime = Time.time;
        }

        if (GameManager.instance.start) {
            time += Time.deltaTime;
            if((time >= GameManager.instance.settingData.songOffset * 0.001f) && !played) {
                //audio.PlayOneShot(Music, GameManager.instance.settingData.BGMVolume / 100);
                audio.Play();
                played = true;
            }            
        }
    }
}
