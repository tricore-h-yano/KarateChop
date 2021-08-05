using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 画面状態のステータス
/// </summary>
public enum ScreenState
{
    Title = 0,
    Tutorial = 1,
    Game = 2,
    Result = 3
}

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
/// 画面遷移を管理するクラス
/// </summary>
public class ScreenController : MonoBehaviour
{
    [SerializeField] Animator sceneControllerAnimator = default;
    [SerializeField] GameObject titleScene = default;
    [SerializeField] GameObject tutorialScene = default;
    [SerializeField] GameObject gameScene = default;
    [SerializeField] GameObject resultScene = default;

    // 遷移に使用するステータス変数
    ScreenState nowScreenState;
    ScreenState nextScreenState;

    // 遷移する時間
    [SerializeField] float transitionTime = default;
    [SerializeField] float fadeOutTime = default;

    // ゲーム終了時アクション
    Dictionary<PriorityOrder, Action> endGameActions = new Dictionary<PriorityOrder, Action>();

    bool isGameEnd;

    /// <summary>
    /// 起動時の処理
    /// </summary>
    void Awake()
    {
        nowScreenState = ScreenState.Title;
        nextScreenState = nowScreenState;
        isGameEnd = false;
    }

    /// <summary>
    /// 次の画面のステータスをもらい遷移を開始させる
    /// </summary>
    /// <param name="screenState">次の画面のステータス</param>
    public void StartTransitionScreen(ScreenState screenState)
    {
        nextScreenState = screenState;
        if(nextScreenState != nowScreenState)
        {
            StartCoroutine("FadeOut");
        }
    }

    /// <summary>
    /// 画面遷移処理
    /// </summary>
    void SelectTransitionScreen()
    {
        if (nextScreenState == ScreenState.Tutorial)
        {
            TransitionTutorial();
        }
        else if (nextScreenState == ScreenState.Game)
        {
            TransitionGame();
        }
        else if (nextScreenState == ScreenState.Title)
        {
            TransitionTitle();
        }
        else if(nextScreenState == ScreenState.Result)
        {
            TransitionResult();
        }
    }

    /// <summary>
    /// フェードインアウトのコルーチン
    /// </summary>
    /// <returns>待つ時間</returns>
    IEnumerator FadeOut()
    {
        sceneControllerAnimator.Play("Play");
        yield return new WaitForSeconds(fadeOutTime);
        SelectTransitionScreen();
        nowScreenState = nextScreenState;
    }

    /// <summary>
    /// タイトルシーンに遷移する時のオブジェクトの切り替え
    /// </summary>
    void TransitionTitle()
    {
        titleScene.SetActive(true);
        if(nowScreenState == ScreenState.Game)
        {
            GiveUpOnGameReset();
            gameScene.SetActive(false);
        }
        else if(nowScreenState == ScreenState.Tutorial)
        {
            tutorialScene.SetActive(false);
        }
        else if(nowScreenState == ScreenState.Result)
        {
            resultScene.SetActive(false);
        }
    }

    /// <summary>
    /// チュートリアルシーンに遷移する時のオブジェクトの切り替え
    /// </summary>
    void TransitionTutorial()
    {
        tutorialScene.SetActive(true);
        titleScene.SetActive(false);
    }

    /// <summary>
    /// ゲームシーンに遷移する時のオブジェクトの切り替え
    /// </summary>
    void TransitionGame()
    {
        gameScene.SetActive(true);
        titleScene.SetActive(false);
    }

    /// <summary>
    /// リザルトシーンに遷移する時のオブジェクトの切り替え
    /// </summary>
    void TransitionResult()
    {
        GameToResult();
        resultScene.SetActive(true);
        gameScene.SetActive(false);
    }

    /// <summary>
    /// ゲーム終了時のアクションをセット
    /// </summary>
    /// <param name="priority">優先度</param>
    /// <param name="action">セットするアクション</param>
    public void SetEndGameAction(PriorityOrder priority, Action setAction)
    {
        if (endGameActions.ContainsKey(priority))
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
        if(!isGameEnd)
        {
            StartCoroutine(GameEndCoroutine());
            isGameEnd = true;
        }
    }

    /// <summary>
    /// ゲーム画面からリザルト画面へ遷移する際に行う処理
    /// </summary>
    public void GameToResult()
    {
        foreach (var pair in endGameActions)
        {
            pair.Value.Invoke();
        }
    }

    /// <summary>
    /// ゲーム画面からギブアップボタンでタイトル画面へ遷移する際に行う処理
    /// </summary>
    public void GiveUpOnGameReset()
    {
        foreach (var pair in endGameActions)
        {
            if(pair.Key == PriorityOrder.Slow)
            {
                pair.Value.Invoke();
            }
        }
    }

    /// <summary>
    /// ゲーム終了コルーチン
    /// </summary>
    /// <returns>待機時間</returns>
    IEnumerator GameEndCoroutine()
    {
        yield return new WaitForSeconds(transitionTime);
        StartTransitionScreen(ScreenState.Result);
    }
}
