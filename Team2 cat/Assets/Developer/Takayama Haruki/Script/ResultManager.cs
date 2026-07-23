using Const;
using UnityEngine;
using System.Collections;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [Header("オブジェクトデータ系")]
    [SerializeField] GameObject[] clear_data; //データオブジェクト
    [SerializeField] GameObject star;  //評価用スターオブジェクト
    [SerializeField] Transform  stars; //星を生成する場所
    [SerializeField] RectTransform[] target_star; //目的位置スターオブジェクト
    [SerializeField] FadeManager fade_obj; //フェード用オブジェクト
    [SerializeField] TextMeshProUGUI result_timer_text; //クリア時間表示オブジェクト
    [SerializeField] TextMeshProUGUI result_score_text; //クリアスコア表示オブジェクト

    [Header("タイミングタイム系")]
    public float clear_time_time; //タイムが出るまでの時間
    public float clear_score_time;//スコアが出までの時間
    public float customer_time; //客のコメントが出るまでの時間
    public float button_time;   //ボタンが出るまでの時間
    public float effect_time; //星が大きくなるまでの時間

    [Header("サウンドボリューム")]
    public float star_up_vlome; //星エフェクトSE音量
    public float result_data_vlome; //データ出現SE音量

    public bool debug_flag;
    public float clear_score { get; private set; }//クリア時の評価 

    private SoundManager sound;  //サウンドインスタンス省略用
    private GameObject star_save; //スターオブジェクト一時保存用
    private float clear_time; //クリア時間

    public static ResultManager Instance; //シングルトン

    void Start()
    {
        Instance = this; //シングルトン

        sound = SoundManager.Instance; //省略用

        if (!debug_flag)
        {
            clear_time = (int)(Timer.Instance.start_time - GameManager.result_time);
            clear_score = ScoreManager.result_score;
        }
        else
        {
            clear_time = 3;
            clear_score = 3;
        }

        //データを隠す
        for (int i = 0; i < clear_data.Length; i++)
        {
            clear_data[i].SetActive(false);
        }

        fade_obj.StartFadeIn();

        StartCoroutine(StartEvaluate());
    }

    private void FixedUpdate()
    {
        int min = (int)clear_time / 60;//分を計算
        int sec = (int)clear_time % 60;//秒を計算

        //クリア時間を表示
        result_timer_text.text = "クリア時間 : " + min + ":" + sec.ToString("00");

        //スコア表示
        result_score_text.text = "レビュー評価 : " + clear_score;

    }

    /// リザルト評価のエフェクト管理用コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator StartEvaluate()
    {
        yield return new WaitForSeconds(clear_time_time); //待つ
        
        if (!GameManager.is_game_over)
        {
            clear_data[ResultData.TIME].SetActive(true); //クリアタイムを表示

            sound.PlaySE((int)SoundConst.SE_ID.RESULT_DATA_POP, result_data_vlome); //音を鳴らす

            yield return new WaitForSeconds(clear_score_time); //待つ
        }

        int star_count = (int)(clear_score / 0.5) / 2; //星の数を取得

        //スコア系表示
        clear_data[ResultData.SCORE].SetActive(true);
        clear_data[ResultData.STAR].SetActive(true);

        sound.PlaySE((int)SoundConst.SE_ID.RESULT_DATA_POP, result_data_vlome); //音を鳴らす

        for (int i = 0; i < clear_score; i++)
        {
            star_save = Instantiate(star, stars); //星を生成
            star_save.GetComponent<RectTransform>().anchoredPosition = target_star[i].anchoredPosition; //星の位置を変更
            star_save.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1); //星のサイズを変更

            //星のエフェクトコルーチン
            yield return StartCoroutine(StarSizeUp(star_save.GetComponent<RectTransform>().sizeDelta, target_star[i], effect_time, star_count));

            star_count--; //星の数を減らす

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(customer_time);

        clear_data[ResultData.CSTOMER].SetActive(true); //レビュー表示
        clear_data[ResultData.BUTTON].SetActive(true);  //ボタン表示

        sound.PlaySE((int)SoundConst.SE_ID.RESULT_DATA_POP, result_data_vlome); //音を鳴らす

    }

    /// <summary>
    /// 星の大きさ変更用コルーチン
    /// </summary>
    /// <param name="start_size">変更前の大きさ</param>
    /// <param name="target">参照するターゲットの</param>
    /// <param name="duraction">変更にかかる時間</param>
    /// <param name="evaluate">星の数</param>
    /// <returns></returns>
    IEnumerator StarSizeUp(Vector2 start_size, RectTransform target, float duraction, int evaluate)
    {
        RectTransform rect = star_save.GetComponent<RectTransform>();
        Vector2 target_size;
        float time = 0;

        //クリアスコアに応じて星の大きさを変える
        if (0 < evaluate)
            target_size = target.sizeDelta;
        else
            target_size = (target.sizeDelta / 2);

        //演出が終了するまで
        while (time < duraction)
        {            
            time += Time.deltaTime;

            //大きさを徐々に変更
            rect.sizeDelta = Vector2.Lerp(start_size, target_size, time / duraction);

            yield return null;
        }

        rect.sizeDelta = target_size; //目標の大きさにする

        //音を鳴らす
        sound.PlaySE((int)SoundConst.SE_ID.STAR_UP, star_up_vlome);

    }
}


