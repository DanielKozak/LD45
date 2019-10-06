using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelloScreenManager : MonoBehaviour
{

    public Image Fader;

    public TMP_Text text1;
    public TMP_Text text2;

    public GameObject MainMenu;
    public GameObject Blocker;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        StartCoroutine(HelloCoroutine());
    }



    public IEnumerator HelloCoroutine()
    {
        yield return null;

        float a = 1f;
        Color c; 

        while (Fader.color.a >= 0f)
        {
            a -= 0.01f;
            c = new Color(0, 0, 0, a);
            Fader.color = c;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(5f);


        a = 0f;
        while (Fader.color.a <= 1f)
        {
            a += 0.01f;
            c = new Color(0, 0, 0, a);
            Fader.color = c;
            yield return null;
        }

        text1.gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(1f);
        text2.gameObject.SetActive(true);

        a = 1f;

        while (Fader.color.a >= 0f)
        {
            a -= 0.01f;
            c = new Color(0, 0, 0, a);
            Fader.color = c;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(5f);

        a = 0f;
        while (Fader.color.a <= 1f)
        {
            a += 0.01f;
            c = new Color(0, 0, 0, a);
            Fader.color = c;
            yield return null;
        }

        text2.gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(1f);

        a = 1f;

        while (Fader.color.a >= 0f)
        {
            a -= 0.01f;
            c = new Color(0, 0, 0, a);
            Fader.color = c;
            yield return null;
        }
        
        MainMenu.SetActive(true);
        Blocker.SetActive(false);
    }
}
