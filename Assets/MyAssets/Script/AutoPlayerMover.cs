using System;
using UnityEngine;

/// <summary>
/// プレイヤーを自動で動かすクラス
/// </summary>
public class AutoPlayerMover : AutoMoverBase
{
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

        if (isMove && receivedMoveSpeed <= 0.1f)
        {
            receivedMoveSpeed = 0.0f;

            StartCoroutine(gameToResultScreenChanger.GameEndCoroutine());
        }
    }

    /// <summary>
    /// リセット処理
    /// </summary>
    void ResetProcess()
    {
        isMove = false;
        receivedMoveSpeed = 0.0f;
        myRectTransform.position = keepPosition;
    }
}
