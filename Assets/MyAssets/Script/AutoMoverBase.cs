using UnityEngine;
using System;

/// <summary>
/// 自動移動を行うクラスの基底クラス
/// </summary>
public class AutoMoverBase : MonoBehaviour
{
    // 最大速度
    [SerializeField] float maxSpeed = default;
    // ヒットチェッカー
    [SerializeField] PlayerHitChecker playerHitChecker = default;
    // プレイヤーコントローラー
    [SerializeField] PlayerController playerController = default;

    // 移動フラグ
    bool isMove;

    // ゲーム終了フラグ
    bool isEnd;

    // 受け取った移動速度保存
    float receivedMoveSpeed;

    // アクション
    Action gameToResultAction;

    /// <summary>
    /// Actionに関数を登録する処理
    /// </summary>
    /// <param name="action">セットするAction</param>
    public void SetAction(Action action)
    {
        gameToResultAction = action;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        isMove = false;
        isEnd = false;
        receivedMoveSpeed = 0.0f;
    }

    void OnEnable()
    {
        isMove = false;
        isEnd = false;
        receivedMoveSpeed = 0.0f;
    }

    void LateUpdate()
    {
        if(isEnd)
        {
            gameToResultAction();
            Debug.Log("End");
        }
    }

    /// <summary>
    /// 自動移動処理
    /// </summary>
    protected void AutoMoveProcess()
    {
        if (isMove)
        {
            float moveSpeed = 0.0f;

            if (receivedMoveSpeed >= maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
            else
            {
                moveSpeed = receivedMoveSpeed;
            }
            transform.Translate(0, moveSpeed, 0);
            receivedMoveSpeed *= 0.99f;

            if (receivedMoveSpeed <= 0.1f)
            {
                receivedMoveSpeed = 0.0f;
                isEnd = true;
            }
        }
        else
        {
            isMove = playerHitChecker.IsAutoMove;
            receivedMoveSpeed = playerController.Speed;
        }
    }
}
