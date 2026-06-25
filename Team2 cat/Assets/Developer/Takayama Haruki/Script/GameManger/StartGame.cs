//using UnityEngine;
//using Const;
//using System.Collections;

//public class StartGame : MonoBehaviour
//{
//    [SerializeField] GameObject[] set_object; //スタート時にセットするオブジェクト

//   // private bool game_start;
//    private float start_time;
//    private float castomer_time; 

//    public StartGame Instans;
//    void Awake()
//    {
//        Instans = this;

//        game_start = false;
//        for (int i = 0; i < set_object.Length; i++)
//            set_object[i].SetActive(false);

//    }

//    IEnumerator StartMovie()
//    {
//        yield return new WaitForSeconds(start_time);

//        //ここで音を鳴らす

//        yield return new WaitForSeconds(castomer_time);

//        //ここでお客さんを登場

//        //その次にお客さんのテキスト]

//        //if()

//        //ゲーム開始

//    }
//}
