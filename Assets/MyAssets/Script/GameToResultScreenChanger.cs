using UnityEngine;
using System;

/// <summary>
/// ゲーム画面からリザルト画面へ画面遷移するクラス
/// </summary>
public class GameToResultScreenChanger : MonoBehaviour
{
    [SerializeField] AutoMoverBase autoMoverBase;
    [SerializeField] GameObject nowSceneObject;
    [SerializeField] GameObject nextSceneObject;
    [SerializeField] float transitionTime;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        autoMoverBase.SetAction(GameToResultProcess);
    }

    void GameToResultProcess()
    {
        time += Time.deltaTime;

        if(time >= transitionTime)
        {
            nowSceneObject.SetActive(false);
            nextSceneObject.SetActive(true);
        }
    }
}
