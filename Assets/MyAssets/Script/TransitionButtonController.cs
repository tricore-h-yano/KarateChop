using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ボタンを押した時に画面遷移を開始させるクラス
/// </summary>
public class TransitionButtonController : MonoBehaviour
{
    [SerializeField] ScreenController screenController = default;
    [SerializeField] Button button = default;
    [SerializeField] ScreenState screenState = default;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        button.onClick.AddListener(() => OnClickTransition());
    }

    /// <summary>
    /// ボタンが押された時画面を遷移を開始する
    /// </summary>
    void OnClickTransition()
    {
        switch (screenState)
        {
            case ScreenState.Title:
                screenController.StartTransitionScreen(ScreenState.Title);
                break;
            case ScreenState.Tutorial:
                screenController.StartTransitionScreen(ScreenState.Tutorial);
                break;
            case ScreenState.Game:
                screenController.StartTransitionScreen(ScreenState.Game);
                break;
        }
    }
}
