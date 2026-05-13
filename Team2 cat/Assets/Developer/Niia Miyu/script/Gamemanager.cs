using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public static GameManager Instance;

    public int moves = 2;//è”

    public MovesUI movesUI;
    //public GameOverManager gameOverManager;

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        movesUI.UpdateUI(moves);
      
    }

    // Update is called once per frame
    public void UseMove()
    {
        moves--; //è”‚ğŒ¸‚ç‚·

        movesUI.UpdateUI(moves);
        if (moves <= 0)
        {
            //gameOverManager.GameOver();//ƒQ[ƒ€I—¹”»’è
        }
    }

    
}  
