using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject LawText;
    public GameObject MainText;

    public TMP_Text MainTextOne;
    public TMP_Text MainTextTwo;



    public GameObject GameUI;
    public GameObject Game;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroCoroutine());
    }

    IEnumerator IntroCoroutine()
    {
        

        yield return new WaitForSecondsRealtime(2f);
        MainTextOne.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(4f);
        MainTextOne.gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(2f);
        MainTextTwo.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(4f);
        MainTextTwo.gameObject.SetActive(false);


        ShowMenu();
    }

    private void ShowMenu()
    {
        throw new NotImplementedException();
    }
}
