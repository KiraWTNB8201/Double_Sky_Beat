using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BackgroundVideoManager : MonoBehaviour {
    public static BackgroundVideoManager Instance {
        get; private set;
    }
    [SerializeField] VideoData data;

    [Header("動画を再生する VideoPlayer")]
    [SerializeField] private VideoPlayer videoPlayer;

    [Header("ポスター画像を表示する RawImage")]
    [SerializeField] private RawImage posterRawImage;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        if (posterRawImage != null)
            posterRawImage.enabled = false;

        videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    /// <summary>
    /// VideoData を受け取って動画を再生する
    /// </summary>
    public void PlayBackgroundVideo(int ID) {
        if (data == null) {
            Debug.LogWarning("VideoData が指定されていません");
            return;
        }

        // ポスター画像を設定して表示
        if (posterRawImage != null && data.posterImage[ID] != null) {
            posterRawImage.texture = data.posterImage[ID];
            posterRawImage.enabled = true;
        }

        // VideoClip をセットして準備開始
        videoPlayer.Stop();
        videoPlayer.clip = data.videoClip[ID];
        videoPlayer.Prepare(); // 準備完了時に OnVideoPrepared が呼ばれる
    }

    private void OnVideoPrepared(VideoPlayer vp) {
        // 準備が完了したらポスター画像を消して動画再生
        if (posterRawImage != null)
            posterRawImage.enabled = false;

        vp.Play();
    }

    /// <summary>
    /// 背景動画を停止する
    /// </summary>
    public void StopBackgroundVideo() {
        videoPlayer.Stop();
        if (posterRawImage != null)
            posterRawImage.enabled = false;
    }
}