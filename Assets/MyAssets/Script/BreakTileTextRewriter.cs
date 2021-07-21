using TMPro;
using UnityEngine;

/// <summary>
/// 割れた瓦の枚数表示テキストを書き換えるクラス
/// </summary>
public class BreakTileTextRewriter : MonoBehaviour
{
    // 割れた瓦を数えるクラス
    [SerializeField] BreakTileCounter breakTileCounter = default;
    // 割れた瓦の枚数を表示するクラス
    [SerializeField] TextMeshProUGUI breakCountTextMesh = default;

    // 枚のstring定数
    const string Sheet = "枚";

    /// <summary>
    /// 更新処理
    /// Textを書き換える
    /// </summary>
    void Update()
    {
        string count = breakTileCounter.BreakTileCount.ToString();
        breakCountTextMesh.text = count + Sheet;
    }
}
