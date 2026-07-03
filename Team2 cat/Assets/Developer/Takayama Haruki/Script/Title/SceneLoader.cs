using UnityEngine;

public class SceneLoader: MonoBehaviour
{
    [SerializeField] FadeManager fade_obj; //フェードアウトするオブジェクト
    public bool move_scene = true; //シーン移動管理用 

    /// <summary>
    /// シーン移動用メソッド
    /// </summary>
    /// <param name="scene_name"></param>
   public void SceneLoad(string scene_name)
    {
        Debug.Log("クリック検知");
        SoundManager.Instance.PlaySE((int)Const.SoundConst.SE_ID.SELECT, 1.0f); //SEを鳴らす
        fade_obj.StartFadeOut(move_scene, scene_name); //ゲームシーンに移動
    }
}
