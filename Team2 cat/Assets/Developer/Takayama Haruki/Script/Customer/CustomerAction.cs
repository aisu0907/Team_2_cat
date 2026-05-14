using UnityEngine;
using Const;

public class CustomerAction : MonoBehaviour
{
    public GameObject comment;
    public int comment_time;

    private int count; 
    private int comment_interval;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!comment.GetComponent<CustomerText>().text_next)
        ++count;

        if(count >= Const.GameConfig.TICK_TIME)
        {
            ++comment_interval;
            count = 0;
        }

        if(comment_interval >= comment_time)
        {
            comment.GetComponent<CustomerText>().text_next = true;
            comment_interval = 0;
        }
    }
}
