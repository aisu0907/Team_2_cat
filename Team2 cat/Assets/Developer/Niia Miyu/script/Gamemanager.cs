using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public static Gamemanager Instance;

    public int moves = 2;//手数

    public MovesUI movesUI;
    //public GameOverManager gameOverManager;

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        UpdateMoves();
    }

    // Update is called once per frame
    public void UseMove()
    {
        moves--; //手数を減らす

        UpdateMoves();

        if (moves <= 0)
        {
            //gameOverManager.GameOver();//ゲーム終了判定
        }
    }

    public void UpdateMoves()
    {
        movesUI.UpdateUI(moves);//表示更新
    }
}
