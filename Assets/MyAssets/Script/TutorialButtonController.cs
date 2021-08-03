using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButtonController : MonoBehaviour
{
    [SerializeField] SceneController sceneController;
    [SerializeField] Button button = default;
    [SerializeField] Animator sceneAnimator = default;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() =>OnClickTransition());
    }

    void OnClickTransition()
    {
        sceneAnimator.SetBool("TitleToTutorial", true);
        //sceneController.StartTitleToTutorialChange();
        //animator.SetBool("TitleToTutorial", true);
    }
}
