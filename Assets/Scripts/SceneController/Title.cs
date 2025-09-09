using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class Title : MonoBehaviour {
    [SerializeField] FadeManager fadeManager;

    // Start is called before the first frame update
    void Start() {


        AudioManager.instance.BGMPlay(0);
        BackgroundVideoManager.Instance.PlayBackgroundVideo(0);
    }

    // Update is called once per frame
    void Update() {
        if (Input.anyKeyDown) {
            AudioManager.instance.BGMStop();

            AudioManager.instance.SEPlay(0);

            fadeManager.fadeOutStart(0, 0, 0, 0, "MusicSelect");
        }
    }
}
