using UnityEngine;

public class MeasureLine : MonoBehaviour {
    public int measureIndex; // この線が何小節目か
    public float bpm;
    public float scrollSpeed;
    public float startPosY;
    public float songStartTime;

    private float beatInterval;
    private float measureInterval;

    void Start() {
        //現在の曲が何番目か算出してその曲のbpmを取り出して代入

        beatInterval = 60f / bpm;
        measureInterval = beatInterval * 4f;
    }

    void Update() {
        float songTime = Time.time - songStartTime;
        float targetTime = measureIndex * measureInterval;

        // 小節線の位置 = 基準位置 - (残り時間 × スクロール速度)
        float y = startPosY - ((targetTime - songTime) * scrollSpeed);
        transform.localPosition = new Vector3(transform.localPosition.x, y, 0);
    }
}
