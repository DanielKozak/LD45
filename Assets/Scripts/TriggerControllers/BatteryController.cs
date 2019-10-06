using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class BatteryController : MonoBehaviour
{
    public Light2D indLight;

    public enum Status
    {
        Empty,
        Vacuum,
        Charged
    }

    public Status BatteryStatus;

    public TMP_Text chargePercentText;

    public Transform VacPosition;
    public Transform ReactorPosition;

    public bool isInAirlock;
    public bool isInVacGen;
    public bool isInReactor;

    Rigidbody2D rb;

    public Transform onPlayerTransform;
    public Transform worldTransform;
    public Vector3 followOffset;

    

    public static BatteryController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeStatus(Status.Empty);
        followOffset = new Vector3(-1.264f, 0, 0);
    }
    public void ChangeStatus(Status s)
    {
        switch (s)
        {
            case Status.Empty:
                BatteryStatus = s;
                indLight.color = Color.red;
                indLight.intensity = 4;
                break;

            case Status.Vacuum:
                BatteryStatus = s;
                indLight.color = Color.yellow;
                indLight.intensity = 4;
                break;

            case Status.Charged:
                BatteryStatus = s;
                indLight.color = Color.green;
                indLight.intensity = 4;
                break;
        }
    }

    private void LateUpdate()
    {
        if (!PlayerController.Instance.isBatteryAttached) return;
        else
        {
            transform.position = PlayerController.Instance.gameObject.transform.position + followOffset;
        }
    }
    public void Grab()
    {
        //transform.SetParent(onPlayerTransform);
        //transform.localPosition = Vector3.zero;
        //transform.position = onPlayerTransform.position;
        //transform.rotation = onPlayerTransform.rotation;
        PlayerController.Instance.isBatteryAttached = true;
        PlayerController.Instance.Tooltip.text = "SPACE to drop battery";
        transform.rotation = Quaternion.Euler(Vector3.zero);

        //rb.simulated = false;

    }

    public void Release()
    {
        //transform.SetParent(worldTransform);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        PlayerController.Instance.isBatteryAttached = false;
        //rb.simulated = true;

        //TODO Add small random rotation and force
    }

    public void PlugInVac()
    {
        //transform.SetParent(worldTransform);

        transform.position = VacPosition.position;
        transform.rotation = VacPosition.rotation;
        PlayerController.Instance.isBatteryAttached = false;
        isInVacGen = true;
        WorkVacuum();
    }

    public void PlugInReactor()
    {
        //transform.SetParent(worldTransform);
        transform.position = ReactorPosition.position;
        transform.rotation = ReactorPosition.rotation;
        PlayerController.Instance.isBatteryAttached = false;

        isInReactor = true;
        WorkReactor();
    }

    public void WorkVacuum()
    {
        if(BatteryStatus == Status.Empty)
        {
            StartCoroutine(ShowShortText("it has to be filled with vacuum from the airlock, dude"));
        }
        if (BatteryStatus == Status.Charged)
        {
            StartCoroutine(ShowShortText("Wrong Hole, Sir"));
        }
        if (BatteryStatus == Status.Vacuum)
            StartCoroutine(VacuumCoroutine());

    }

    private IEnumerator VacuumCoroutine()
    {
        int percent = 0;
        chargePercentText.gameObject.SetActive(true);
        //playsound
        while(percent < 100)
        {
            percent++;
            chargePercentText.text = percent + "%";
            yield return null;
        }
        //playsound
        ChangeStatus(Status.Charged);

        chargePercentText.gameObject.SetActive(false);
    }

    public void WorkReactor()
    {
        if (BatteryStatus == Status.Empty)
        {
            StartCoroutine(ShowShortText("first vacuum, then charge, then here."));
        }
        if (BatteryStatus == Status.Vacuum)
        {
            StartCoroutine(ShowShortText("This is not the vacuum Generator"));
        }
        if (BatteryStatus == Status.Charged)
            StartCoroutine(ReactorCoroutine());

    }
    private IEnumerator ReactorCoroutine()
    {
        int percent = 0;
        chargePercentText.gameObject.SetActive(true);
        //playsound
        while (percent < 100)
        {
            percent++;
            chargePercentText.text = percent + "%";
            yield return null;
        }
        //playsound
        ChangeStatus(Status.Empty);

        chargePercentText.gameObject.SetActive(false);

        StatController.Instance.SetStarterPower(0.1f);
    }


    public void WorkAirlock()
    {
        if (!isInAirlock) return;

        if (BatteryStatus == Status.Vacuum)
        {
            StartCoroutine(ShowShortText("You brought a full charge of vacuum to vacuum. Nice. "));
        }
        if (BatteryStatus == Status.Charged)
        {
            StartCoroutine(ShowShortText("Are you going to throw it out?"));
        }
        if (BatteryStatus == Status.Empty)
            StartCoroutine(AirlockCoroutine());
    }

    private IEnumerator AirlockCoroutine()
    {
        while (!AirlockControler.Instance.charging)
        {
            yield return new WaitForSecondsRealtime(1f);
        }

        int percent = 0;
        chargePercentText.gameObject.SetActive(true);
        //playsound
        while (percent < 100)
        {
            percent++;
            chargePercentText.text = percent + "%";
            yield return null;
        }
        //playsound
        ChangeStatus(Status.Vacuum);

        chargePercentText.gameObject.SetActive(false);
    }

    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Space))
        {
         //   PlugInVac();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "TriggerAirlockBattery")
        {
            isInAirlock = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "TriggerAirlockBattery")
        {
            isInAirlock = false;
        }
    }

    private IEnumerator ShowShortText(string newText)
    {
        string oldText = PlayerController.Instance.Tooltip.text;
        PlayerController.Instance.Tooltip.text = newText;
        yield return new WaitForSecondsRealtime(1f);
        PlayerController.Instance.Tooltip.text = oldText;

    }
}
