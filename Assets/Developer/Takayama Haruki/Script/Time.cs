using UnityEngine;
using UnityEngine.UI;
using Const;

public class Time : MonoBehaviour
{

    public GameObject time_obj; //時間表示オブジェクト
    public int game_time; //ゲーム時間

    private int time_interval; //時間更新間隔
    private Text time_text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        time_text = time_obj.GetComponent<Text>();
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
        time_text.text = "時間 :" + game_time;
    }
}
