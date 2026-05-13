using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class CustomerText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] TextAsset TextFile;
    private int text_num;
    private int count;

    List<string[]> TextData = new List<string[]>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StringReader reader = new StringReader(TextFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            TextData.Add(line.Split(','));
        }
    }

    // Update is called once per frame
    void Update()
    {
        string Times = TextData[text_num][count].ToString();
    }
}
