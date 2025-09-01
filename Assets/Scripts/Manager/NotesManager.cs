using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jsonファイル内のデータを受け取るための受け皿
[Serializable]
public class Data {
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;
}
[Serializable] 
public class Note{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

public class NotesManager : MonoBehaviour {
    public int noteNum;         //総ノーツ数
    private string songName;    //曲名

    public List<int> LaneNum = new List<int>();                 //何レーン目にノーツが落ちてくるか
    public List<int> NoteType = new List<int>();                //ノーツタイプ
    public List<float> NotesTime = new List<float>();           //ノーツの判定線到着時間
    public List<GameObject> NotesObj = new List<GameObject>();  //GameObject

    [SerializeField] private float NotesSpeed;  //ノーツ速度
    [SerializeField] GameObject noteObj;        //ノーツのプレハブを搭載
    [SerializeField] GameObject LeftUpLine;
    [SerializeField] GameObject LeftDownLine;
    [SerializeField] GameObject RightUpLine;
    [SerializeField] GameObject RightDownLine;

    [SerializeField] SongDataBase dataBase;

    void OnEnable() {
        NotesSpeed = GameManager.instance.settingData.noteSpeed;
        noteNum = 0;        //総ノーツ数を初期化
        songName = dataBase.songData[GameManager.instance.songID].songName;  //曲名の取得
        Load(songName);     //Load関数を呼び出す


        //同時ノーツを一覧から探し見つかれば色を変更する
        for (int i = 0, max = NotesTime.Count - 1; i < max; i++) {
            if (NotesTime[i] == NotesTime[i + 1]) {
                MeshRenderer obj = NotesObj[i].GetComponent<MeshRenderer>();
                MeshRenderer obj2 = NotesObj[i + 1].GetComponent<MeshRenderer>();
                obj.material.color = Color.yellow;
                obj2.material.color = Color.yellow;
            }
        }
    }

    private void Load(string songName) {
        string inputString = Resources.Load<TextAsset>(songName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);               //JSONファイルの読み込み

        noteNum = inputJson.notes.Length;   //総ノーツ数を設定
        GameManager.instance.maxScore = noteNum * 5;

        for (int i = 0; i < noteNum; i++) {
            float distance = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);                                      //
            float beatSec = distance * (float)inputJson.notes[i].LPB;                                                   //→ノーツの流れてくる時間を設定
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f; //
            NotesTime.Add(time);                        //
            LaneNum.Add(inputJson.notes[i].block);      //→ノーツの情報をリストに登録
            NoteType.Add(inputJson.notes[i].type);      //

            float z = NotesTime[i] * NotesSpeed;                                                                            //
            //NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, z),Quaternion.identity));    //→ノーツを生成

            switch (inputJson.notes[i].block) {
                case 0:
                    NotesObj.Add(Instantiate(noteObj, new Vector3(LeftUpLine.transform.position.x + 0.5f,LeftUpLine.transform.localPosition.y + 0.5f,z), LeftUpLine.transform.rotation, LeftUpLine.transform));
                    break;
                case 1:
                    NotesObj.Add(Instantiate(noteObj, new Vector3(LeftDownLine.transform.position.x + 0.5f, LeftDownLine.transform.localPosition.y - 0.5f, z), LeftDownLine.transform.rotation, LeftDownLine.transform));
                    break;
                case 2:
                    NotesObj.Add(Instantiate(noteObj, new Vector3(RightUpLine.transform.position.x - 0.5f, RightUpLine.transform.localPosition.y - 0.5f, z), RightUpLine.transform.rotation, RightUpLine.transform));
                    break;
                case 3: 
                    NotesObj.Add(Instantiate(noteObj, new Vector3(RightDownLine.transform.position.x - 0.5f, RightDownLine.transform.localPosition.y + 0.5f, z), RightDownLine.transform.rotation, RightDownLine.transform));
                    break;
                default:
                    break;
            }
        }
    }
}
