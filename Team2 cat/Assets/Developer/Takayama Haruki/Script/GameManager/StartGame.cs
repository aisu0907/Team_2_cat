using UnityEngine;
using Const;
using System.Collections;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject fade_obj;  //フェード用オブジェクト
    [SerializeField] GameObject start_text;//スタートコール用オブジェクト
    [SerializeField] GameObject[] set_object; //スタート時にセットするオブジェクト
   
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
    public bool game_start; //ゲームのスタート管理用フラグ
    public bool text_start; //テキストのスタート管理用フラグ

    private FadeManager fade_in_obj; //FadeManager取得用
    private SoundManager sound;  //サウンドインスタン省略用
    private GameObject save_obj; //オブジェクト一時的保存用

    public static StartGame Instance; //シングルトン
    void Awake()
    {
        Instance = this; //シングルトン

        game_start = false;
        text_start = false;

        fade_in_obj = fade_obj.GetComponent<FadeManager>();
    }
    
    void Start()
    {
        //オブジェクトを非表示
        for (int i = 0; i < set_object.Length; i++)
            set_object[i].SetActive(false);

        fade_obj.SetActive(true); //フェードオブジェクトを表示

        sound = SoundManager.Instance; //省略

        sound.PlayBGM((int)SoundConst.BGM_ID.GAME, game_bgm_volome); //BGMを設定

        fade_in_obj.StartFadeIn(); //フェードインコルーチン

        StartCoroutine(StartMovie()); //スタートムービーコルーチン
    }

    /// <summary>
    /// ゲームスタート時の演出用コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator StartMovie()
    {
        yield return new WaitForSeconds(start_time); //指定した時間まで待機

        sound.PlaySE((int)SoundConst.SE_ID.DOOR, door_volume); //ドアの音を鳴らす

        yield return new WaitForSeconds(bell_time); //指定した時間まで待機

        sound.PlaySE((int)SoundConst.SE_ID.BELL, bell_volome); //入店音を鳴らす

        yield return new WaitForSeconds(castomer_time); //指定した時間まで待機

        set_object[0].SetActive(true); //お客さんを登場

        text_start = true; //スタートテキストを起動
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
