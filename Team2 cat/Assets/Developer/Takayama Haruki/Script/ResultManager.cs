using Const;
using UnityEngine;
using System.Collections;

public class ResultManager : MonoBehaviour
{
    [SerializeField] GameObject fade_obj; //フェード用オブジェクト
    [SerializeField] GameObject result_timer_text;
    [SerializeField] GameObject customer_result_text;

    private FadeManager fade_in; //FadeManager取得用
    private float time;
    private float score;
    private SoundManager sound; //サウンドインスタンス省略用

    void Start()
    {
        fade_in = fade_obj.GetComponent<FadeManager>();

        fade_in.StartFadeIn();
        time = GameManager.result_time;
        score = ScoreManager.result_score;

        sound = SoundManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


