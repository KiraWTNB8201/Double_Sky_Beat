using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Judge : MonoBehaviour {
    //変数
    [SerializeField] private GameObject[] MassageObj;   //プレイヤーに判定を伝えるオブジェクト
    [SerializeField] NotesManager notesManager;         //スクリプトを入れる

    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject finish;
    new AudioSource audio;
    [SerializeField] AudioClip hitSound;
    bool finishFlag;

    float endTime = 0;

    void Start () {
        audio = GetComponent<AudioSource>();
        endTime = notesManager.NotesTime[notesManager.NotesTime.Count - 1];
        finishFlag = false;
    }

    void Update() {
        if (GameManager.instance.start && !finishFlag) {
            //指定キーが押されたときキーとレーンが一致しているか確認、ノーツが処理される予定だった位置と処理された位置の差異(絶対値)を算出、関数に送る
            if (Input.GetKeyDown(KeyCode.F)) LeftUp();
            if (Input.GetKeyDown(KeyCode.V)) LeftDown();
            if (Input.GetKeyDown(KeyCode.N)) RightDown();
            if (Input.GetKeyDown(KeyCode.J)) RightUp();

            if (Time.time > endTime + GameManager.instance.startTime){
                finishFlag = true;
                finish.SetActive(true);
                Invoke("ResultScene", 3.0f);
                return;
            }

            if (notesManager.NotesTime.Count != 0) {
                if (Time.time > notesManager.NotesTime[0] + 0.2f + GameManager.instance.startTime) {  //判定可能時間内に入力を検知しなかった場合Miss判定
                    Judge_Message(3);
                    Debug.Log("Miss");
                    GameManager.instance.miss++;
                    GameManager.instance.combo = 0;
                    deleteData(0);
                }
            }
        }

        if (GameManager.instance.showScore < GameManager.instance.score) {
            GameManager.instance.showScore += 149;
            if(GameManager.instance.showScore > GameManager.instance.score) GameManager.instance.showScore = GameManager.instance.score;
        }
        scoreText.text = GameManager.instance.showScore.ToString();
    }

    public void LeftUp() {
        if (notesManager.LaneNum[0] == 0)
            Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GameManager.instance.startTime)), 0);
        else if (notesManager.LaneNum[1] == 0)
            Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GameManager.instance.startTime)), 1);
    }

    public void LeftDown() {
        if (notesManager.LaneNum[0] == 1)
            Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GameManager.instance.startTime)), 0);
        else if (notesManager.LaneNum[1] == 1)
            Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GameManager.instance.startTime)), 1);
    }

    public void RightDown() {
        if (notesManager.LaneNum[0] == 2)
            Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GameManager.instance.startTime)), 0);
        else if (notesManager.LaneNum[1] == 2)
            Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GameManager.instance.startTime)), 1);
    }

    public void RightUp() {
        if (notesManager.LaneNum[0] == 3)
            Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GameManager.instance.startTime)), 0);
        else if (notesManager.LaneNum[1] == 3)
            Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GameManager.instance.startTime)), 1);
    }

    void Judgement(float timeLag ,int numOffset) {
        audio.PlayOneShot(hitSound,GameManager.instance.settingData.SEVolume/100);

        if (timeLag <= 0.10) {          //誤差が100ms以下
            Debug.Log("Perfect");
            Judge_Message(0);
            GameManager.instance.ratioScore += 5;
            GameManager.instance.perfect++;
            GameManager.instance.combo++;
            deleteData(numOffset);
        } else if (timeLag <= 0.15) {   //誤差が150ms以下
            Debug.Log("Great");
            Judge_Message(1);
            GameManager.instance.ratioScore += 3;
            GameManager.instance.great++;
            GameManager.instance.combo++;
            deleteData(numOffset);
        } else if (timeLag <= 0.20) {   //誤差が200ms以下
            Debug.Log("Bad");
            Judge_Message(2);
            GameManager.instance.ratioScore += 1;
            GameManager.instance.bad++;
            GameManager.instance.combo = 0;
            deleteData(numOffset);
        }

        if (GameManager.instance.combo > GameManager.instance.maxCombo) {
            GameManager.instance.maxCombo = GameManager.instance.combo;
        }
    }

    /// <summary>
    /// 引数の絶対値を返す
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    float GetABS(float num) {
        if (num >= 0) return num;
        else return -num;
    }

    /// <summary>
    /// 判定が行われたノーツを削除する
    /// </summary>
    private void deleteData(int numOffset) {
        notesManager.NotesTime.RemoveAt(numOffset);
        notesManager.LaneNum.RemoveAt(numOffset);
        notesManager.NoteType.RemoveAt(numOffset);
        GameManager.instance.score = (int)Mathf.Round(1000000 * Mathf.Floor(GameManager.instance.ratioScore / GameManager.instance.maxScore * 1000000) / 1000000);
        comboText.text = GameManager.instance.combo.ToString();
        //scoreText.text = GameManager.instance.showScore.ToString();
    }

    /// <summary>
    /// 判定を表示
    /// </summary>
    /// <param name="judge"></param>
    void Judge_Message(int judge) {
        Instantiate(MassageObj[judge],new Vector3(notesManager.LaneNum[0]-1.5f,0.76f,0.15f),Quaternion.identity);
    }

    void ResultScene() {
        SceneManager.LoadScene("Result");
    }
}