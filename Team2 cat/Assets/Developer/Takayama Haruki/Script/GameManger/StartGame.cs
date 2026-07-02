using UnityEngine;
using Const;
using System.Collections;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject[] set_object; //スタート時にセットするオブジェクト

    public float start_time;
    public float bell_time;
    public float castomer_time;
    public bool game_start;
    
    private SoundManger sound;

    public static StartGame Instance; //シングルトン
    void Awake()
    {
        Instance = this; //シングルトン

        game_start = false;
    }
    
    void Start()
    {
        //オブジェクトを非表示
        for (int i = 0; i < set_object.Length; i++)
            set_object[i].SetActive(false);

        sound = SoundManger.Instance;

        sound.PlayBGM((int)SoundConst.BGM_ID.GAME, 50.0f);

        StartCoroutine(StartMovie());
    }
    IEnumerator StartMovie()
    {
        yield return new WaitForSeconds(start_time);

        //入店音を鳴らす
        sound.PlaySE((int)SoundConst.SE_ID.BELL, 50.0f);

        yield return new WaitForSeconds(bell_time);

        //ドアの音を鳴らす
        sound.PlaySE((int)SoundConst.SE_ID.DOOR, 50.0f);

        yield return new WaitForSeconds(castomer_time);

        //お客さんを登場
        set_object[2].SetActive(true);


        //その次にお客さんのテキスト

        //if()

        //ゲーム開始

    }
}
