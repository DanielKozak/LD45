using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHelloScreenController : MonoBehaviour
{
    public GameObject World;
    public GameObject UI;

    public Image Fader;

    private void OnEnable()
    {
        StartCoroutine(HelloRoutine());
    }

    private IEnumerator HelloRoutine()
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

        UI.SetActive(true);
        World.SetActive(true);
        gameObject.SetActive(false);
    }
}
