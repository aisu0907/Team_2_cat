using NUnit.Framework.Constraints;
using UnityEngine;

namespace Const
{
    //スクリプト名は単語で大文字区切り 例(FpsLimiter)
    //変数名はすべて小文字の単語をアンダーバー区切り　例(fps_limiter)
    //関数名は頭文字大文字あとは小文字　例(Fpslimiter)

    //ゲームの基礎設定
    public static class GameConfig
    {
        public const int TICK_TIME = 60;
        
    }

    //シーンの名前
    public static class SceneName
    {
        public const string TITLE = "TitleScene";
        public const string GAME = "GameScene";
        public const string RESULT = "ResultScene";
        public const string GAMEOVER = "GameOverScene";
        public const string TWO = "two Scene";

    }

    public static class CodeAsset
    {
        public const int SPACE = 5;
    }

    public static class PosterConst
    {
        public const int COMEDY = 0;
        public const int HOLLOR = 1;
        public const int LOVECOMEDY = 2;

        public const int GENRE = 0;
        public const int TITLE = 1;
        public const int SUMMARY = 2;
    }
}