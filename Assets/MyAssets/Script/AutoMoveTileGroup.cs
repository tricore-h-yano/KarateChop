using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 瓦グループの自動移動処理を行うクラス
/// </summary>
public class AutoMoveTileGroup : MonoBehaviour
{
    // プレイヤーヒットチェッカー
    [SerializeField] PlayerHitChecker playerHitChecker = default;
    // プレイヤーコントローラー
    [SerializeField] PlayerController playerController = default;
    // 瓦オブジェクトのリスト
    [SerializeField] List<GameObject> tileObjects = default;
    // 割れている瓦オブジェクトのリスト
    [SerializeField] List<GameObject> breakTileObjects = default;
    // 最大速度
    [SerializeField] float maxSpeed = default;

    // 移動フラグ
    bool moveFlag;

    // 実際に作用する移動速度
    float moveSpeed;
    // 仮の移動速度
    float preMoveSpeed;

    // ブレイクポイントのTag
    const string HidePointTag = "HidePoint";

    // アクション
    Action<GameObject> repositionAction;

    /// <summary>
    /// Actionに関数を登録する処理
    /// </summary>
    /// <param name="action">セットするAction</param>
    public void SetAction(Action<GameObject> action)
    {
        repositionAction = action;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        moveFlag = false;
        moveSpeed = 0.0f;
        preMoveSpeed = 0.0f;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (moveFlag)
        {
            if(preMoveSpeed >= maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
            else
            {
                moveSpeed = preMoveSpeed;
            }
            transform.Translate(0, moveSpeed, 0);
            preMoveSpeed *= 0.99f;
        }
        else
        {
            moveFlag = playerHitChecker.AutoMoveFlag;
            preMoveSpeed = playerController.Speed;
        }
    }

    /// <summary>
    /// トリガーに触れた時
    /// </summary>
    /// <param name="other">触れたオブジェクトのコライダー </param>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(HidePointTag))
        {
            ResetProcess();
        }
    }

    /// <summary>
    /// 停止位置に来た時のリセット処理
    /// </summary>
    void ResetProcess()
    {
        foreach (var tile in tileObjects)
        {
            tile.SetActive(true);
        }

        foreach (var tile in breakTileObjects)
        {
            tile.SetActive(false);
        }

        repositionAction(gameObject);
    }
}
