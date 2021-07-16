using UnityEngine;


/// <summary>
/// 瓦を自動で動かすクラス
/// </summary>
public class AutoMoveTile : MonoBehaviour
{
    // 移動速度
    [SerializeField] Vector3 moveSpeed = default;
    [SerializeField] TileHitChecker checker = default;
    // 隠すポイント
    [SerializeField] float hidePoint = default;
    // 生成ポイント
    [SerializeField] float createPoint = default;

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
            transform.Translate(moveSpeed);
        }
        else
        {
            moveFlag = checker.AutoMoveFlag;
        }

        if(transform.position.y > hidePoint)
        {
            RepositionProcess();
        }
    }

    /// <summary>
    /// 停止位置に到達したときの処理
    /// </summary>
    void RepositionProcess()
    {
        Vector3 position = transform.position;
        position.y = createPoint;
        transform.position = position;
    }

}
