using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

/// <summary>
/// 瓦の当たり判定をチェックするクラス
/// </summary>
public class TileHitChecker : MonoBehaviour
{
    // 割れていない瓦
    [SerializeField] GameObject tileObject;
    // 割れている瓦
    [SerializeField] GameObject breakTileObject;

    /// <summary>
    /// コライダーに触れた時
    /// </summary>
    /// <param name="collision">触れたオブジェクトのコライダー</param>
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<ScreenTransformGesture>().enabled = false;
        collision.gameObject.GetComponent<Transformer>().enabled = false;
        collision.rigidbody.isKinematic = true;
        tileObject.SetActive(false);
        breakTileObject.SetActive(true);
    }

}
