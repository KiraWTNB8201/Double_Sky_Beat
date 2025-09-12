using UnityEngine;

public class Notes : MonoBehaviour {
    public double hitTime;              // このノーツが叩かれる時間（NoteEditorからもらう）
    public float leadTime = 2.0f;       // 出現から判定までの秒数
    public Vector3 spawnPos;            // 出現位置
    public Vector3 hitPos;              // 判定ラインの位置

    private Renderer rend;

    void Start() {
        rend = GetComponent<Renderer>();
        rend.enabled = false; // 最初は非表示
    }

    void Update() {
        if (!GameManager.instance.start)
            return;

        double songTime = MusicManager.instance.GetSongTime();
        double timeUntilHit = hitTime - songTime;

        // まだリードタイムより先 → 非表示のまま
        if (timeUntilHit > leadTime) {
            rend.enabled = false;
            return;
        }

        // 判定ラインを過ぎた → 破棄
        if (timeUntilHit < -0.2f) { // 0.2秒くらい余裕を持って消す
            Destroy(gameObject);
            return;
        }

        // 表示オン
        rend.enabled = true;

        // 位置をリードタイム基準で補間
        float t = Mathf.Clamp01((float)(1 - (timeUntilHit / leadTime)));
        transform.position = Vector3.Lerp(spawnPos, hitPos, t);
    }
}