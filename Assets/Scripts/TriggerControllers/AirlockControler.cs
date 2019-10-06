using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class AirlockControler : MonoBehaviour
{
    public static AirlockControler Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    public Light2D IndicatorLight;
    public SpriteRenderer IndicatorSprite;

    public SpriteRenderer onnSprite;
    public SpriteRenderer offSprite;

    public bool isOpen;

    public bool charging;

    public bool cycling;
    
    private void Start()
    {
        isOpen = false;
    }
    private void GreenLight()
    {
        IndicatorLight.color = Color.green;
        IndicatorSprite.color = Color.green;
    }

    private void RedLight()
    {
        IndicatorLight.color = Color.red;
        IndicatorSprite.color = Color.red;
    }
    private float openSpeed = 10f;

    private IEnumerator Open()
    {
        while (gameObject.transform.localPosition.y < -8)
        {
            Debug.Log(gameObject.transform.localPosition.y);

            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + Time.deltaTime * openSpeed, gameObject.transform.localPosition.z);
            yield return null;
        }
        isOpen = true;
        IndicatorLight.intensity = 0;
        cycling = false;
    }
    private IEnumerator CloseAndDepressurize()
    {
        cycling = true;
        isOpen = false;
        offSprite.gameObject.SetActive(true);
        onnSprite.gameObject.SetActive(false);

        BatteryController.Instance.WorkAirlock();

        while (gameObject.transform.localPosition.y >= -14)
        {

            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - Time.deltaTime * openSpeed, gameObject.transform.localPosition.z);
            yield return new WaitForEndOfFrame();
        }
        IndicatorLight.intensity = 0.5f;

        yield return new WaitForSecondsRealtime(1f);

        StartCoroutine(Depressurize());

    }

    private IEnumerator Depressurize()
    {
        RedLight();
        //TODO: Play sound
        yield return new WaitForSecondsRealtime(5f);
        charging = true;
        yield return new WaitForSecondsRealtime(5f);
        charging = false;
        cycling = false;
    }
    private IEnumerator PressurizeAndOpen()
    {
        if (charging) yield break;
        cycling = true;
        //TODO: Play sound
        offSprite.gameObject.SetActive(false);
        onnSprite.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        GreenLight();
        StartCoroutine(Open());

    }
    

    public void Toggle()
    {
        if (cycling) return;

        if(isOpen)
            StartCoroutine(CloseAndDepressurize());
        else if(!isOpen)
            StartCoroutine(PressurizeAndOpen());

    }
}
