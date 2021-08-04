using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

public class ScreenController : MonoBehaviour
{
    [SerializeField] Animator sceneController;
    [SerializeField] GameObject titleScene;
    [SerializeField] GameObject tutorialScene;
    [SerializeField] GameObject gameScene;
    [SerializeField] GameObject resultScene;
    [SerializeField] ScreenState nowScreenState = default;
    
    ScreenState nextScreenState;

    // 遷移する時間
    [SerializeField] float transitionTime;

    // ゲーム終了時アクション
    Dictionary<PriorityOrder, Action> endGameActions = new Dictionary<PriorityOrder, Action>();


    void Awake()
    {
        nextScreenState = nowScreenState;
    }

    void Update()
    {
        if(nowScreenState != nextScreenState)
        {
            StartCoroutine("FadeOut");
            nowScreenState = nextScreenState;
        }
    }

    public void StartTransitionScreen(ScreenState screenState)
    {
        nextScreenState = screenState;
    }

    void TransitionScreen()
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

    IEnumerator FadeOut()
    {
        sceneController.Play("Play");
        yield return new WaitForSeconds(0.5f);
        TransitionScreen();
        nowScreenState = nextScreenState;
    }

    void TransitionTitle()
    {
        Debug.Log("TransitionTitle");
        titleScene.SetActive(true);
        tutorialScene.SetActive(false);
        gameScene.SetActive(false);
        resultScene.SetActive(false);
    }

    void TransitionTutorial()
    {
        Debug.Log("TransitionTutorial");
        tutorialScene.SetActive(true);
        titleScene.SetActive(false);
        gameScene.SetActive(false);
        resultScene.SetActive(false);
    }

    void TransitionGame()
    {
        Debug.Log("TransitionGame");
        gameScene.SetActive(true);
        titleScene.SetActive(false);
        tutorialScene.SetActive(false);
        resultScene.SetActive(false);
    }

    void TransitionResult()
    {
        Debug.Log("TransitionResult");
        GameToResult();
        resultScene.SetActive(true);
        gameScene.SetActive(false);
        titleScene.SetActive(false);
        tutorialScene.SetActive(false);
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
        StartCoroutine(GameEndCoroutine());
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
    /// ゲーム終了コルーチン
    /// </summary>
    /// <returns>待機時間</returns>
    IEnumerator GameEndCoroutine()
    {
        yield return new WaitForSeconds(transitionTime);
        StartTransitionScreen(ScreenState.Result);
    }

}
