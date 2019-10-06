using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    void Update()
    {
        pos = transform.position;
        if (Input.GetMouseButton(0))
        {
            if (motionState == PlayerState.Floating) return;
            rb.velocity = new Vector2(0, 0);
            if (JumpCharge < 30) JumpCharge += 0.5f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(motionState == PlayerState.Grabbing)
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
        motionState = PlayerState.Floating;
        orientationState = PlayerOrientation.Vertical;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = new Vector2(0,0);
        //Tooltip.gameObject.transform.rotation = Quaternion.identity;
        motionState = PlayerState.Grabbing;
        if(collision.gameObject.GetComponent<Surface>().Orient == Surface.Orientation.Vertical)
        {
            orientationState = PlayerOrientation.Vertical;
        }
        else
        {
            orientationState = PlayerOrientation.Horizontal;
        }

        Debug.Log("COLLISION " + orientationState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "TriggerManualGenerator")
        { 
            Debug.Log("Manual Generator enter");
            Tooltip.text = "SPACE to interact";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "TriggerManualGenerator")
        {
            Debug.Log("Manual Generator exit");
            Tooltip.text = "";
        }
    }
}
