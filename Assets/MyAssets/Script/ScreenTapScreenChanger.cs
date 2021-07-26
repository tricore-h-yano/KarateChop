using UnityEngine;
using TouchScript.Gestures;
using System;

/// <summary>
/// スクリーンをタップしたときに画面を切り替えるクラス
/// </summary>
public class ScreenTapScreenChanger : MonoBehaviour
{
    // ジェスチャークラス
    [SerializeField] TapGesture tapGesture = default;
    // 今のシーンのオブジェクト
    [SerializeField] GameObject nowSceneObject = default;
    // 次のシーンのオブジェクト
    [SerializeField] GameObject nextSceneObject = default;

    /// <summary>
    /// 有効化されたときの処理
    /// </summary>
    void OnEnable()
    {
        // Transform Gestureのdelegateに登録
        tapGesture.Tapped += OnTappedSceneChange;
    }

    /// <summary>
    /// 有効化されたときの処理
    /// </summary>
    void OnDisable()
    {
        // Transform Gestureのdelegateに登録
        tapGesture.Tapped -= OnTappedSceneChange;
    }

    /// <summary>
    /// スクリーンがタップされたときに画面を切り替える処理
    /// </summary>
    /// <param name="sender">送信者となるオブジェクト</param>
    /// <param name="e">イベント</param>
    void OnTappedSceneChange(object sender, EventArgs e)
    {
        nowSceneObject.SetActive(false);
        nextSceneObject.SetActive(true);
    }
}
