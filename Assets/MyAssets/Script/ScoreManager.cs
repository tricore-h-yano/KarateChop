using TMPro;
using UnityEngine;

/// <summary>
/// スコアのロードと書き換えと保存するクラス
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField] BreakTileCounter breakTileCounter = default;
    [SerializeField] GameToResultScreenChanger gameToResultScreenChanger = default;
    [SerializeField] TileTypeSelector tileTypeSelector = default;

    // スコアテキスト
    [SerializeField] TextMeshProUGUI nowScoreText = default;
    [SerializeField] TextMeshProUGUI totalScoreText = default;
    [SerializeField] TextMeshProUGUI rankText = default;
    [SerializeField] TextMeshProUGUI normalHighScoreText = default;
    [SerializeField] TextMeshProUGUI goldHighScoreText = default;

    // 割った枚数によるランク付けのための閾値
    [SerializeField] float firstScoreRankThreshold = default;
    [SerializeField] float secondScoreRankThreshold = default;
    [SerializeField] float thirdScoreRankThreshold = default;

    // 割った総枚数によるランク付けのための閾値
    [SerializeField] float firstTotalScoreRankThreshold = default;
    [SerializeField] float secondTotalScoreRankThreshold = default;
    [SerializeField] float thirdTotalScoreRankThreshold = default;

    // 割った枚数
    int nowScore;
    // 総枚数
    int totalScore;
    // ノーマル瓦のハイスコア
    int normalHighScore;
    // ゴールド瓦のハイスコア
    int goldHighScore;
    // 称号
    string rank;

    // PlayerPrefsの鍵
    const string TotalScoreKey = "TotalScore";
    const string NormalHighScoreKey = "NormalHighScore";
    const string GoldHighScoreKey = "GoldHighScore";
    const string RankKey = "Rank";

    // 枚のstring定数
    const string Sheet = "枚";
    // ノーマル瓦のハイスコア表示用string定数
    const string NormalHighSheet = "ノーマル瓦のハイスコア";
    // ゴールド瓦のハイスコア表示用string定数
    const string GoldHighSheet = "ゴールド瓦のハイスコア";

    // 割った枚数ランク
    const string WhiteBelt = "白帯";
    const string BlackBelt = "黒帯";
    const string Master = "達人";
    const string Ogre = "鬼";

    // 割った総枚数ランク
    const string MyHome = "我が家の";
    const string Japanese = "日本一の";
    const string Worlds = "世界一の";
    const string Space = "宇宙一の";

    /// <summary>
    /// 起動時の処理
    /// </summary>
    void Awake()
    {
        normalHighScore = PlayerPrefs.GetInt(NormalHighScoreKey);
        goldHighScore = PlayerPrefs.GetInt(GoldHighScoreKey);
        totalScore = PlayerPrefs.GetInt(TotalScoreKey);
        rank = PlayerPrefs.GetString(RankKey);

        if (normalHighScore > 0)
        {
            normalHighScoreText.text = NormalHighSheet + normalHighScore + Sheet;
        }

        if (goldHighScore > 0)
        {
            goldHighScoreText.text = GoldHighSheet + goldHighScore + Sheet;
        }
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        nowScore = 0;
        gameToResultScreenChanger.SetEndGameAction(GetCounterScore);
        gameToResultScreenChanger.SetResetAction(ScoreUpdate);
    }

    /// <summary>
    /// カウンターからスコアを受け取る
    /// </summary>
    void GetCounterScore()
    {
        nowScore = breakTileCounter.BreakTileCount;
    }

    /// <summary>
    /// スコアの更新処理
    /// </summary>
    void ScoreUpdate()
    {
        if (nowScore > normalHighScore && !tileTypeSelector.IsGold)
        {
            normalHighScore = nowScore;
        }
        else if(nowScore > goldHighScore && tileTypeSelector.IsGold)
        {
            goldHighScore = nowScore;
        }

        totalScore += nowScore;

        RankUpdate();

        SendScore();

        SaveScore();
    }

    /// <summary>
    /// ランクの更新
    /// </summary>
    void RankUpdate()
    {
        rank = TotalScoreRankEvaluation() + ScoreRankEvaluation();
    }

    /// <summary>
    /// 割った枚数の評価
    /// </summary>
    /// <returns>評価されたランク</returns>
    string ScoreRankEvaluation()
    {
        if (firstScoreRankThreshold > nowScore)
        {
            return WhiteBelt;
        }
        else if (firstScoreRankThreshold <= nowScore && secondScoreRankThreshold > nowScore)
        {
            return BlackBelt;
        }
        else if (secondScoreRankThreshold <= nowScore && thirdScoreRankThreshold > nowScore)
        {
            return Master;
        }
        else
        {
            return Ogre;
        }
    }

    /// <summary>
    /// 総枚数の評価
    /// </summary>
    /// <returns>評価されたスコア</returns>
    string TotalScoreRankEvaluation()
    {
        if (firstTotalScoreRankThreshold > nowScore)
        {
            return MyHome;
        }
        else if (firstTotalScoreRankThreshold <= nowScore && secondTotalScoreRankThreshold > nowScore)
        {
            return Japanese;
        }
        else if (secondTotalScoreRankThreshold <= nowScore && thirdTotalScoreRankThreshold > nowScore)
        {
            return Worlds;
        }
        else
        {
            return Space;
        }
    }

    /// <summary>
    /// それぞれの描画クラスにスコアを渡す
    /// </summary>
    void SendScore()
    {
        string count = nowScore.ToString();
        nowScoreText.text = count + Sheet;

        count = totalScore.ToString();
        totalScoreText.text = count + Sheet;

        rankText.text = rank;

        if(normalHighScore > 0)
        {
            count = normalHighScore.ToString();
            normalHighScoreText.text = NormalHighSheet + count + Sheet;
        }

        if(goldHighScore > 0)
        {
            count = goldHighScore.ToString();
            goldHighScoreText.text = GoldHighSheet + count + Sheet;
        }
    }

    /// <summary>
    /// スコアの保存
    /// </summary>
    void SaveScore()
    {
        PlayerPrefs.SetInt(NormalHighScoreKey, normalHighScore);
        PlayerPrefs.SetInt(GoldHighScoreKey, goldHighScore);
        PlayerPrefs.SetInt(TotalScoreKey, totalScore);
        PlayerPrefs.Save();
    }
}
