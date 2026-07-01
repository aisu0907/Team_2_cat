using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartText : MonoBehaviour
{
    [Header("テキストデータの設定")]
    [SerializeField] TextMeshProUGUI text; //変更するテキスト先
    [SerializeField] TextAsset[] textfile; //読み取るテキストデータ
    [SerializeField] List<string[]> text_data = new List<string[]>(); //テキストファイルのテキストを保存用

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
