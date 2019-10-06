using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    
    #endregion
    



    private List<string> CurrentAlertList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AuxDrainCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StatController.Instance.SetAuxPower(1);
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            MainCam.GetComponent<Shaker>().enabled = true;
        }
    }

    private IEnumerator AuxDrainCoroutine()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(1f);
            StatController.Instance.SetAuxPower(-1);
             
        }

    }
    
}
