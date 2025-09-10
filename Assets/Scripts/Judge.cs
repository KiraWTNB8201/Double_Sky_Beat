using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Judge : MonoBehaviour {
    [SerializeField] private GameObject[] MassageObj;
    [SerializeField] private NotesManager notesManager;
    [SerializeField] private GameObject[] EffectPrefabs;
    [SerializeField] private GameObject[] EffectPlayTransform;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject finish;

    [SerializeField] private AudioSource audioSource; // 曲のAudioSource

    private AudioSource seSource;
    [SerializeField] private AudioClip hitSound;

    private bool finishFlag = false;
    private float endTime = 0;

    void Start() {
        seSource = GetComponent<AudioSource>();
        if (notesManager.NotesTime.Count > 0)
            endTime = notesManager.NotesTime[notesManager.NotesTime.Count - 1];
    }

    void Update() {
        float musicTime = audioSource.time; // 曲の再生時間を基準にする

        if (GameManager.instance.start && !finishFlag) {
            // タッチ／マウス
            if (Input.touchCount > 0) {
                foreach (var touch in Input.touches) {
                    if (touch.phase == TouchPhase.Began) {
                        int lane = GetTouchedLane(touch.position);
                        if (lane >= 0)
                            TryJudgeLane(lane, musicTime);
                    }
                }
            } else if (Input.GetMouseButtonDown(0)) {
                int lane = GetTouchedLane(Input.mousePosition);
                if (lane >= 0)
                    TryJudgeLane(lane, musicTime);
            }

            // 楽曲終了チェック
            if (musicTime > endTime) {
                finishFlag = true;
                finish.SetActive(true);
                Invoke(nameof(ResultScene), 3.0f);
                return;
            }

            // Miss判定
            if (notesManager.NotesTime.Count > 0) {
                if (musicTime > notesManager.NotesTime[0] + 0.2f) {
                    Judge_Message(3);
                    Debug.Log("Miss");
                    GameManager.instance.miss++;
                    GameManager.instance.combo = 0;
                    deleteData(0);
                }
            }
        }

        // スコア演出
        if (GameManager.instance.showScore < GameManager.instance.score) {
            GameManager.instance.showScore += 149;
            if (GameManager.instance.showScore > GameManager.instance.score)
                GameManager.instance.showScore = GameManager.instance.score;
        }
        scoreText.text = GameManager.instance.showScore.ToString();
    }

    private int GetTouchedLane(Vector2 screenPos) {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            var lane = hit.collider.GetComponent<LaneInfo>();
            if (lane != null)
                return lane.laneIndex;
        }
        return -1;
    }

    private void TryJudgeLane(int lane, float musicTime) {
        for (int i = 0; i < notesManager.LaneNum.Count; i++) {
            if (notesManager.LaneNum[i] == lane) {
                float timeLag = Mathf.Abs(musicTime - notesManager.NotesTime[i]);
                Judgement(timeLag, i);
                break;
            }
        }
    }

    private void Judgement(float timeLag, int numOffset) {
        seSource.PlayOneShot(hitSound, GameManager.instance.settingData.SEVolume / 100f);

        if (timeLag <= 0.10f) {
            Debug.Log("Perfect");
            Judge_Message(0);
            GameManager.instance.ratioScore += 5;
            GameManager.instance.perfect++;
            GameManager.instance.combo++;
            deleteData(numOffset);
        } else if (timeLag <= 0.15f) {
            Debug.Log("Great");
            Judge_Message(1);
            GameManager.instance.ratioScore += 3;
            GameManager.instance.great++;
            GameManager.instance.combo++;
            deleteData(numOffset);
        } else if (timeLag <= 0.20f) {
            Debug.Log("Bad");
            Judge_Message(2);
            GameManager.instance.ratioScore += 1;
            GameManager.instance.bad++;
            GameManager.instance.combo = 0;
            deleteData(numOffset);
        }

        if (GameManager.instance.combo > GameManager.instance.maxCombo)
            GameManager.instance.maxCombo = GameManager.instance.combo;
    }

    private void deleteData(int numOffset) {
        notesManager.NotesTime.RemoveAt(numOffset);
        notesManager.LaneNum.RemoveAt(numOffset);
        notesManager.NoteType.RemoveAt(numOffset);
        GameManager.instance.score = (int)Mathf.Round(
            1000000 * Mathf.Floor(GameManager.instance.ratioScore / GameManager.instance.maxScore * 1000000) / 1000000
        );
        comboText.text = GameManager.instance.combo.ToString();
    }

    private void Judge_Message(int judge) {
        Instantiate(MassageObj[judge],
            new Vector3(notesManager.LaneNum[0] - 1.5f, 0.77f, 0.15f),
            Quaternion.identity);
        Instantiate(EffectPrefabs[judge],
            EffectPlayTransform[notesManager.LaneNum[0]].transform.position,
            Quaternion.identity);
    }

    private void ResultScene() {
        SceneManager.LoadScene("Result");
    }
}