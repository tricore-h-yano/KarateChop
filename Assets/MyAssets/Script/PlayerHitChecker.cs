using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

/// <summary>
/// プレイヤーの当たり判定をチェックするクラス
/// </summary>
public class PlayerHitChecker : MonoBehaviour
{
    // プレイヤー
    [SerializeField] GameObject playerObject;

    // オブジェクト変換クラス
    Transformer transformer = default;
    // ジェスチャークラス
    ScreenTransformGesture transformGesture = default;

    // 自動移動フラグ
    bool autoMoveFlag;
    public bool AutoMoveFlag { get { return autoMoveFlag; } }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        transformer = playerObject.GetComponent<Transformer>();
        transformGesture = playerObject.GetComponent<ScreenTransformGesture>();
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
