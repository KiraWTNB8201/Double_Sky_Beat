using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "VideoData", menuName = "Game/Video Data")]
public class VideoData : ScriptableObject {
    [Header("再生する動画クリップ")]
    public VideoClip[] videoClip;

    [Header("ポスター画像（動画準備中に表示）")]
    public Texture[] posterImage;
}
