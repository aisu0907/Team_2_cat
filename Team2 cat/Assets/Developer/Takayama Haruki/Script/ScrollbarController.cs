using UnityEngine;
using UnityEngine.UI;

public class ScrollbarController : MonoBehaviour
{
    public float start_value; //バーの初期位置
    public float bar_size; //バーの大きさ 

    private Scrollbar scrollbar; //スクロールバーコンポーネント

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //スクロールバーコンポーネントを取得
        scrollbar = gameObject.GetComponent<Scrollbar>();

        scrollbar.value = start_value;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    scrollbar.size = bar_size;
    //}

    public void BarPosReset()
    {
        scrollbar.value = start_value;
    }
}
