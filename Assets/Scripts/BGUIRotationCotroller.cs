using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGUIRotationCotroller : MonoBehaviour
{
    [SerializeField] Transform rotationRoot;
    [SerializeField] SongDataBase dataBase;
    [SerializeField] Image jacket;

    void Start() {
        jacket = GetComponent<Image>();
        jacket.sprite = dataBase.songData[GameManager.instance.songID].songImage;
        if (GameManager.instance.settingData.BackGroundBrightness == 0) {
            GameManager.instance.settingData.BackGroundBrightness = 100;
        }
        float Bright = (float)(GameManager.instance.settingData.BackGroundBrightness) * 2.55f;
        jacket.color = new Color(Bright, Bright, Bright);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rotationRoot.rotation;
    }
}
