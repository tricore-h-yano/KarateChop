using System;
using UnityEngine;
/// <summary>
/// プレイヤーを自動で動かすクラス
/// </summary>
public class AutoPlayerMover : AutoMoverBase
{
    // アクション
    Action gameToResultAction;

    /// <summary>
    /// 初期化処理
    /// リセット関数の登録と初期ポジションの保存
    /// </summary>
    void Start()
    {
        gameToResultScreenChanger.SetAction(ResetProcess);
        keepPosition = rectTransform.position;
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
            isEnd = true;
        }
    }
    /// <summary>
    /// 全ての更新処理終了後に行う更新処理
    /// </summary>
    void LateUpdate()
    {
        if (isEnd)
        {
            gameToResultAction();
        }
    }

    /// <summary>
    /// Actionに関数を登録する処理
    /// </summary>
    /// <param name="action">セットするAction</param>
    public void SetAction(Action action)
    {
        gameToResultAction += action;
    }

    /// <summary>
    /// リセット処理
    /// </summary>
    void ResetProcess()
    {
        isMove = false;
        isEnd = false;
        receivedMoveSpeed = 0.0f;
        rectTransform.position = keepPosition;
    }
}
