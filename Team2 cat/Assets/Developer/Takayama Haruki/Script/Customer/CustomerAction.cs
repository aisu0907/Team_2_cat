using UnityEngine;
using Const;

public class CustomerAction : MonoBehaviour
{
    [Header("コメントの設定")]
    [SerializeField] GameObject comment; //コメントオブジェクト
    public int comment_time; //コメントするまでの時間

    private bool new_comment; //新しいヒント管理用フラグ

    private void Start()
    {
        new_comment = false;
    }
    void Update()
    {
        if (StartGame.Instance.game_start)
        {
            //次のヒントに行ってなかったら
            if ((int)(Timer.Instance.game_time - 1) % comment_time == 0)
            {
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
