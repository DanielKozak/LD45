using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatController : MonoBehaviour
{
    public static StatController Instance;

    #region UI
    public TMP_Text ShieldValueText;
    public TMP_Text LifeSupportValueText;
    public TMP_Text AuxPowerValueText;

    public Slider StarterPowerSlider;


    public Slider ShieldFactorSlider;
    public Slider LifeSupportFactorSlider;

    #endregion 

    public float LifeSupport = 50;
    public float Shields = 50;

    public float AuxPower = 50;
    public float StarterPower = 0;

    public float ShieldFactor = 0.5f;
    public float LifeSupportFactor = 0.5f;



    private void Awake()
    {
        Instance = this;
    }

    public void SetLifeSupport(float delta)
    {
        if(LifeSupport + delta < 0)
        {
            ShipController.Instance.DieLifeSupport();
            return;
        }
        if (LifeSupport + delta > 100) return;
        LifeSupport += delta;
        if(LifeSupport < 50)
        {
            AlarmManager.Instance.StartAlarmLifeSupportLow();
        }
        if(LifeSupport < 25)
        {
            AlarmManager.Instance.StartAlarmLifeSupportCritical();
        }
        if (LifeSupport > 50)
        {
            AlarmManager.Instance.StopAlarmLifeSupportLow();
        }
        if (LifeSupport > 25)
        {
            AlarmManager.Instance.StopAlarmLifeSupportCritical();
        }


        LifeSupportValueText.text = Mathf.CeilToInt(LifeSupport) + "%";
    }

    public void SetShields(float delta)
    {
        if (Shields + delta < 0 || Shields + delta > 100) return;
        Shields += delta;
        if (Shields < 50)
        {
            AlarmManager.Instance.StartAlarmLifeSupportLow();
        }
        if (Shields < 25)
        {
            AlarmManager.Instance.StartAlarmLifeSupportCritical();
        }
        if (Shields > 50)
        {
            AlarmManager.Instance.StopAlarmLifeSupportLow();
        }
        if (Shields > 25)
        {
            AlarmManager.Instance.StopAlarmLifeSupportCritical();
        }
        ShieldValueText.text = Mathf.CeilToInt(Shields) + "%";
    }
    


    public void SetAuxPower(float delta)
    {
        if (AuxPower + delta < 0 || AuxPower + delta > 100) return;
        AuxPower += delta;
        if (AuxPower < 50)
        {
            AlarmManager.Instance.StartAlarmLifeSupportLow();
        }
        if (AuxPower < 25)
        {
            AlarmManager.Instance.StartAlarmLifeSupportCritical();
        }
        if (AuxPower > 50)
        {
            AlarmManager.Instance.StopAlarmLifeSupportLow();
        }
        if (AuxPower > 25)
        {
            AlarmManager.Instance.StopAlarmLifeSupportCritical();
        }

        AuxPowerValueText.text = Mathf.CeilToInt(AuxPower) + "MWt";
        if(delta > 0)
        {
            SetShields(delta * ShieldFactor);
            SetLifeSupport(delta * LifeSupportFactor);
        }
        else
        {
            SetShields(delta);
            SetLifeSupport(delta);
        }

    }


    public void SetStarterPower(float delta)
    {
        if (StarterPower + delta < 0) return;
        if(StarterPower + delta > 100)
        {
            ShipController.Instance.Win();
            return;
        }
        StarterPower += delta;
        StarterPowerSlider.value = StarterPower / 100f;
    }


    public void SetShieldFactor(float delta)
    {
        ShieldFactor += delta;
        ShieldFactorSlider.value = ShieldFactor;
    }
    public void SetLifeSupportFactor(float delta)
    {
        LifeSupportFactor += delta;
        LifeSupportFactorSlider.value = LifeSupportFactor;

    }

    public void OnAddShieldFactor(float add)
    {
        if (ShieldFactor + add < 0 || ShieldFactor + add > 1) return;

        SetShieldFactor(add);
        SetLifeSupportFactor(-add / 2);
    }
    public void OnAddLifeSupportFactor(float add)
    {
        if (LifeSupportFactor + add < 0 || LifeSupportFactor + add > 1) return;

        SetShieldFactor(- add / 2);
        SetLifeSupportFactor(add);
    }
}
