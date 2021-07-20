using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 瓦グループの位置関係を管理するクラス
/// </summary>
public class TileGroupManager : MonoBehaviour
{
    // 瓦グループオブジェクトのリスト
    [SerializeField] List<AutoMoveTileGroup> tileGroupObjects = default;
    // 一番最後尾の瓦グループオブジェクト
    [SerializeField] GameObject lastGroup = default;
    // ポジションリセット時の間隔
    [SerializeField] float offset = default;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        foreach (var moveTileGroup in tileGroupObjects)
        {
            moveTileGroup.SetAction(RepositionProcess);
        }
    }

    /// <summary>
    /// リセット時のポジション変更処理
    /// </summary>
    /// <param name="hitObject">リセットするオブジェクト</param>
    void RepositionProcess(GameObject hitObject)
    {
        Vector3 position = hitObject.transform.position;
        position.y = lastGroup.transform.position.y - offset;
        hitObject.transform.position = position;
        lastGroup = hitObject.gameObject;
    }
}
