using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    [SerializeField] TileTypeSelector tileTypeSelector;
    [SerializeField] ScreenController screenController;
    [SerializeField] Button button = default;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        button.onClick.AddListener(() => OnClickTransition());
    }

    void OnClickTransition()
    {
        tileTypeSelector.SelectColor();
        screenController.StartTransitionScreen(ScreenState.Game);
    }
}
