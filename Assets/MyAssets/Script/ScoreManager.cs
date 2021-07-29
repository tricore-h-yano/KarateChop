using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    int totalScore;
    int nowScore;
    int normalHighScore;
    int goldHighScore;

    // 枚のstring定数
    const string Sheet = "枚";
    // ノーマル瓦のハイスコア表示用string定数
    const string NormalHighSheet = "ノーマル瓦のハイスコア";
    // ゴールド瓦のハイスコア表示用string定数
    const string GoldHighSheet = "ゴールド瓦のハイスコア";

    void Awake()
    {
        normalHighScore = PlayerPrefs.GetInt("NormalHighScore");
        goldHighScore = PlayerPrefs.GetInt("GoldHighScore");
        totalScore = PlayerPrefs.GetInt("TotalScore");
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        nowScore = 0;
        gameToResultScreenChanger.SetEndGameAction(GetNowScore);
        gameToResultScreenChanger.SetResetAction(ScoreUpdate);
    }

    /// <summary>
    /// カウンターからスコアを受け取る
    /// </summary>
    void GetNowScore()
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

        SendScore();
        SaveScore();
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

        count = nowScore.ToString();
        rankTextMesh.text = count + Sheet;

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

    void SaveScore()
    {
        PlayerPrefs.SetInt("NormalHighScore", normalHighScore);
        PlayerPrefs.SetInt("GoldHighScore", goldHighScore);
        PlayerPrefs.SetInt("TotalScore", totalScore);
        PlayerPrefs.Save();
    }
}
