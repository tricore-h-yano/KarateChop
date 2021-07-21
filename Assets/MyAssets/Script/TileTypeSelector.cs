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
    /// buttonが押された際の処理
    /// </summary>
    public void OnClick()
    {
        float random = Random.value;

        if(random >= randomThreshold)
        {
            GoldTileActive();
        }
        else
        {
            NormalTileActive();
        }

    }

    /// <summary>
    /// 普通の瓦をアクティブにする処理
    /// </summary>
    void NormalTileActive()
    {
        gameObjectRoot.SetActive(true);
        normalTileObjectRoot.SetActive(true);
    }

    /// <summary>
    /// 金の瓦をアクティブにする処理
    /// </summary>
    void GoldTileActive()
    {
        gameObjectRoot.SetActive(true);
        goldTileObjectRoot.SetActive(true);
    }

}
