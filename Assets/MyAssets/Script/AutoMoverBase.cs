using UnityEngine;

/// <summary>
/// 自動移動を行うクラスの基底クラス
/// </summary>
public class AutoMoverBase : MonoBehaviour
{
    // 最大速度
    [SerializeField] protected float maxSpeed = default;
    // ヒットチェッカー
    [SerializeField] protected PlayerHitChecker playerHitChecker = default;
    // プレイヤーコントローラー
    [SerializeField] protected PlayerController playerController = default;
    // 移動フラグ
    protected bool isMove;

    // 受け取った移動速度保存
    protected float receivedMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        isMove = false;
        receivedMoveSpeed = 0.0f;
    }

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
