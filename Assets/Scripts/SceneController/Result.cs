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

    int showScore = 0;

    private void Start() {
        AudioManager.instance.BGMPlay(2);
        AudioManager.instance.SEPlay(2);

        SongName.text = MusicManager.instance.songName;
        //scoreText.text = GameManager.instance.score.ToString();
        comboText.text = GameManager.instance.maxCombo.ToString();
        perfectText.text = GameManager.instance.perfect.ToString();
        greatText.text = GameManager.instance.great.ToString();
        badText.text = GameManager.instance.bad.ToString();
        missText.text = GameManager.instance.miss.ToString();

        SongJacket.sprite = dataBase.songData[GameManager.instance.songID].songImage;
    }

    private void Update() {
        if(showScore < GameManager.instance.score) {
            showScore += 3999;
            if(showScore > GameManager.instance.score) {
                showScore = GameManager.instance.score;
                AudioManager.instance.SEStop();
            }
        }

        scoreText.text = showScore.ToString();
    }

    public void Retry() {
        AudioManager.instance.BGMStop();
        AudioManager.instance.SEPlay(1);

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
        AudioManager.instance.BGMStop();
        AudioManager.instance.SEPlay(1);

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
