using Const;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] RectTransform[] target_star; //目的位置スターオブジェクト
    [SerializeField] GameObject star;     //評価用スターオブジェクト
    [SerializeField] GameObject fade_obj; //フェード用オブジェクト
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
        fade_in = fade_obj.GetComponent<FadeManager>(); 

        fade_in.StartFadeIn();
        if (GameManager.result_time != 0)
            clear_time  = (int)(Timer.Instance.start_time - GameManager.result_time);
        clear_score = ScoreManager.result_score;

        sound = SoundManager.Instance;


    }

    void Update()
    {
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

    /// リザルト評価のエフェクト管理用メソッド
    /// </summary>
    /// <returns></returns>
    IEnumerator StartEvaluate()
    {
        for (int i = 0; i < clear_score; i++)
        {
            star_save = Instantiate(star, target_star[i].transform);
            star_save.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);

            yield return StartCoroutine(StarSizeUp(star_save.GetComponent<RectTransform>().sizeDelta, i, effect_time));

            yield return new WaitForSeconds(0.1f);
        }



    }

    IEnumerator StarSizeUp(Vector2 start_size, int i, float duraction)
    {
        RectTransform rect = star_save.GetComponent<RectTransform>();
        Vector2 target_size;
        float time = 0;

        //クリアスコアに応じて星の大きさを変える
        if (clear_score % 0.5f == 0)
            target_size = target_star[i].sizeDelta;
        else
            target_size = (target_star[i].sizeDelta / 2);

        //演出が終了するまで
        while (time < duraction)
        {
            time += Time.deltaTime;

            rect.sizeDelta = Vector2.Lerp(start_size, target_size, time / duraction);

            yield return null;
        }

        rect.sizeDelta = target_star[i].sizeDelta;
    }
}


