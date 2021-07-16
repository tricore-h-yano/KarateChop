using UnityEngine;

/// <summary>
/// プレイヤーを自動で動かすクラス
/// </summary>
public class AutoMovePlayer : MonoBehaviour
{
    // 移動速度
    [SerializeField] Vector3 moveSpeed = default;
    // プレイヤー
    [SerializeField] GameObject player = default;
    // ヒットチェッカー
    [SerializeField] PlayerHitChecker checker = default;
    // 移動フラグ
    bool moveFlag;

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
            // TODO:瓦とプレイヤーの移動速度を同期させる 
            player.transform.Translate(moveSpeed);
        }
        else
        {
            moveFlag = checker.AutoMoveFlag;
        }
    }
}
