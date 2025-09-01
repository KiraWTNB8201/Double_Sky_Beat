using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

using static Const;
using System.IO;

public class SongSelect : MonoBehaviour {

    [SerializeField] SongDataBase dataBase;
    [SerializeField] int songCount;
    [SerializeField] TextMeshProUGUI[] songNameText;
    [SerializeField] TextMeshProUGUI[] songLevelText;
    [SerializeField] public Image songImage;
    new AudioSource audio;
    AudioClip Music;
    string songName;

    public int select;

    [SerializeField]GameObject songDataObject;
    [SerializeField]GameObject songDataObjectRoot;
    private const int _SONG_NAME_INDEX = 0;
    private const int _SONG_LEVEL_INDEX = 1;

    void Start() {
        songCount = dataBase.songData.Length;
        for (int songCurrent = 0; songCurrent < songCount; songCurrent++) {
            GameObject song = Instantiate(songDataObject,songDataObjectRoot.transform);

            song.GetComponent<SongSelectButtonResponce>().songID = songCurrent;
            song.transform.GetChild(_SONG_NAME_INDEX).GetComponent<TextMeshProUGUI>().text =dataBase.songData[songCurrent].songName;
            song.transform.GetChild(_SONG_LEVEL_INDEX).GetComponent<TextMeshProUGUI>().text = "Lv." + dataBase.songData[songCurrent].songLevel;
        }

        select = 0;
        audio = GetComponent<AudioSource>();
        songName = dataBase.songData[select].songName;
        Music = (AudioClip)Resources.Load("Musics/" + songName);
        audio.PlayOneShot(Music);
        songImage.sprite = dataBase.songData[select].songImage;
    }

    // Update is called once per frame
    void Update() {       
        if(select != GameManager.instance.songID) {
            select = GameManager.instance.songID;
            songName = dataBase.songData[select].songName;
            Music = (AudioClip)Resources.Load("Musics/" + songName);
            audio.Stop();
            audio.PlayOneShot(Music);
            songImage.sprite = dataBase.songData[select].songImage;
        }
    }
    public void SongStart() {
        //ノーツ速度が不正値だったらデフォルト値に直す
        if(GameManager.instance.settingData.noteSpeed < 1 || GameManager.instance.settingData.noteSpeed > 99) {
            GameManager.instance.settingData.noteSpeed = _DEFAULT_NOTE_SPEED;
        }
            
        SceneManager.LoadScene("MainGame");
    }

    public void GotoSetting() {
        AudioManager.instance.SEPlay(0);

        SceneManager.LoadScene("Setting");
    }
}
