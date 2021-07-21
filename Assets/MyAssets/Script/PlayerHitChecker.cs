using UnityEngine;
using TouchScript.Gestures.TransformGestures;
using TouchScript.Behaviors;

/// <summary>
/// プレイヤーの当たり判定をチェックするクラス
/// </summary>
public class PlayerHitChecker : MonoBehaviour
{
    // ジェスチャー
    [SerializeField] ScreenTransformGesture transformGesture = default;
    // トランスフォーマー
    [SerializeField] Transformer transformer = default;

    // 自動移動フラグ
    bool isAutoMove;
    public bool IsAutoMove { get { return isAutoMove; } }

    // 動き出すポイントのTag
    const string HitMovePointTag = "HitMovePoint";

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        isAutoMove = false;
    }

    /// <summary>
    /// トリガーに触れた時
    /// </summary>
    /// <param name="other">触れたオブジェクトのコライダー</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(HitMovePointTag))
        {
            isAutoMove = true;
            transformer.enabled = false;
            transformGesture.enabled = false;
        }
    }
}
