using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using static Const;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Image jacketImage;

    public float maxScore;
    public float ratioScore;

    public int songID;
    public SettingData settingData;

    public bool start;
    public float startTime;

    public int combo;
    public int maxCombo;
    public int score;
    public int showScore;

    public int perfect;
    public int great;
    public int bad;
    public int miss;

    SettingData save = new SettingData();
    string filePath;

    public void Awake() {
        filePath = Application.persistentDataPath + "/" + "savedata.json";
        Application.targetFrameRate = 90;

        showScore = 0;

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
        Imported();
    }

    public void Imported() {
        //セーブデータがあるか確認
        if (File.Exists(filePath)) {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            save = JsonUtility.FromJson<SettingData>(data);
        } else return;

        //設定を適用
        settingData.noteSpeed = save.noteSpeed;
        settingData.BackGroundBrightness = save.BackGroundBrightness;
        settingData.BGMVolume = save.BGMVolume;
        settingData.SEVolume = save.SEVolume;
        settingData.songOffset = save.songOffset;
        settingData.noteOffset = save.noteOffset;
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    public void ResetUp() {
        combo = 0;
        maxCombo = 0;
        score = 0;
        showScore= 0;
        perfect = 0;
        great = 0;
        bad = 0;
        miss = 0;
    }
}