using UnityEngine;

/// <summary>
/// 割れた瓦を数えるクラス
/// </summary>
public class BreakTileCounter : MonoBehaviour
{
    // 割れた瓦を表示するクラス
    [SerializeField] GameObject breakTileText = default;
    [SerializeField] ScreenController screenController = default;

    // 割れた瓦の枚数カウント
    int breakTileCount;
    public int BreakTileCount { get { return breakTileCount; } }

    // 瓦のTag
    const string TileTag = "Tile";

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        breakTileCount = 0;
        screenController.SetEndGameAction(PriorityOrder.Normal, ResetOnEndGame);
    }

    /// <summary>
    /// トリガーに触れた時の処理
    /// </summary>
    /// <param name="other">触れたオブジェクトのコライダー</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(TileTag))
        {
            ++breakTileCount;
            if(!breakTileText.activeSelf)
            {
                breakTileText.SetActive(true);
            }
        }
    }

    /// <summary>
    /// ゲームシーン終了時に行うリセット
    /// </summary>
    void ResetOnEndGame()
    {
        breakTileCount = 0;
        breakTileText.SetActive(false);
    }
}
