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
        gameToResultScreenChanger.SetAction(Initialize);
        keepPosition = myRectTransform.position;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        base.AutoMove();
    }

    /// <summary>
    /// トリガーに触れた時
    /// </summary>
    /// <param name="other">触れたオブジェクトのコライダー </param>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(HidePointTag))
        {
            ResetImage();
            repositionAction(gameObject);
        }
    }

    /// <summary>
    /// 画像を元の状態に戻す処理
    /// </summary>
    void ResetImage()
    {
        foreach (var tile in tileObjects)
        {
            tile.SetActive(true);
        }

        foreach (var tile in breakTileObjects)
        {
            tile.SetActive(false);
        }
    }

    /// <summary>
    /// ループ中に行う初期化処理
    /// </summary>
    void Initialize()
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
