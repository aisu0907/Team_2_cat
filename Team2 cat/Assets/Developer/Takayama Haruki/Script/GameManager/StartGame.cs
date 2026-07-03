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

        sound.PlayBGM((int)SoundConst.BGM_ID.GAME, 1.0f);

        fade_in_obj.StartFadeIn();

        StartCoroutine(StartMovie());
    }

    IEnumerator StartMovie()
    {
        yield return new WaitForSeconds(start_time);

        //ドアの音を鳴らす
        sound.PlaySE((int)SoundConst.SE_ID.DOOR, 0.5f);

        yield return new WaitForSeconds(bell_time);

        //入店音を鳴らす
        sound.PlaySE((int)SoundConst.SE_ID.BELL, 0.2f);

        yield return new WaitForSeconds(castomer_time);

        //お客さんを登場
        set_object[0].SetActive(true);

        text_start = true;

        //if()

        //ゲーム開始

    }

    public void ObjectActive()
    {
        //オブジェクトを非表示
        for (int i = 0; i < set_object.Length; i++)
            set_object[i].SetActive(true);
    }

    public void GameStart()
    {
        save_obj =  Instantiate(start_text, new Vector3(0, 0, 0), Quaternion.identity);

    }

    public void GameStartOff()
    {
        Destroy(save_obj);
    }
}
