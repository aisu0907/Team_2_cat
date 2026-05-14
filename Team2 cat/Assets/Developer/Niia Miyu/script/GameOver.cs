using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;

        SceneManager.LoadScene("GameOverScene");
    }
}
