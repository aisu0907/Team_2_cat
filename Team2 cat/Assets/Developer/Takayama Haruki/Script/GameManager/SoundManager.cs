using Const;
using UnityEngine;
using UnityEngine.Rendering;
using static Const.SoundConst;

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

    /// <summary>
    /// SEを鳴らす用メソッド
    /// </summary>
    /// <param name="se_id"></param>
    /// <param name="vlome"></param>
    public void PlaySE(int se_id, float vlome)
    {
        Debug.Log("SEを鳴らしました");
        se_sound.volume = vlome; //音量を設定
        se_sound.PlayOneShot(sound_data[SoundConst.SE].sound[se_id]); //対応したSEを流す

    }

    ///// <summary>
    ///// 持続的にSEを鳴らすようメソッド
    ///// </summary>
    ///// <param name="duration_se_id"></param>
    ///// <param name="vlome"></param>
    //public void DurationPlaySe(int duration_se_id, float vlome)
    //{
    //    Debug.Log("持続SEを鳴らしました");
    //    se_sound.volume = vlome; //音量を設定

    //    se_sound.clip = sound_data[SoundConst.SE].sound[duration_se_id];
        
    //    se_sound.Play(); //対応したSEを流す
    //}
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
