using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatController : MonoBehaviour
{
    public static StatController Instance;

    #region UI
    public Slider ShieldSlider;
    public Slider LifeSupportSlider;
    public Slider ThrusterPowerSlider;
    public Slider AuxPowerSlider;
    public Slider StarterPowerSlider;


    public Slider ShieldFactorSlider;
    public Slider LifeSupportFactorSlider;
    public Slider ThrusterFactorSlider;

    #endregion 

    public float LifeSupport = 50;
    public float Shields = 50;
    public float ThrusterPower = 50;

    public float AuxPower = 50;
    public float StarterPower = 0;

    public float ShieldFactor = 0.5f;
    public float LifeSupportFactor = 0.5f;
    public float ThrusterPowerFactor = 0.5f;



    private void Awake()
    {
        Instance = this;
    }

    public void SetLifeSupport(float delta)
    {
        if (LifeSupport + delta < 0 || LifeSupport + delta > 100) return;
        LifeSupport += delta;
        LifeSupportSlider.value = LifeSupport / 100f;
    }

    public void SetShields(float delta)
    {
        if (Shields + delta < 0 || Shields + delta > 100) return;
        Shields += delta;
        ShieldSlider.value = Shields / 100f;
    }

    public void SetThrusterPower(float delta)
    {
        if (ThrusterPower + delta < 0 || ThrusterPower + delta > 100) return;
        ThrusterPower += delta;
        ThrusterPowerSlider.value = ThrusterPower / 100f;
    }


    public void SetAuxPower(float delta)
    {
        if (AuxPower + delta < 0 || AuxPower + delta > 100) return;
        AuxPower += delta;
        AuxPowerSlider.value = AuxPower / 100f;
        if(delta > 0)
        {
            SetShields(delta * ShieldFactor);
            SetLifeSupport(delta * LifeSupportFactor);
            SetThrusterPower(delta * ThrusterPowerFactor);
        }
        else
        {
            SetShields(delta);
            SetLifeSupport(delta);
            SetThrusterPower(delta);
        }

    }


    public void SetStarterPower(float delta)
    {
        if (StarterPower + delta < 0 || StarterPower + delta > 100) return;
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
    public void SetThrusterPowerFactor(float delta)
    {
        ThrusterPowerFactor += delta;
        ThrusterFactorSlider.value = ThrusterPowerFactor;
    }

    public void OnAddShieldFactor(float add)
    {
        if (ShieldFactor + add < 0 || ShieldFactor + add > 1) return;

        SetShieldFactor(add);
        SetLifeSupportFactor(-add / 2);
        SetThrusterPowerFactor(-add / 2);
    }
    public void OnAddLifeSupportFactor(float add)
    {
        if (LifeSupportFactor + add < 0 || LifeSupportFactor + add > 1) return;

        SetShieldFactor(- add / 2);
        SetLifeSupportFactor(add);
        SetThrusterPowerFactor(-add / 2);
    }
    public void OnAddThrusterFactor(float add)
    {
        if (ThrusterPowerFactor + add < 0 || ThrusterPowerFactor + add > 1) return;

        SetShieldFactor(- add / 2);
        SetLifeSupportFactor(-add / 2);
        SetThrusterPowerFactor(add);
    }
}
