 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour {
    [SerializeField] SongDataBase dataBase;

    [SerializeField] TextMeshProUGUI SongName;
    [SerializeField] Image SongJacket;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI perfectText;
    [SerializeField] TextMeshProUGUI greatText;
    [SerializeField] TextMeshProUGUI badText;
    [SerializeField] TextMeshProUGUI missText;

    private void OnEnable() {
        SongName.text = MusicManager.instance.songName;
        scoreText.text = GameManager.instance.score.ToString();
        comboText.text = GameManager.instance.maxCombo.ToString();
        perfectText.text = GameManager.instance.perfect.ToString();
        greatText.text = GameManager.instance.great.ToString();
        badText.text = GameManager.instance.bad.ToString();
        missText.text = GameManager.instance.miss.ToString();

        SongJacket.sprite = dataBase.songData[GameManager.instance.songID].songImage;
    }

    public void Retry() {
        GameManager.instance.perfect = 0;
        GameManager.instance.great = 0;
        GameManager.instance.bad = 0;
        GameManager.instance.miss = 0;
        GameManager.instance.score = 0;
        GameManager.instance.combo = 0;
        GameManager.instance.ratioScore = 0;
        GameManager.instance.maxCombo = 0;
        GameManager.instance.showScore = 0;
        SceneManager.LoadScene("MainGame");
    }

    public void Close() {
        GameManager.instance.perfect = 0;
        GameManager.instance.great = 0;
        GameManager.instance.bad = 0;
        GameManager.instance.miss = 0;
        GameManager.instance.score = 0;
        GameManager.instance.combo = 0;
        GameManager.instance.ratioScore = 0;
        GameManager.instance.maxCombo = 0;
        GameManager.instance.showScore = 0;
        SceneManager.LoadScene("MusicSelect");
    }
}
