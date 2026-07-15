using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float game_time; //ゲーム時間
    public int score_down_time; //スコアが下がる間隔
    public bool pause; //ゲームの時間停止管理用
    public float start_time { get; private set; } //初期タイム

    private TMP_Text time_text; //時間を表示するテキスト
    private bool score_down; //スコア減少管理用フラグ

    public static Timer Instance;　//シングルトン

    private void Awake()
    {
        Instance = this; //シングルトン
        
        //フラグリセット 
        pause = false;

        //数値リセット
        start_time = game_time;

        //テキストをセット
        time_text = gameObject.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        //ゲームが再生されていたら
        if(!pause)
        {
            //タイマーが0以下じゃない場合
            if(game_time > 0)
            {
                //タイムを減らす
                game_time -= Time.deltaTime;
            }
            else
                //ゲームオーバーじゃなかった場合
                if(!GameManager.Instance.is_game_over)
                    GameManager.Instance.GameOver(); //ゲームオーバーを起動
        }

        //指定の秒数経ったらかつゲームタイマーが初期じゃなかった時
        if ((int)(game_time) % 30 == 0 && start_time != game_time)
        {
            //スコアが下げれる場合
            if (score_down)
            {
                //スコアを下げる
                ScoreManager.Instance.ScoreDown();
                score_down = false;
            }
        }
        else
            score_down = true;

    }

    private void FixedUpdate()
    {
        int min = (int)game_time / 60;//分を計算
        int sec = (int)game_time % 60;//秒を計算
            
        //残り時間を表示
        if (game_time % 60 < 10)
         time_text.text = "時間 : " + min.ToString() + ":0" + sec.ToString();
        else
         time_text.text = "時間 : " + min.ToString() + ":" + sec.ToString();

    }
}
