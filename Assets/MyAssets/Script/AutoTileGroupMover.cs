using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 瓦グループの自動移動処理を行うクラス
/// </summary>
public class AutoTileGroupMover : AutoMoverBase
{
    // 瓦オブジェクトのリスト
    [SerializeField] List<GameObject> tileObjects = default;
    // 割れている瓦オブジェクトのリスト
    [SerializeField] List<GameObject> breakTileObjects = default;

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
    /// リセット関数の登録と初期ポジションの保存
    /// </summary>
    void Start()
    {
        gameToResultScreenChanger.SetAction(ResetProcess);
        keepPosition = myRectTransform.position;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        base.AutoMoveProcess();
    }

    /// <summary>
    /// トリガーに触れた時
    /// </summary>
    /// <param name="other">触れたオブジェクトのコライダー </param>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(HidePointTag))
        {
            StopResetProcess();
        }
    }

    /// <summary>
    /// 停止位置に来た時のリセット処理
    /// </summary>
    void StopResetProcess()
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

    /// <summary>
    /// リセット処理
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
        isMove = false;
        receivedMoveSpeed = 0.0f;
        myRectTransform.position = keepPosition;
    }
}
