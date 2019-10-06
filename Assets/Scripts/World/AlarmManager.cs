using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlarmManager : MonoBehaviour
{

    public static AlarmManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public List<AlarmLightRotator> LightList;

    public TextMeshProUGUI AlarmLabel;


    private List<string> AlarmMessageList = new List<string>();

    

    public TextMeshProUGUI ShieldText;
    public TextMeshProUGUI ShieldValue;

    public TextMeshProUGUI LifeSupportText;
    public TextMeshProUGUI LifeSupportValue;

    public TextMeshProUGUI AuxPowerText;
    public TextMeshProUGUI AuxPowerValue;


    bool ShieldLowRunning;
    bool LifeSupportLowRunning;
    bool AuxLowRunning;
    bool ShieldCriticalRunning;
    bool LifeSupportCriticalRunning;

    private void OnEnable()
    {
        AlarmLabel.text = "";
        AlarmLabel.GetComponent<TextFlasher>().StartFlashing(Color.red);

        StartCoroutine(AlarmTextCoroutine());
    }

    bool LightsOn;

    private IEnumerator AlarmTextCoroutine()
    {
        while (true)
        {
            if(AlarmMessageList.Count == 0)
            {
                if (LightsOn)
                {
                    foreach (AlarmLightRotator light in LightList)
                    {
                        light.StopRotating();
                    }
                }
                
            }
            else
            {
                if (!LightsOn)
                {
                    foreach (AlarmLightRotator light in LightList)
                    {
                        light.StartRotating();
                    }
                }
                for (int i = 0; i < AlarmMessageList.Count; i++)
                {
                    AlarmLabel.text = AlarmMessageList[i];
                    yield return new WaitForSecondsRealtime(1f);
                }

            }
            yield return null;
            
        }
    }

    public void StartAlarmShieldsLow()
    {
        if (ShieldLowRunning) return;
        ShieldLowRunning = true;
        //PlaySound();
        ShieldText.gameObject.GetComponent<TextFlasher>().StartFlashing(Color.yellow);
        ShieldValue.gameObject.GetComponent<TextFlasher>().StartFlashing(Color.yellow);
    }

    public void StopAlarmShieldsLow()
    {
        ShieldLowRunning = false;
        ShieldText.gameObject.GetComponent<TextFlasher>().StopFlashing();
        ShieldValue.gameObject.GetComponent<TextFlasher>().StopFlashing();
    }

    public void StartAlarmLifeSupportLow()
    {
        if (LifeSupportLowRunning) return;
        LifeSupportLowRunning = true;
        //PlaySound();
        LifeSupportText.gameObject.GetComponent<TextFlasher>().StartFlashing(Color.yellow);
        LifeSupportValue.gameObject.GetComponent<TextFlasher>().StartFlashing(Color.yellow);

    }
    public void StopAlarmLifeSupportLow()
    {
        LifeSupportLowRunning = false;
        LifeSupportText.gameObject.GetComponent<TextFlasher>().StopFlashing();
        LifeSupportValue.gameObject.GetComponent<TextFlasher>().StopFlashing();

    }
    public void StartAlarmAuxLow()
    {
        if (AuxLowRunning) return;
        AuxLowRunning = true;
        //PlaySound();
        AuxPowerText.gameObject.GetComponent<TextFlasher>().StartFlashing(Color.yellow);
        AuxPowerValue.gameObject.GetComponent<TextFlasher>().StartFlashing(Color.yellow);
    }
    public void StopAlarmAuxLow()
    {
        AuxLowRunning = false;
        AuxPowerText.gameObject.GetComponent<TextFlasher>().StopFlashing();
        AuxPowerValue.gameObject.GetComponent<TextFlasher>().StopFlashing();
    }
    public void StartAlarmProximity()
    {
        //PLAySOUND CONTINUOUSLY
        Debug.Log("Alarm proxy");
        AlarmMessageList.Add("PROXIMITY ALERT, ASTEROID INCOMING");
        
    }
    public void StopAlarmProximity()
    {
        AlarmMessageList.Remove("PROXIMITY ALERT, ASTEROID INCOMING");
        
    }
    public void StartAlarmShieldsCritical()
    {
        if (ShieldCriticalRunning) return;
        ShieldCriticalRunning = true;
        //PLAySOUND CONTINUOUSLY
        AlarmMessageList.Add("SHIELDS CRITICAL");
        StopAlarmShieldsLow();
    }
    public void StopAlarmShieldsCritical()
    {
        ShieldCriticalRunning = false;
        AlarmMessageList.Remove("SHIELDS CRITICAL");
    }


    public void StartAlarmLifeSupportCritical()
    {
        if (LifeSupportCriticalRunning) return;
        LifeSupportCriticalRunning = true;
        //PLAySOUND CONTINUOUSLY
        AlarmMessageList.Add("SHIELDS CRITICAL");
        StopAlarmLifeSupportLow();
    }
    public void StopAlarmLifeSupportCritical()
    {
        LifeSupportCriticalRunning = false;
        AlarmMessageList.Remove("SHIELDS CRITICAL");
    }







}
