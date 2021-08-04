using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiveUpButtonController : MonoBehaviour
{
    [SerializeField] ScreenController screenController;
    [SerializeField] Button button = default;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => OnClickTransition());
    }

    void OnClickTransition()
    {
        screenController.StartTransitionScreen(ScreenState.Title);
    }
}
