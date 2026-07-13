using UnityEngine;
using Const;
using System.Threading;

public class CustomerAction : MonoBehaviour
{
    [Header("コメントの設定")]
    [SerializeField] GameObject comment; //コメントオブジェクト
    public int comment_time; //コメントするまでの時間

    private bool new_comment; //新しいヒント管理用フラグ
    private Timer timer; //シングルトン省略用
    private void Start()
    {
        new_comment = false;
        timer = Timer.Instance; //省略
    }
    void Update()
    {
        if (StartGame.Instance.game_start)
        {
            //指定の秒数経ったらかつゲームタイマーが初期じゃなかった時
            if ((int)timer.game_time % comment_time == 0 && timer.start_time != timer.game_time)
            {
                //次のヒントに行ってなかったら
                if (new_comment)
                {
                    comment.GetComponent<CustomerText>().text_next = true;
                    new_comment = false;
                }
            }
            else
                new_comment = true;
        }
    }
}
