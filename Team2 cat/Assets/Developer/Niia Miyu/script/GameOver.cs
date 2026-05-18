using UnityEngine;
using UnityEngine.SceneManagement;//シーンを切り替え機能を使うのに必要

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;//どこからでも呼べるようにする


    void Awake()
    {
        Instance = this;
    }
}
