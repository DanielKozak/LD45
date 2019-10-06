using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    public static ShipController Instance;

    private void Awake()
    {
        Instance = this;
    }
    #region UI

    public TMP_Text MainAlertLabel;
    public TMP_Text ToolTipText;

    public Camera MainCam;
    public Light2D shieldLight;
    public GameObject Manual;


    #endregion

    public bool isManualShown;

    private List<string> CurrentAlertList = new List<string>();

    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("enabled");
        StartCoroutine(AuxDrainCoroutine());
        StartCoroutine(AsteroidEventCoroutine());
    }

    public void SetManual(bool a)
    {
        isManualShown = a;
    }

    private IEnumerator AuxDrainCoroutine()
    {
        Debug.Log("aux_drain");
        while (true)
        {
            yield return new WaitForSecondsRealtime(2f);

            Debug.Log("aux_drain_2");
            StatController.Instance.SetAuxPower(-1);
        }

    }
    private IEnumerator AsteroidEventCoroutine()
    {
        
        while (true)
        {
            
            float delay = UnityEngine.Random.Range(20f, 40f);
            yield return new WaitForSecondsRealtime(delay);
            AlarmManager.Instance.StartAlarmProximity();

            //ALARM ASTEROID
            delay = UnityEngine.Random.Range(10f, 20f);
            yield return new WaitForSecondsRealtime(delay);

            StartCoroutine(AsteroidHit());

        }

    }

    private IEnumerator AsteroidHit()
    {
        //yield return new WaitForSecondsRealtime(10f);
        //DieLifeSupport();
        MainCam.GetComponent<Shaker>().enabled = true;
        AlarmManager.Instance.StopAlarmProximity();

        int times = Mathf.FloorToInt(UnityEngine.Random.value + 1 * 10);
        for (int i = 0; i < times + 1; i++)
        {
            float delay = UnityEngine.Random.Range(0.05f, 0.2f);

            yield return new WaitForSecondsRealtime(delay);
            delay = UnityEngine.Random.Range(0.05f, 0.2f);
            shieldLight.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(delay);
            shieldLight.gameObject.SetActive(false);
        }

        if (StatController.Instance.Shields <= 25f)
        {
            DieAsteroid();
        }
        StatController.Instance.SetShields(-25f);

    }

    public Image blocker;
    public Image Fader;
    public TMP_Text endText;

    public void DieAsteroid()
    {
        StartCoroutine(EndGameRoutine(1));
    }

    public void DieLifeSupport()
    {
        StartCoroutine(EndGameRoutine(2));

    }

    public void Win()
    {
        StartCoroutine(EndGameRoutine(0));

    }

    bool canExit = false;
    public IEnumerator EndGameRoutine(int type)
    {
        float a = 0f;
        Color c;
        Debug.Log("ËOGR");
        blocker.gameObject.SetActive(true);

        while (blocker.color.a <= 1f)
        {
            a += 0.01f;
            c = new Color(0, 0, 0, a);
            blocker.color = c;
            Debug.Log(blocker.color.a);
            yield return null;
        }

        switch (type)
        {
            case 0:
                endText.text = "Wow. You won! You restarted the reactor! Now we can get to the next task. But that's another story.";

                break;
            case 1:
                endText.text = "You Died.\n\n\n\"I told you so\".\n\t\tMurphy.";

                break;
            case 2:
                endText.text = "You Died.\n\n\n\"I told you so\".\n\t\tMurphy.";

                break;

            default:
                break;
        }
        
         a = 1.0f;
       
        while(Fader.color.a >= 0)
        {
            a -= 0.01f;
            c = new Color(0, 0, 0, a);
            Fader.color = c;
            yield return null;
        }
        canExit = true;
        StopAllCoroutines();
    }

    public GameObject Manual_1;

    private void Update()
    {
        if (isManualShown)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Manual_1.SetActive(false);
            }
        }
        if (canExit)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }

}
