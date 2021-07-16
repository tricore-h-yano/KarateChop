using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 瓦を自動で動かすクラス
/// </summary>
public class AutoMoveTile : MonoBehaviour
{
    // 移動速度
    [SerializeField] Vector3 moveSpeed = default;
    // 瓦の親オブジェクト
    [SerializeField] GameObject tileObject = default;
    // 普通の瓦画像
    [SerializeField] GameObject normalTile = default;
    // 割れた瓦画像
    [SerializeField] GameObject breakTile = default;
    // 停止ポジション
    [SerializeField] float stopPoint = default;
    // 開始ポジション
    [SerializeField] float startPoint = default;

    // 当たり判定チェッカー
    TileHitChecker checker = default;
    // 移動フラグ
    bool moveFlag;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        moveFlag = false;
        checker = tileObject.GetComponent<TileHitChecker>();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (moveFlag)
        {
            tileObject.transform.Translate(moveSpeed);
        }
        else
        {
            moveFlag = checker.AutoMoveFlag;
        }

        if(tileObject.transform.position.y > stopPoint)
        {
            RepositionProcess();
        }
    }

    /// <summary>
    /// 停止位置に到達したときの処理
    /// </summary>
    void RepositionProcess()
    {
        Vector3 position = tileObject.transform.position;
        position.y = startPoint;
        tileObject.transform.position = position;
        normalTile.SetActive(true);
        breakTile.SetActive(false);
    }

}
