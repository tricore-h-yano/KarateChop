using UnityEngine;

/// <summary>
/// プレイヤーを自動で動かすクラス
/// </summary>
public class AutoMovePlayer : MonoBehaviour
{
    // プレイヤー
    [SerializeField] GameObject player = default;
    // ヒットチェッカー
    [SerializeField] PlayerHitChecker checker = default;
    // コントローラー
    [SerializeField] PlayerController playerController = default;
    // 最大速度
    [SerializeField] float maxSpeed = default;

    // 移動フラグ
    bool moveFlag;
    // 実際に作用する移動速度
    float moveSpeed;
    // 仮の移動速度
    float preMoveSpeed;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        moveFlag = false;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (moveFlag)
        {
            if (preMoveSpeed >= maxSpeed)
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
            moveFlag = checker.AutoMoveFlag;
            preMoveSpeed = playerController.Speed;
        }
    }
}
