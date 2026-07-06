using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [Header("サウンド設定")]
    public float bgm_vlome; //0.3f

    void Start()
    {
        //BGMを鳴らす
        SoundManager.Instance.PlayBGM((int)Const.SoundConst.BGM_ID.TITLE, bgm_vlome);
    }
}
