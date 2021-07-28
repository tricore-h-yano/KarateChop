using UnityEngine;

/// <summary>
/// 割れた瓦を数えるクラス
/// </summary>
public class BreakTileCounter : MonoBehaviour
{
    // 割れた瓦を表示するクラス
    [SerializeField] GameObject breakTileText = default;
    // スクリーンチェンジャー
    [SerializeField] GameToResultScreenChanger gameToResultScreenChanger = default;
    // 割れた瓦の枚数カウント
    int breakTileCount;
    public int BreakTileCount { get { return breakTileCount; } }

    // 瓦のTag
    const string TileTag = "Tile";

    /// <summary>
    /// 初期化処理
    /// リセット関数の登録
    /// </summary>
    void Start()
    {
        breakTileCount = 0;
        gameToResultScreenChanger.SetAction(ResetProcess);
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
    /// リセット処理
    /// </summary>
    void ResetProcess()
    {
        breakTileCount = 0;
        breakTileText.SetActive(false);
    }
}
