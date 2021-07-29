using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 呼び出しの優先順位
/// </summary>
public enum PriorityOrder
{
    Fast = 1,
    Normal = 2,
    Slow = 3
}

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

    // ゲーム終了時アクション
    Dictionary<PriorityOrder, Action> endGameActions = new Dictionary<PriorityOrder, Action>();

    /// <summary>
    /// ゲーム終了時のアクションをセット
    /// </summary>
    /// <param name="priority">優先度</param>
    /// <param name="action">セットするアクション</param>
    public void SetEndGameAction(PriorityOrder priority, Action setAction)
    {
        if(endGameActions.ContainsKey(priority))
        {
            endGameActions[priority] += setAction;
        }
        else
        {
            endGameActions.Add(priority, setAction);
            endGameActions.OrderBy(arg => arg.Key);
        }
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
        foreach (var pair in endGameActions)
        {
            pair.Value.Invoke();
        }
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
        GameToResult();
    }
}
