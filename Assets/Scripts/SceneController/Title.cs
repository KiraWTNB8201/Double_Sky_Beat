using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class Title : MonoBehaviour {
    [SerializeField] FadeManager fadeManager;
    [SerializeField] AudioClip bgm;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgm;
        audioSource.loop = true;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update() {
        if (Input.anyKeyDown) {
            fadeManager.fadeOutStart(0, 0, 0, 0, "MusicSelect");
        }
    }
}
