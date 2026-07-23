using Const;
using UnityEngine;
using System.Collections;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] RectTransform[] target_star; //目的位置スターオブジェクト
    [SerializeField] GameObject star;     //評価用スターオブジェクト
    [SerializeField] GameObject fade_obj; //フェード用オブジェクト
    [SerializeField] Transform stars;
    [SerializeField] TextMeshProUGUI result_timer_text; //クリア時間表示オブジェクト
    [SerializeField] TextMeshProUGUI result_score_text; //クリアスコア表示オブジェクト
    public float effect_time; //星が大きくなるまでの時間
    
    private FadeManager fade_in; //FadeManager取得用
    private SoundManager sound; //サウンドインスタンス省略用
    private GameObject star_save; //スターオブジェクト一時保存用
    private bool effect_start; //エフェクト管理用フラグ
    private float clear_time; //クリア時間
    private float clear_score;//クリア時の評価

    void Start()
    {
        effect_start = false;

        fade_in = fade_obj.GetComponent<FadeManager>(); 

        sound = SoundManager.Instance;

        clear_time = (int)(Timer.Instance.start_time - GameManager.result_time);
        clear_score = ScoreManager.result_score;

        fade_in.StartFadeIn();

        StartCoroutine(StartEvaluate());
    }

    void Update()
    {
        if (!effect_start)
        {
            effect_start = true;
        }
    }

    private void FixedUpdate()
    {
        int min = (int)clear_time / 60;//分を計算
        int sec = (int)clear_time % 60;//秒を計算

        //クリア時間を表示
        result_timer_text.text = "クリア時間 : " + min + ":" + sec.ToString("00");

        //スコア表示
        result_score_text.text = "評定 : " + clear_score;

    }

    /// リザルト評価のエフェクト管理用コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator StartEvaluate()
    {
        int star_count = (int)(clear_score / 0.5) / 2;

        Debug.Log("通ってる");
        for (int i = 0; i < clear_score; i++)
        {
            star_save = Instantiate(star, stars);
            star_save.GetComponent<RectTransform>().anchoredPosition = target_star[i].anchoredPosition;
            star_save.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);

            yield return StartCoroutine(StarSizeUp(star_save.GetComponent<RectTransform>().sizeDelta, target_star[i], effect_time, star_count));

            star_count--;

            yield return new WaitForSeconds(0.1f);
        }
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

            rect.sizeDelta = Vector2.Lerp(start_size, target_size, time / duraction);

            yield return null;
        }

        rect.sizeDelta = target_size;
    }
}


