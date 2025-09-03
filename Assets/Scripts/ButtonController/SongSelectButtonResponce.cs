using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SongSelectButtonResponce : MonoBehaviour {
    public int songID = -1;

    public void OnSelect() {
        GameManager.instance.songID = songID;
        AudioManager.instance.SEPlay(1);
    }

}
