using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public SpriteRenderer frameRenderer;//色を変えたい画像を入れる

    public Color normalColor = Color.white;//通常が白
    public Color selectedColor = Color.red;//選択時が赤

    private static SelectableObject currentSelected;//全オブジェクト共通で1つ

    void Start()
    {
        frameRenderer.color = normalColor;//初めの色指定
       
    }

    void OnMouseDown()//このオブジェクトがクリックされた瞬間
    {
        // 前の選択を戻す
        if (currentSelected != null)
        {
             currentSelected.frameRenderer.color =
             currentSelected.normalColor;
            
        }

        // 今回クリックしたものを選択
        frameRenderer.color = selectedColor;
        currentSelected = this;//クリックされたものを保存
        
        // 手数消費
        GameManager.Instance.UseMove();
    }
}