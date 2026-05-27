using UnityEngine;
using Const;

public class CustomerAction : MonoBehaviour
{
    public GameObject comment;
    public int comment_time;

    private int count; 
    private int comment_interval;

    void Start()
    {
        //リセット
        count = 0;
        comment_interval = 0;
}

    void Update()
    {
        //次のヒントに行ってなかったら
        if(!comment.GetComponent<CustomerText>().text_next)
        ++count;

        //1秒たったら
        if(count >= GameConfig.TICK_TIME)
        {
            ++comment_interval;
            count = 0;
        }

        //インターバルが終了したら
        if(comment_interval >= comment_time)
        {
            comment.GetComponent<CustomerText>().text_next = true; //次のヒントに行く
            comment_interval = 0; //インターバルリセット
        }
    }
}
