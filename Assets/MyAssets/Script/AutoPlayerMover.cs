using System;
using UnityEngine;

/// <summary>
/// プレイヤーを自動で動かすクラス
/// </summary>
public class AutoPlayerMover : AutoMoverBase
{
    // ゲームを終了するスピードの指標
    [SerializeField] float gameEndSpeed = default;

    /// <summary>
    /// 初期化処理
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

        // 現在の減速のさせ方だと速度が0.0f以下にならないため一定数値以下で停止とみなしています
        if (isMove && receivedMoveSpeed <= gameEndSpeed)
        {
            receivedMoveSpeed = 0.0f;
            gameToResultScreenChanger.StartGameEndCoroutine();
        }
    }

    /// <summary>
    /// ループ中に行う初期化処理
    /// </summary>
    void Initialize()
    {
        isMove = false;
        receivedMoveSpeed = 0.0f;
        myRectTransform.position = keepPosition;
    }
}
