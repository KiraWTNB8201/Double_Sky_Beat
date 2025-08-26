using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFadeController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] bool Inverted_Black;
    bool flag = false;
    int alfa;

    // Start is called before the first frame update
    void Start() {
        alfa = 255;
    }

    // Update is called once per frame
    void Update(){
        if (flag) {
            alfa += 2;
            if (alfa >= 255) {
                flag = false;
            }
        } else {
            alfa -= 2;
            if (alfa <= 0) {
                flag = true;
            }
        }
        if (Inverted_Black) {
            text.color = new Color(0.0f, 0.0f, 0.0f, (float)alfa / 128);
        } else {
            text.color = new Color(1.0f, 1.0f, 1.0f, (float)alfa / 128);
        }        
    }
}
