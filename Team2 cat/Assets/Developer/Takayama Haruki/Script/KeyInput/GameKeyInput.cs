using UnityEngine;
using UnityEngine.InputSystem;
public class GameController : MonoBehaviour
{
    private GameController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        controller = new GameController(); 
    }

    private void OnDisable()
    {
        //controller.
    }
}
