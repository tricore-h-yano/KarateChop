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
    [SerializeField] AudioSource audioSource = default;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        button.onClick.AddListener(() => OnClickTransition());
        button.onClick.AddListener(() => OnPlaySound());
    }

    /// <summary>
    /// ボタンが押された時画面を遷移を開始する
    /// </summary>
    void OnClickTransition()
    {
        screenController.StartTransitionScreen(screenState);
    }

    /// <summary>
    /// ボタンが押されたときに音を鳴らす処理
    /// </summary>
    void OnPlaySound()
    {
        audioSource.Play();
    }
}
