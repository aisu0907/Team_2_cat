using UnityEngine;

public class Select : MonoBehaviour
{
    private SpriteRenderer sr;
    public GameObject outline;

    private static Select currentSelected;//１個の記憶（常に１つだけ保存）
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        outline.SetActive(false);
    }

    void OnMouseDown()//クリックされた
    {
        SelectThis();
        Gamemanager.Instance.UseMove();
    }

    void SelectThis()
    {
        // 前の選択を解除
        if (currentSelected != null)
        {
            currentSelected.SetSelected(false);
        }

        // 今のやつを選択にする
        currentSelected = this;
        SetSelected(true);
    }

    public void SetSelected(bool value)
    {
        outline.SetActive(value);
        sr.color = value ? Color.yellow : Color.white;//色変更
    }
}