using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

/// <summary>
/// プレイヤーの当たり判定をチェックするクラス
/// </summary>
public class PlayerHitChecker : MonoBehaviour
{
    // プレイヤー
    [SerializeField] GameObject playerObject = default;

    // オブジェクト変換クラス
    [SerializeField] Transformer transformer = default;
    // ジェスチャークラス
    [SerializeField] ScreenTransformGesture transformGesture = default;

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
        if(other.gameObject.CompareTag("Tile"))
        {
            transformer.enabled = false;
            transformGesture.enabled = false;
            autoMoveFlag = true;
        }
    }
}
