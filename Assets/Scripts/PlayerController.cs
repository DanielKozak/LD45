using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum PlayerState
    {
        Floating,
        Grabbing
    }

    private PlayerState motionState = PlayerState.Grabbing;
    private float JumpCharge = 0f;

    public Camera MainCam;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            JumpCharge += 0.2f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(motionState == PlayerState.Grabbing)
            {
                mousePos = MainCam.ScreenToWorldPoint(Input.mousePosition);
                jumpDirection = mousePos - pos;
                Jump(jumpDirection);
            }
            JumpCharge = 0f;

        }

    }

    void Jump(Vector2 direction)
    {
        rb.AddForce(direction, ForceMode2D.Force);
        rb.velocity = (direction.normalized * JumpCharge);
        motionState = PlayerState.Floating;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLLISION");
        rb.velocity = new Vector2(0,0);
        rb.freezeRotation = true;
        motionState = PlayerState.Grabbing;
        rb.freezeRotation = false;

    }
}
