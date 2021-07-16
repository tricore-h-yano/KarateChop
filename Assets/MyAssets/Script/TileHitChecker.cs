using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

/// <summary>
/// 瓦の当たり判定をチェックするクラス
/// </summary>
public class TileHitChecker : MonoBehaviour
{
    // 瓦
    [SerializeField] GameObject normalTile = default;
    // 割れている瓦
    [SerializeField] GameObject breakTile = default;

    // 自動移動フラグ
    bool autoMoveFlag;
    public bool AutoMoveFlag { get { return autoMoveFlag; } }

    // Tag判別用string
    // Player
    const string PlayerTag = "Player";
    // BreakPoint
    const string BreakPointTag = "BreakPoint";

    /// <summary>
    /// トリガーに触れた時
    /// </summary>
    /// <param name="other">触れたオブジェクトのコライダー</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerTag))
        {
            autoMoveFlag = true;
        }

        if(other.gameObject.CompareTag(BreakPointTag))
        {
            normalTile.SetActive(false);
            breakTile.SetActive(true);
        }
    }
}
