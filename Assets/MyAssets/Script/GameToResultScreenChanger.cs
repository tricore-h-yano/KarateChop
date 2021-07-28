using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// ゲーム画面からリザルト画面へ画面遷移するクラス
/// </summary>
public class GameToResultScreenChanger : MonoBehaviour
{
    // プレイヤーの自動移動クラス
    [SerializeField] AutoPlayerMover autoPlayerMover;
    // 今のシーン
    [SerializeField] GameObject nowSceneObject;
    // 次のシーン
    [SerializeField] GameObject nextSceneObject;
    // 遷移する時間
    [SerializeField] float transitionTime;

    // アクション
    Action resetAction;

    /// <summary>
    /// Actionに関数を登録する処理
    /// </summary>
    /// <param name="action">セットするAction</param>
    public void SetAction(Action action)
    {
        resetAction += action;
    }

    /// <summary>
    /// ゲーム画面からリザルト画面へ切り替える処理
    /// </summary>
    void GameToResultProcess()
    {
        resetAction();
        nowSceneObject.SetActive(false);
        nextSceneObject.SetActive(true);
    }

    /// <summary>
    /// ゲーム終了コルーチン
    /// </summary>
    /// <returns>待機時間</returns>
    public IEnumerator GameEndCoroutine()
    {
        yield return new WaitForSeconds(transitionTime);
        GameToResultProcess();
    }
}
