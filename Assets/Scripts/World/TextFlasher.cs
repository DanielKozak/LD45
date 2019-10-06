using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFlasher : MonoBehaviour
{
    public float delay = 0.2f;

    TextMeshProUGUI Text;
    

    Color32 ac;
    private void OnEnable()
    {
        Text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void StartFlashing(Color accent)
    {
        ac = accent;
        StartCoroutine(FlashRoutine(ac));
    }

    public void StopFlashing()
    {
        StopCoroutine(FlashRoutine(ac));
    }

    private IEnumerator FlashRoutine(Color accent)
    {
        while (true)
        {
            Text.color = accent;
            yield return new WaitForSecondsRealtime(delay);

            Text.color = Color.white;
            yield return new WaitForSecondsRealtime(delay);
        }
    }
}
