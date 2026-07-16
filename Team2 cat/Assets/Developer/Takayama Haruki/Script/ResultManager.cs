using Const;
using UnityEngine;
using System.Collections;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] GameObject fade_obj; //フェード用オブジェクト
    [SerializeField] TextMeshProUGUI result_timer_text; //クリア時間表示オブジェクト
    [SerializeField] TextMeshProUGUI result_score_text; //クリアスコア表示オブジェクト

    private FadeManager fade_in; //FadeManager取得用
    private float clear_time; //クリア時間
    private float clear_score;//クリア時の評価
    private SoundManager sound; //サウンドインスタンス省略用

    void Start()
    {
        fade_in = fade_obj.GetComponent<FadeManager>();

        fade_in.StartFadeIn();
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
        if (clear_time % 60 < 10)
            result_timer_text.text = "クリア時間 : " + min.ToString() + ":0" + sec.ToString();
        else
            result_timer_text.text = "クリア時間 : " + min.ToString() + ":" + sec.ToString();

        //スコア表示
        result_score_text.text = "評定 : " + clear_score.ToString();

    }
}


