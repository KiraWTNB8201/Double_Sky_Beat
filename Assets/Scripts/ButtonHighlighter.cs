using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlighter : MonoBehaviour {
    public Transform parentObject;      // ボタンたちの親オブジェクト
    public Color normalColor = Color.white;
    public Color selectedColor = Color.green;

    private Button lastPressedButton;
    private Image lastPressedImage;

    void Start() {
        // 親オブジェクトの子 Button をすべて取得
        Button[] buttons = parentObject.GetComponentsInChildren<Button>();

        foreach (var btn in buttons) {
            // Button にアタッチされている Image コンポーネント
            Image img = btn.GetComponent<Image>();
            if (img != null) {
                img.color = normalColor; // 初期色
            }

            // ボタン押下時の処理を登録
            btn.onClick.AddListener(() => OnButtonPressed(btn));
        }
    }

    void OnButtonPressed(Button pressed) {
        // 前回押したボタンの色を戻す
        if (lastPressedButton != null && lastPressedImage != null) {
            lastPressedImage.color = normalColor;
        }

        // 今押したボタンの Image を取得して色を変更
        Image pressedImage = pressed.GetComponent<Image>();
        if (pressedImage != null) {
            pressedImage.color = selectedColor;
        }

        // 記録
        lastPressedButton = pressed;
        lastPressedImage = pressedImage;
    }
}
