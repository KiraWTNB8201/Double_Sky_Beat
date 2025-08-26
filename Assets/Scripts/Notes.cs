using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour {

    float NoteSpeed;
    bool Enable;
    new Renderer renderer;

    void Start () {
        NoteSpeed = GameManager.instance.settingData.noteSpeed;
        renderer = gameObject.GetComponent<Renderer>();
        renderer.enabled = false;
    }

    void Update(){
        if (transform.localPosition.z <= 0.5f && Enable == false) {
            Enable = true;
            renderer.enabled = true;
        }

        if (GameManager.instance.start) {
            transform.position -= transform.forward * Time.deltaTime * NoteSpeed;
        }
    }
}
