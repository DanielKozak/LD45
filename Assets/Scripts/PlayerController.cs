using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public TMP_Text Tooltip;

    private enum PlayerState
    {
        Floating,
        Grabbing
    }
    private enum PlayerOrientation
    {
        Vertical, Horizontal
    }

    private PlayerOrientation orientationState = PlayerOrientation.Vertical;

    private PlayerState motionState = PlayerState.Grabbing;
    private float JumpCharge = 5f;

    public Camera MainCam;

    public Rigidbody2D rb;

    public bool isBatteryAttached;

    public bool inBatteryTrigger;
    public bool inVacGenTrigger;
    public bool inManGenTrigger;
    public bool inAirlockTrigger;
    public bool inReactorTrigger;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    Vector2 pos;
    Vector2 mousePos;
    Vector2 jumpDirection;

    bool PunFirstTime = true;

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (inVacGenTrigger)
            {
                if (!isBatteryAttached)
                {
                    if (BatteryController.Instance.isInVacGen)
                    {
                        BatteryController.Instance.Grab();
                    }
                    else
                    {
                        Tooltip.text = "Need to carry a battery filled with vacuum! (from the airlock)";
                    }
                }
                else
                {
                    BatteryController.Instance.PlugInVac();
                }

            }
            else if (inManGenTrigger)
            {
                if(PunFirstTime)
                    Tooltip.text = "Yeah, work those robot muscles";
                StatController.Instance.SetAuxPower(1f);
            }
            else if (inAirlockTrigger)
            {
                AirlockControler.Instance.Toggle();
            }
            else if (inReactorTrigger)
            {
                if (!isBatteryAttached)
                {
                    if (BatteryController.Instance.isInReactor)
                    {
                        BatteryController.Instance.Grab();
                    }
                    else
                    {
                        Tooltip.text = "Need to carry a battery filled with Energy! (from the vacuum generator)";
                    }
                }
                else
                {
                    BatteryController.Instance.PlugInReactor();
                }
            }
            else if (inBatteryTrigger)
            {
                if (isBatteryAttached)
                {
                    BatteryController.Instance.Release();
                }
                else
                {
                    BatteryController.Instance.Grab();

                }
            }
        }

        pos = transform.position;
        if (Input.GetMouseButton(0))
        {
            //if(EventSystem.current.IsPointerOverGameObject()) return;
            if (motionState == PlayerState.Floating) return;
            rb.velocity = new Vector2(0, 0);
            if (JumpCharge < 30) JumpCharge += 0.5f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            //if(EventSystem.current.IsPointerOverGameObject()) return;
            if (motionState == PlayerState.Grabbing)
            {
                mousePos = MainCam.ScreenToWorldPoint(Input.mousePosition);
                jumpDirection = mousePos - pos;
                Jump(jumpDirection);
            }
            JumpCharge = 5f;

        }

    }

    void Jump(Vector2 direction)
    {
        rb.AddForce(direction, ForceMode2D.Force);
        rb.velocity = (direction.normalized * JumpCharge);
        transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), 1, 1);
        BatteryController.Instance.followOffset = new Vector3(BatteryController.Instance.followOffset.x - Mathf.Sign(rb.velocity.x)*BatteryController.Instance.followOffset.x, 0, 0);

        motionState = PlayerState.Floating;
        orientationState = PlayerOrientation.Vertical;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = new Vector2(0,0);
        //Tooltip.gameObject.transform.rotation = Quaternion.identity;
        motionState = PlayerState.Grabbing;

        Debug.Log("COLLISION ");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "TriggerManualGenerator")
        { 
            Tooltip.text = "Hit SPACE to work the generator";
            inManGenTrigger = true;
        }
        if (collision.gameObject.name == "TriggerVacGenerator")
        {
            Tooltip.text = "SPACE to insert/remove battery";
            inVacGenTrigger = true;
        }
        if (collision.gameObject.name == "TriggerAirlockControl")
        {
            inAirlockTrigger = true;
            Tooltip.text = "SPACE to cycle airlock";
        }
        if (collision.gameObject.name == "TriggerReactorCharger")
        {
            inReactorTrigger = true;
            Tooltip.text = "SPACE to insert/remove battery";
        }

        if (collision.gameObject.name == "Battery")
        {
            if (isBatteryAttached) return;
            inBatteryTrigger = true;
            Tooltip.text = "SPACE to grab battery";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "TriggerManualGenerator")
        {
            inManGenTrigger = false; 
            Tooltip.text = "";
        }
        if (collision.gameObject.name == "TriggerVacGenerator")
        {
            inVacGenTrigger = false;
            Tooltip.text = "";
        }
        if (collision.gameObject.name == "TriggerAirlockControl")
        {
            inAirlockTrigger = false;
            Tooltip.text = "";
        }
        if (collision.gameObject.name == "TriggerReactorCharger")
        {
            inReactorTrigger = false;
            Tooltip.text = "";
        }
        if (collision.gameObject.name == "Battery")
        {
            if (isBatteryAttached) return;
            inBatteryTrigger = false; ;
            Tooltip.text = "";
        }
    }
}
