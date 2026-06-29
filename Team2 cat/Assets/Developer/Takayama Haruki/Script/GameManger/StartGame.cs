using UnityEngine;
using Const;
using System.Collections;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject[] set_object; //スタート時にセットするオブジェクト

    public float start_time;
    public float bell_time;
    public float castomer_time;

    private bool game_start;
    private SoundManger sound;

    public static StartGame Instans;
    void Awake()
    {
        Instans = this;

        sound = SoundManger.Instans;

        game_start = false;
        for (int i = 0; i < set_object.Length; i++)
            set_object[i].SetActive(false);

        

    }
    
    void Start()
    {
        StartCoroutine(StartMovie());

    }
    IEnumerator StartMovie()
    {
        yield return new WaitForSeconds(start_time);

        //入店音を鳴らす
        sound.PlaySE((int)SoundConst.SE_ID.BELL, 50.0f);

        yield return new WaitForSeconds(bell_time);

        //ドアが開いた音を鳴らす
        sound.PlaySE((int)SoundConst.SE_ID.DOOR, 50.0f);

        yield return new WaitForSeconds(castomer_time);

        //ここでお客さんを登場
        set_object[3].SetActive(true);

        //その次にお客さんのテキスト]

        //if()

        //ゲーム開始

    }
}
