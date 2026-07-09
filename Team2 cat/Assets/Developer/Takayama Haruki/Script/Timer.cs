using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float game_time; //ゲーム時間
    public bool pause;//ゲームの時間停止管理用

    private TMP_Text time_text;//時間を表示するテキスト
    private int min;//分
    private int sec;//秒

    private void Start()
    {
        pause = false;
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
    }

    private void FixedUpdate()
    {
        min = (int)game_time / 60;//分を計算
        sec = (int)game_time % 60;//秒を計算
            
        //残り時間を表示
        if (game_time % 60 < 10)
         time_text.text = "時間 : " + min.ToString() + ":0" + sec.ToString();
        else
         time_text.text = "時間 : " + min.ToString() + ":" + sec.ToString();

    }
}
