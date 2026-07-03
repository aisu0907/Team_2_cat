using UnityEngine;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        //BGMを鳴らす
        SoundManager.Instance.PlayBGM((int)Const.SoundConst.BGM_ID.TITLE, 0.3f);
    }
}
