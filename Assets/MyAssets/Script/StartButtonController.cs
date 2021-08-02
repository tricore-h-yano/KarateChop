using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TileTypeSelector tileTypeSelector;
    [SerializeField] GameObject fadeInOutObject;
    [SerializeField] GameObject nowScene;
    [SerializeField] GameObject nextScene;
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        button.onClick.AddListener(Transition);
    }

    void Transition()
    {
        StartCoroutine("FadeInOut");
    }

    IEnumerator FadeInOut()
    {
        tileTypeSelector.SelectColor();
        fadeInOutObject.SetActive(true);
        yield return new WaitForSeconds(1);
        nowScene.SetActive(false);
        nextScene.SetActive(true);
        fadeInOutObject.SetActive(false);
    }
}
