using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data {
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset; // ms想定
    public Note[] notes;
}

[Serializable]
public class Note {
    public int type;
    public int num;
    public int block;
    public int LPB;
}

public class NotesManager : MonoBehaviour {
    public int noteNum;
    public List<int> LaneNum = new List<int>();
    public List<int> NoteType = new List<int>();
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NotesObj = new List<GameObject>();

    [SerializeField] private float NotesSpeed; // 判定線に届くまでの移動速度
    [SerializeField] private GameObject noteObj;

    [SerializeField] private GameObject LeftUpLine;
    [SerializeField] private GameObject LeftDownLine;
    [SerializeField] private GameObject RightUpLine;
    [SerializeField] private GameObject RightDownLine;

    [SerializeField] private SongDataBase dataBase;

    void OnEnable() {
        NotesSpeed = GameManager.instance.settingData.noteSpeed;
        noteNum = 0;
        string songName = dataBase.songData[GameManager.instance.songID].songName;
        Load(songName);

        // 同時押し色変更
        for (int i = 0, max = NotesTime.Count - 1; i < max; i++) {
            if (Mathf.Approximately(NotesTime[i], NotesTime[i + 1])) {
                var obj = NotesObj[i].GetComponent<MeshRenderer>();
                var obj2 = NotesObj[i + 1].GetComponent<MeshRenderer>();
                obj.material.color = Color.yellow;
                obj2.material.color = Color.yellow;
            }
        }
    }

    private void Load(string songName) {
        string inputString = Resources.Load<TextAsset>(songName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        noteNum = inputJson.notes.Length;
        GameManager.instance.maxScore = noteNum * 5;

        for (int i = 0; i < noteNum; i++) {
            // BPM → 1拍の秒数
            float beatSec = 60f / inputJson.BPM;
            // 拍位置
            float noteBeatPosition = (float)inputJson.notes[i].num / inputJson.notes[i].LPB;
            // ノーツが判定線に来るべき時刻（AudioSource.time基準）
            float time = beatSec * noteBeatPosition + inputJson.offset * 0.001f;

            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);

            // 初期位置（z方向に落ちてくると仮定）
            float travelTime = time; // 判定線までの残り時間を移動距離に変換
            float z = travelTime * NotesSpeed;

            Vector3 spawnPos;
            Quaternion spawnRot;
            Transform parent;
            switch (inputJson.notes[i].block) {
                case 0:
                    spawnPos = LeftUpLine.transform.position + new Vector3(0.5f, 0.5f, z);
                    spawnRot = LeftUpLine.transform.rotation;
                    parent = LeftUpLine.transform;
                    break;
                case 1:
                    spawnPos = LeftDownLine.transform.position + new Vector3(0.5f, -0.5f, z);
                    spawnRot = LeftDownLine.transform.rotation;
                    parent = LeftDownLine.transform;
                    break;
                case 2:
                    spawnPos = RightUpLine.transform.position + new Vector3(-0.5f, -0.5f, z);
                    spawnRot = RightUpLine.transform.rotation;
                    parent = RightUpLine.transform;
                    break;
                case 3:
                    spawnPos = RightDownLine.transform.position + new Vector3(-0.5f, 0.5f, z);
                    spawnRot = RightDownLine.transform.rotation;
                    parent = RightDownLine.transform;
                    break;
                default:
                    spawnPos = Vector3.zero;
                    spawnRot = Quaternion.identity;
                    parent = null;
                    break;
            }
            NotesObj.Add(Instantiate(noteObj, spawnPos, spawnRot, parent));
        }
    }
}