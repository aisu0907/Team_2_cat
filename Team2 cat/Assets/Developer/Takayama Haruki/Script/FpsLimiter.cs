using UnityEngine;

public class FpsLimiter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Application.targetFrameRate = 60; //FPSê›íË
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
