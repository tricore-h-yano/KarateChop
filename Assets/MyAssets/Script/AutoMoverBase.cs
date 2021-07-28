using System;
using UnityEngine;

/// <summary>
/// 自動移動を行うクラスの基底クラス
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class AutoMoverBase : MonoBehaviour
{
    // 最大速度
    [SerializeField] float maxSpeed = default;
    // ゲームを終了するスピードの指標
    [SerializeField] float gameEndSpeed = default;
    [SerializeField] PlayerHitChecker playerHitChecker = default;
    [SerializeField] PlayerController playerController = default;
    [SerializeField] RectTransform myRectTransform = default;
    [SerializeField] GameToResultScreenChanger gameToResultScreenChanger = default;

    // 初期ポジションを保存する
    Vector3 keepPosition;
    // 移動フラグ
    bool isMove;
    // 受け取った移動速度保存
    float receivedMoveSpeed;

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        Move();
    }

    /// <summary>
    /// GameToResultScreenChangerクラスのActionに関数をセットする
    /// </summary>
    /// <param name="action">セットするAction</param>
    public void SetScreenChangerAction(Action action)
    {
        gameToResultScreenChanger.SetResetAction(action);
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected void Initialize()
    {
        isMove = false;
        receivedMoveSpeed = 0.0f;
        keepPosition = myRectTransform.position;
        gameToResultScreenChanger.SetResetAction(OnEndGameReset);
    }

    /// <summary>
    /// 自動移動処理
    /// </summary>
    void Move()
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

            // 現在の減速のさせ方だと速度が0.0f以下にならないため一定数値以下で停止とみなしています
            if (receivedMoveSpeed <= gameEndSpeed)
            {
                receivedMoveSpeed = 0.0f;
                gameToResultScreenChanger.StartGameEndCoroutine();
            }
        }
        else
        {
            isMove = playerHitChecker.IsAutoMove;
            receivedMoveSpeed = playerController.Speed;
        }
    }

    /// <summary>
    /// ゲームシーン終了時に行うリセット
    /// </summary>
    void OnEndGameReset()
    {
        isMove = false;
        receivedMoveSpeed = 0.0f;
        myRectTransform.position = keepPosition;
    }
}
