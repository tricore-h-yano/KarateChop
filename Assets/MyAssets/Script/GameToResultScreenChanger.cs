using UnityEngine;
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

    // 時間計測
    float time;

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
    /// 初期化処理
    /// リセット関数を登録
    /// </summary>
    void Start()
    {
        autoPlayerMover.SetAction(GameToResultProcess);
    }

    /// <summary>
    /// ゲーム画面からリザルト画面へ切り替える処理
    /// </summary>
    void GameToResultProcess()
    {
        time += Time.deltaTime;

        if(time >= transitionTime)
        {
            resetAction();
            nowSceneObject.SetActive(false);
            nextSceneObject.SetActive(true);
        }
    }
}
