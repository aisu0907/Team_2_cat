using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public static GameManager Instance;//どこからでもゲームマネージャーを使える

    public int moves = 2;//手数

    public TMP_Text moveText;
    //public GameOverManager gameOverManager;

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        UpdateUI();
      
    }

    // Update is called once per frame
    public void UseMove()
    {
        moves--; //手数を減らす
       
        if (moves < 0)
            moves = 0;
        UpdateUI();
    }

    void UpdateUI()//画面更新
    {
        moveText.text ="残り手数 : "+ moves;
    }

}  
