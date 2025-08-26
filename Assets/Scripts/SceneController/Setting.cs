using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

using static Const;

public class Setting : MonoBehaviour {
    [SerializeField]Slider noteSpeed_Slid;
    [SerializeField]TextMeshProUGUI noteSpeed_text;
    [SerializeField]Slider BGBrightness_Slid;
    [SerializeField] TextMeshProUGUI BGBrightness_text;
    [SerializeField]Slider BGMVolume_Slid;
    [SerializeField] TextMeshProUGUI BGMVolume_text;
    [SerializeField]Slider SEVolume_Slid;
    [SerializeField] TextMeshProUGUI SEVolume_text;
    [SerializeField] Slider songOffset_Slid;
    [SerializeField] TextMeshProUGUI songOffset_text;
    [SerializeField] Slider noteOffset_Slid;
    [SerializeField] TextMeshProUGUI noteOffset_text;

    public SettingData setData = new SettingData();

    public string filePath;
    SettingData save = new SettingData();

    void Start() {
        filePath = Application.persistentDataPath + "/" + "savedata.json";
        
        //セーブデータがなかったら新規作成
        if (Load()) {
            //セーブデータをロード 
            setData.noteSpeed = save.noteSpeed;
            setData.BackGroundBrightness = save.BackGroundBrightness;
            setData.BGMVolume = save.BGMVolume;
            setData.SEVolume = save.SEVolume;
            setData.songOffset = save.songOffset;
            setData.noteOffset = save.noteOffset;
        } else {
            //新規作成
            setData.noteSpeed = _DEFAULT_NOTE_SPEED;
            setData.BackGroundBrightness = 100;
            setData.BGMVolume = 100;
            setData.SEVolume = 100;
            setData.songOffset = 100;
            setData.noteOffset = 100;
            Save();
        }
        //スライダーの値を同期
        noteSpeed_Slid.value = setData.noteSpeed;
        BGBrightness_Slid.value = setData.BackGroundBrightness;
        BGMVolume_Slid.value = setData.BGMVolume;
        SEVolume_Slid.value = setData.SEVolume;
        songOffset_Slid.value = setData.songOffset;
        noteOffset_Slid.value = setData.noteOffset;
    }

    void Update() {
        //値を表示に反映
        noteSpeed_text.text = setData.noteSpeed.ToString();
        BGBrightness_text.text = setData.BackGroundBrightness.ToString();
        BGMVolume_text.text = setData.BGMVolume.ToString();
        SEVolume_text.text = setData.SEVolume.ToString();
        songOffset_text.text = setData.songOffset.ToString();
        noteOffset_text.text = setData.noteOffset.ToString();
    }

    /// <summary>
    /// 設定を保存し選曲画面に戻る
    /// </summary>
    public void ExitSettings() {
        //設定を適用
        GameManager.instance.settingData.noteSpeed = setData.noteSpeed;
        GameManager.instance.settingData.BackGroundBrightness = setData.BackGroundBrightness;
        GameManager.instance.settingData.BGMVolume = setData.BGMVolume;
        GameManager.instance.settingData.SEVolume = setData.SEVolume;
        GameManager.instance.settingData.songOffset = setData.songOffset;
        GameManager.instance.settingData.noteOffset = setData.noteOffset;
        //セーブデータを同期
        save = setData;
        //同期したManager内のデータをセーブする
        Save();
        //選曲画面に戻る
        SceneManager.LoadScene("MusicSelect");
    }

    public void Save() {
        string json = JsonUtility.ToJson(save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();

    }

    public bool Load() {
        if (File.Exists(filePath)) {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            save = JsonUtility.FromJson<SettingData>(data);
            return true;
        }
        return false;
    }

    public void ChangeValue_NS() {
        Debug.Log("NoteSpeedが変更されました");
        setData.noteSpeed = (int)noteSpeed_Slid.value;
    }

    public void ChangeValue_BGB() {
        Debug.Log("BackGroundBrightnessが変更されました");
        setData.BackGroundBrightness = (int)BGBrightness_Slid.value;
    }

    public void ChangeValue_BGMV() {
        Debug.Log("BGMVolumeが変更されました");
        setData.BGMVolume = (int)BGMVolume_Slid.value;
    }

    public void ChangeValue_SEV() {
        Debug.Log("SEVolumeが変更されました");
        setData.SEVolume = (int)SEVolume_Slid.value;
    }

    public void ChangeValue_SO() {
        Debug.Log("songOffsetが変更されました");
        setData.songOffset = (int)songOffset_Slid.value;
    }

    public void ChangeValue_NO() {
        Debug.Log("noteOffsetが変更されました");
        setData.noteOffset = (int)noteOffset_Slid.value;
    }
}
