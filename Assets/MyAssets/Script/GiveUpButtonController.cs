using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ギブアップボタンを押した時に画面遷移を開始させるクラス
/// </summary>
public class GiveUpButtonController : MonoBehaviour
{
    [SerializeField] ScreenController screenController;
    [SerializeField] Button button = default;

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
        screenController.StartTransitionScreen(ScreenState.Title);
    }
}
