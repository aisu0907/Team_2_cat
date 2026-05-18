using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public static GameManager Instance;//どこからでもゲームマネージャーを使える

    public int moves = 2;//手数

    public TMP_Text moveText;//画面に残り手数を出すUI
    //public GameOverManager gameOverManager;

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        UpdateUI();//ゲーム開始時に一度UIを表示
      
    }

    // Update is called once per frame
    public void UseMove()//手数を減らす処理
    {
        moves--; //手数を減らす
        

        if (moves <= 0)//0以下になったら
        {

            moves = 0;//マイナスにならないように
            UpdateUI();

            // 手数切れによるゲームオーバー呼び出し
            GameOverManager.Instance.GameOver();
        }
        else
        {
            UpdateUI();//まだ残っていれば数字だけ更新
        }
    }

    void UpdateUI()//画面更新
    {
        if (moveText != null)
        {
            moveText.text = "残り手数 : " + moves;
        }
    }

}  
