using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField]private BGMData BGMData;
    [SerializeField]private SEData SEData;

    public static AudioManager instance = null;

    private AudioSource bgmSource = null;
    private AudioSource seSource = null;

    void Awake(){
        // シングルトン初期化
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;

        //AudioSourceをBGM用とSE用に二つ追加する
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        seSource = gameObject.AddComponent<AudioSource>();

        
    }

    /// <summary>
    /// BGMを新しく再生する。
    /// </summary>
    /// <param name="bgmID"></param>
    public void BGMPlay(int bgmID) {
        bgmSource.clip = BGMData.bgm[bgmID];
        bgmSource.Play();
    }

    /// <summary>
    /// BGMを再生停止する。
    /// </summary>
    public void BGMStop() {
        bgmSource.Stop();
    }

    /// <summary>
    /// SEを新しく再生する。
    /// </summary>
    /// <param name="bgmID"></param>
    public void SEPlay(int bgmID) {
        seSource.PlayOneShot(BGMData.bgm[bgmID]);
    }

    /// <summary>
    /// BGMを再生停止する。
    /// </summary>
    public void SEStop() {
        seSource.Stop();
    }
}
