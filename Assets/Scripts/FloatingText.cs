using UnityEngine;

public class FloatingText : MonoBehaviour {
    public RectTransform rectTransform; // 対象のRectTransform
    public float amplitude = 10f;       // 揺れ幅
    public float frequency = 2f;        // 揺れスピード

    private float initialTop;

    void Start() {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();

        initialTop = rectTransform.offsetMax.y; // Topの初期値を保存
    }

    void Update() {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        Vector2 maxOffset = rectTransform.offsetMax;
        maxOffset.y = initialTop + offset;
        rectTransform.offsetMax = maxOffset;
    }
}
