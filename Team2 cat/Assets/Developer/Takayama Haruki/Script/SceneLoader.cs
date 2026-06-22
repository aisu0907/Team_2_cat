using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
   public void SceneLoad(string scene_name)
    {
        Debug.Log("クリック検知");
        SceneManager.LoadScene(scene_name);
    }
}
