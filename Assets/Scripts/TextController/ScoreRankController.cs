using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using static Const;

public class ScoreRankManager : MonoBehaviour {

    [SerializeField] TextMeshProUGUI rankText;

    // Start is called before the first frame update
    void Start() {
        if (GameManager.instance.score >= S_dobule_plus_rank) {
            rankText.text = "S++";
            return;
        } else if (GameManager.instance.score >= S_plus_rank) {
            rankText.text = "S+";
            return;
        }else if(GameManager.instance.score >= S_rank) {
            rankText.text = "S";
            return;
        } else if(GameManager.instance.score >= A_plus_rank) {
            rankText.text = "A+";
            return;
        } else if(GameManager.instance.score >= A_rank) {
            rankText.text = "A";
            return;
        } else if (GameManager.instance.score >= B_plus_rank) {
            rankText.text = "B+";
            return;
        } else if (GameManager.instance.score >= B_rank) {
            rankText.text = "B";
            return;
        } else if (GameManager.instance.score >= C_rank) {
            rankText.text = "C";
            rankText.color = new Color(110, 110, 110);
            return;
        } else {
            rankText.text = "D";
            rankText.color = new Color(55,55,55);
            return;
        }
    }
}
