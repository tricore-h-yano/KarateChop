using UnityEngine;

// ToDo:タイトルがまだ未実装なのでひとまずゲーム開始処理も含んでいます
/// <summary>
/// 普通の瓦を出すか金の瓦を出すかを決めるクラス
/// </summary>
public class TileTypeSelector : MonoBehaviour
{
    // ゲームシーンそのものの親クラス
    [SerializeField] GameObject gameObjectRoot = default;
    // 普通の瓦オブジェクトの親クラス
    [SerializeField] GameObject normalTileObjectRoot = default;
    // 金の瓦オブジェクトの親クラス
    [SerializeField] GameObject goldTileObjectRoot = default;
    // 金の瓦が出現するランダムな値の閾値
    [SerializeField] float randomThreshold = default;

    /// <summary>
    /// ランダムで色を切り替える処理
    /// </summary>
    public void SelectColor()
    {
        float random = Random.value;
        gameObjectRoot.SetActive(true);
        goldTileObjectRoot.SetActive(random >= randomThreshold);
        normalTileObjectRoot.SetActive(!goldTileObjectRoot.activeSelf);
    }
}
