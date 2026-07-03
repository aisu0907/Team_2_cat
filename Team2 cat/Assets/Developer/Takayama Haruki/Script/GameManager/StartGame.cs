using UnityEngine;
using Const;
using System.Collections;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject fade_obj;
    [SerializeField] GameObject start_text;
    [SerializeField] GameObject[] set_object; //スタート時にセットするオブジェクト
    [SerializeField] FadeManager fade_in_obj; 

    //演出管理用タイマー
    [Header("演出管理用タイマー")]
    public float start_time; //演出開始タイム
    public float bell_time;  //入店タイム
    public float castomer_time; //客登場タイム

    [Header("サウンド音量")]
    public float game_bgm_volome; //ゲームBGMの音量
    public float door_volume; //ドアSEの音量
    public float bell_volome; //ベルSEの音量

    [Header("ゲーム開始フラグ")]
    public bool game_start;
    public bool text_start;
    
    private SoundManager sound; //サウンドインスタン省略用
    private GameObject save_obj;

    public static StartGame Instance; //シングルトン
    void Awake()
    {
        Instance = this; //シングルトン

        game_start = false;
        text_start = false;
    }
    
    void Start()
    {
        //オブジェクトを非表示
        for (int i = 0; i < set_object.Length; i++)
            set_object[i].SetActive(false);

        fade_obj.SetActive(true);

        sound = SoundManager.Instance;

        sound.PlayBGM((int)SoundConst.BGM_ID.GAME, game_bgm_volome);

        fade_in_obj.StartFadeIn();

        StartCoroutine(StartMovie());
    }

    IEnumerator StartMovie()
    {
        yield return new WaitForSeconds(start_time); //指定した時間まで待機

        //ドアの音を鳴らす
        sound.PlaySE((int)SoundConst.SE_ID.DOOR, door_volume);

        yield return new WaitForSeconds(bell_time); //指定した時間まで待機

        //入店音を鳴らす
        sound.PlaySE((int)SoundConst.SE_ID.BELL, bell_volome);

        yield return new WaitForSeconds(castomer_time); //指定した時間まで待機

        //お客さんを登場
        set_object[0].SetActive(true);

        //スタートテキストを起動
        text_start = true;
    }

    /// <summary>
    /// ゲーム開始時にOFFにしたオブジェクトをActiveにする用メソッド
    /// </summary>
    public void ObjectActive()
    {
        //オブジェクトを非表示
        for (int i = 0; i < set_object.Length; i++)
            set_object[i].SetActive(true);
    }

    /// <summary>
    /// スタートコールを表示する用メソッド
    /// </summary>
    public void GameStart()
    {
        save_obj =  Instantiate(start_text, new Vector3(0, 0, 0), Quaternion.identity);

    }

    /// <summary>
    /// スタートコールを削除する用メソッド
    /// </summary>
    public void GameStartOff()
    {
        Destroy(save_obj);
    }
}
