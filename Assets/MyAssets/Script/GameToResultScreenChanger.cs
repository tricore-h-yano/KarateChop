using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// ゲーム画面からリザルト画面へ画面遷移するクラス
/// </summary>
public class GameToResultScreenChanger : MonoBehaviour
{
    // 今のシーン
    [SerializeField] GameObject nowSceneObject;
    // 次のシーン
    [SerializeField] GameObject nextSceneObject;
    // 遷移する時間
    [SerializeField] float transitionTime;

    // リセットアクション
    Action resetAction;
    // ゲーム終了時アクション
    Action endGameAction;

    /// <summary>
    /// リセットアクションに関数を登録
    /// </summary>
    /// <param name="action">セットするAction</param>
    public void SetResetAction(Action action)
    {
        resetAction += action;
    }

    /// <summary>
    /// ゲーム終了時アクション
    /// </summary>
    /// <param name="action">セットするAction</param>
    public void SetEndGameAction(Action action)
    {
        endGameAction += action;
    }

    /// <summary>
    /// ゲーム終了コルーチン開始
    /// </summary>
    public void StartGameEndCoroutine()
    {
        StartCoroutine(GameEndCoroutine());
    }

    /// <summary>
    /// ゲーム画面からリザルト画面へ切り替える処理
    /// </summary>
    void GameToResult()
    {
        resetAction();
        nowSceneObject.SetActive(false);
        nextSceneObject.SetActive(true);
    }

    /// <summary>
    /// ゲーム終了コルーチン
    /// </summary>
    /// <returns>待機時間</returns>
    IEnumerator GameEndCoroutine()
    {
        yield return new WaitForSeconds(transitionTime);
        endGameAction();
        GameToResult();
    }
}
