using UnityEngine;
using UnityEngine.UI;
using Const;
using TMPro;

public class Time : MonoBehaviour
{

    public GameObject time_obj; //時間表示オブジェクト
    public int game_time; //ゲーム時間

    private int time_interval; //時間更新間隔
    private TMP_Text time_text;
    private int min; //分
    private int sec; //秒
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        time_text = time_obj.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        time_interval++;
        
        if(time_interval == Const.GameConfig.TICK_TIME && game_time > 0)
        {
            time_interval = 0; //インターバルリセット
            game_time--; //1秒減らす
        }
    }

    private void FixedUpdate()
    {
        min = game_time / 60;//分を計算
        sec = game_time % 60;//秒を計算
            
        //残り時間を表示
        if (game_time % 60 < 10)
         time_text.text = "時間 : " + min.ToString() + ":0" + sec.ToString();
        else
         time_text.text = "時間 : " + min.ToString() + ":" + sec.ToString();

    }
}
