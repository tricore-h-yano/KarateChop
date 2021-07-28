using UnityEngine;

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
    // スクリーンチェンジャー
    [SerializeField] protected GameToResultScreenChanger gameToResultScreenChanger = default;
    // レクトトランスフォーム
    [SerializeField] protected RectTransform rectTransform = default;

    // 初期ポジションを保存する
    protected Vector3 keepPosition;
    // 移動フラグ
    protected bool isMove;
    // ゲーム終了フラグ
    protected bool isEnd;
    // 受け取った移動速度保存
    protected float receivedMoveSpeed;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        isMove = false;
        isEnd = false;
        receivedMoveSpeed = 0.0f;
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
        }
        else
        {
            isMove = playerHitChecker.IsAutoMove;
            receivedMoveSpeed = playerController.Speed;
        }
    }
}
