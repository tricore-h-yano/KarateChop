using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] BreakTileCounter breakTileCounter = default;
    [SerializeField] GameToResultScreenChanger gameToResultScreenChanger = default;
    [SerializeField] TileTypeSelector tileTypeSelector = default;

    [SerializeField] TextMeshProUGUI nowScoreTextMesh = default;
    [SerializeField] TextMeshProUGUI totalScoreTextMesh = default;
    [SerializeField] TextMeshProUGUI rankTextMesh = default;
    [SerializeField] TextMeshProUGUI normalHighScoreTextMesh = default;
    [SerializeField] TextMeshProUGUI goldHighScoreTextMesh = default;

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
            normalHighScoreTextMesh.text = NormalHighSheet + normalHighScore + Sheet;

        }

        if (goldHighScore > 0)
        {
            goldHighScoreTextMesh.text = GoldHighSheet + goldHighScore + Sheet;
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
    /// 更新処理
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("セーブデータ削除");
            DeleteSaveDate();
        }
    }

    /// <summary>
    /// セーブデータ削除
    /// </summary>
    void DeleteSaveDate()
    {
        PlayerPrefs.DeleteAll();
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
        string scoreRank;
        string totalScoreRank;

        // 割った枚数の評価
        if (firstScoreRankThreshold > nowScore)
        {
            scoreRank = WhiteBelt;
        }
        else if(firstScoreRankThreshold <= nowScore && secondScoreRankThreshold > nowScore)
        {
            scoreRank = BlackBelt;
        }
        else if(secondScoreRankThreshold <= nowScore && thirdScoreRankThreshold> nowScore)
        {
            scoreRank = Master;
        }
        else
        {
            scoreRank = Ogre;
        }

        // 総枚数の評価
        if (firstTotalScoreRankThreshold > nowScore)
        {
            totalScoreRank = MyHome;
        }
        else if (firstTotalScoreRankThreshold <= nowScore && secondTotalScoreRankThreshold > nowScore)
        {
            totalScoreRank = Japanese;
        }
        else if (secondTotalScoreRankThreshold <= nowScore && thirdTotalScoreRankThreshold > nowScore)
        {
            totalScoreRank = Worlds;
        }
        else
        {
            totalScoreRank = Space;
        }

        rank = totalScoreRank + scoreRank;
    }

    /// <summary>
    /// それぞれの描画クラスにスコアを渡す
    /// </summary>
    void SendScore()
    {
        string count = nowScore.ToString();
        nowScoreTextMesh.text = count + Sheet;

        count = totalScore.ToString();
        totalScoreTextMesh.text = count + Sheet;

        rankTextMesh.text = rank;

        if(normalHighScore > 0)
        {
            count = normalHighScore.ToString();
            normalHighScoreTextMesh.text = NormalHighSheet + count + Sheet;

        }

        if(goldHighScore > 0)
        {
            count = goldHighScore.ToString();
            goldHighScoreTextMesh.text = GoldHighSheet + count + Sheet;
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
