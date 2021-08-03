using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator sceneController;
    [SerializeField] GameObject titleScene;
    [SerializeField] GameObject tutorialScene;
    [SerializeField] GameObject gameScene;
    [SerializeField] GameObject resultScene;

    //void Update()
    //{
    //    if(sceneController.GetBool("TitleToTutorial"))
    //    {
    //        StartCoroutine(TitleToTutorialChange());
    //    }

    //    if (sceneController.GetBool("TutorialToTitle"))
    //    {
    //        StartCoroutine(TutorialToTitleChange());
    //    }
    //}

    public void StartTitleToTutorialChange()
    {
        titleScene.SetActive(false);
        tutorialScene.SetActive(true);
        //sceneController.SetBool("TitleToTutorial", false);

        //StartCoroutine(TitleToTutorialChange());
    }

    //IEnumerator TitleToTutorialChange()
    //{
    //    //sceneController.SetBool("TitleToTutorial", true);
    //    //yield return new WaitForSeconds(0.5f);
    //    titleScene.SetActive(false);
    //    tutorialScene.SetActive(true);
    //    sceneController.SetBool("TitleToTutorial", false);
    //}

    IEnumerator TutorialToTitleChange()
    {
        yield return new WaitForSeconds(1);
        titleScene.SetActive(true);
        tutorialScene.SetActive(false);
        sceneController.SetBool("TutorialToTitle", false);
    }
}
