using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public SpriteRenderer framerenderer;//色を変えたい画像を入れる
    public Color normalColor = Color.white;//通常が白
    public Color selectedColor = Color.red;//選択時が赤

    private static SelectableObject currentSelected;//全オブジェクト共通で1つ

    void Start()
    {
        framerenderer.color = normalColor;//初めの色指定
       
    }

    void OnMouseDown()//このオブジェクトがクリックされた瞬間
    {
        // 前の選択を戻す
        if (currentSelected != null)
        {
             currentSelected.framerenderer.color =
             currentSelected.normalColor;
            
        }

        // 今回クリックしたものを選択
        framerenderer.color = selectedColor;
        currentSelected = this;//クリックされたものを保存
    }
}