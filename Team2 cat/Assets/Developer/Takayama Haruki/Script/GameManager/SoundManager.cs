using Const;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundData[] sound_data; //サウンドデータ

    [SerializeField] AudioSource se_sound; //SEを鳴らす用
    [SerializeField] AudioSource bgm_sound;//BGMを鳴らす用

    //シングルトン
    public static SoundManager Instance;
    void Awake()
    {
        Instance = this;

        bgm_sound.loop = true; //BGMのループをON
    }

    public void PlaySE(int se_id, float vlome)
    {
        Debug.Log("SEを鳴らしました");
        se_sound.volume = vlome; //音量を設定
        se_sound.PlayOneShot(sound_data[SoundConst.SE].sound[se_id]); //対応したSEを流す

    }

    public void PlayBGM(int bgm_id, float vlome)
    {

        //BGMがなっていたら止める
        if (bgm_sound.clip != null)
            bgm_sound.Stop();
        
        bgm_sound.volume = vlome; //音量を設定

        bgm_sound.clip = sound_data[SoundConst.BGM].sound[bgm_id]; //BGM設定

        bgm_sound.Play(); //対応したBGMを流す
    }

    public void StopBGM()
    {
        bgm_sound.Stop();
    }
}
