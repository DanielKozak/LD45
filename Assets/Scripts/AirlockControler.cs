using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class AirlockControler : MonoBehaviour
{
    public enum State {
        Open,
        Closed,
        
    }
    public Light2D IndicatorLight;
    public SpriteRenderer IndicatorSprite;

    public bool isOpen = false;

    public bool charging = false;

    public void GreenLight()
    {
        IndicatorLight.color = Color.green;
        IndicatorSprite.color = Color.green;
    }

    public void RedLight()
    {
        IndicatorLight.color = Color.red;
        IndicatorSprite.color = Color.red;
    }
    public float openSpeed = 10f;

    public IEnumerator Open()
    {
        if (charging) yield break;
        while (gameObject.transform.position.y <= -8)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + Time.deltaTime * openSpeed, gameObject.transform.position.z);
            yield return null;
        }
        isOpen = true;
        IndicatorLight.intensity = 0;

    }
    public IEnumerator CloseAndDepressurize()
    {
        while (gameObject.transform.position.y >= -14)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - Time.deltaTime * openSpeed, gameObject.transform.position.z);
            yield return null;
        }
        IndicatorLight.intensity = 0.5f;

        yield return new WaitForSecondsRealtime(1f);

        StartCoroutine(Depressurize());

    }

    public IEnumerator Depressurize()
    {
        RedLight();
        //TODO: Play sound
        yield return new WaitForSecondsRealtime(5f);
        charging = true;
        yield return new WaitForSecondsRealtime(10f);
        charging = false;
        GreenLight();

    }
    public IEnumerator PressurizeAndOpen()
    {
        //TODO: Play sound
        yield return new WaitForSecondsRealtime(5f);
        GreenLight();
        StartCoroutine(Open());

    }
    private void Update()
    {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                StartCoroutine(PressurizeAndOpen());
            }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            StartCoroutine(CloseAndDepressurize());
        }
    }
}
