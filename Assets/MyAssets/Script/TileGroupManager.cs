using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 瓦グループの位置関係を管理するクラス
/// </summary>
public class TileGroupManager : MonoBehaviour
{
    // 瓦グループオブジェクトのリスト
    [SerializeField] List<AutoTileGroupMover> tileGroupObjects = default;
    // 一番最後尾の瓦グループオブジェクト
    [SerializeField] GameObject lastGroup = default;
    // ポジションリセット時の間隔
    [SerializeField] float offset = default;
    // 生成されたときに最後尾のグループを保存する
    GameObject keepLastGroup = default;

    /// <summary>
    /// 初期化処理
    /// 瓦グループオブジェクトリスト内の瓦グループにそれぞれリセット時のポジション変更処理をセットする
    /// </summary>
    void Start()
    {
        foreach (var moveTileGroup in tileGroupObjects)
        {
            moveTileGroup.SetAction(RepositionProcess);
        }

        keepLastGroup = lastGroup;
    }

    /// <summary>
    /// 無効化されたときの処理
    /// </summary>
    void OnDisable()
    {
        lastGroup = keepLastGroup;    
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
