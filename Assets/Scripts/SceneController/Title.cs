using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {
    [SerializeField] FadeManager fadeManager;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.anyKeyDown) {
            fadeManager.fadeOutStart(0, 0, 0, 0, "MusicSelect");
        }
    }
}
