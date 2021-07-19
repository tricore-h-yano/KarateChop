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
    bool autoMoveFlag;
    public bool AutoMoveFlag { get { return autoMoveFlag; } }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        autoMoveFlag = false;
    }

    /// <summary>
    /// トリガーに触れた時
    /// </summary>
    /// <param name="other">触れたオブジェクトのコライダー</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BreakPoint"))
        {
            autoMoveFlag = true;
            transformer.enabled = false;
            transformGesture.enabled = false;
        }
    }
}
