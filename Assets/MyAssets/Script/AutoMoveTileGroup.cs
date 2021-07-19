using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveTileGroup : MonoBehaviour
{
    // プレイヤーヒットチェッカー
    [SerializeField] PlayerHitChecker playerHitChecker = default;
    // 移動速度
    /*[SerializeField] */
    float moveSpeed = default;
    // 隠すポイント
    [SerializeField] RectTransform hidePoint = default;
    // 生成ポイント
    [SerializeField] RectTransform createPoint = default;

    [SerializeField]
    PlayerController playerController = default;

    [SerializeField] List<GameObject> tileObjects = default;
    [SerializeField] List<GameObject> breakTileObjects = default;

    // 移動フラグ
    bool moveFlag;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (moveFlag)
        {
            transform.Translate(0, moveSpeed, 0);
            Debug.Log(moveSpeed);
            moveSpeed *= 0.99f;
        }
        else
        {
            moveFlag = playerHitChecker.AutoMoveFlag;
            moveSpeed = playerController.Speed;
        }

        if (transform.position.y > hidePoint.position.y)
        {
            RepositionProcess();
        }
    }

    /// <summary>
    /// 停止位置に到達したときの処理
    /// </summary>
    void RepositionProcess()
    {
        foreach(var tile in tileObjects)
        {
            tile.SetActive(true);
        }

        foreach (var tile in breakTileObjects)
        {
            tile.SetActive(false);
        }

        Vector3 position = transform.position;
        position.y = createPoint.position.y;
        transform.position = position;
    }
}
