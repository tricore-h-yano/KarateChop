using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButtonController : MonoBehaviour
{
    [SerializeField] Button button = default;
    [SerializeField] Animator animator = default;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() =>OnClickTransition());
    }

    void OnClickTransition()
    {
        animator.SetBool("TitleToTutorial", true);
    }
}
