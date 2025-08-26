using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreRankManager : MonoBehaviour {

    [SerializeField] TextMeshProUGUI rankText;

    private const int S_dobule_plus_rank = 990000;
    private const int S_plus_rank = 9600000;
    private const int S_rank = 920000;
    private const int A_plus_rank = 880000;
    private const int A_rank = 820000;
    private const int B_plus_rank = 760000;
    private const int B_rank = 700000;
    private const int C_rank = 600000;



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

    // Update is called once per frame
    void Update() {
        
    }
}
