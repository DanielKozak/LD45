using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class AlarmLightRotator : MonoBehaviour
{

    public float rotSpeed = 0.1f;

    private void Start()
    {
        gameObject.GetComponent<Light2D>().enabled = false;

    }

    public IEnumerator Rotating()
    {
        while (true)
        {
            gameObject.transform.RotateAround(Vector3.forward, rotSpeed*Time.deltaTime);
            yield return null;
        }
    }

    public void StartRotating()
    {
        gameObject.GetComponent<Light2D>().enabled = true;
        StartCoroutine(Rotating());
    }

    public void StopRotating()
    {
        gameObject.GetComponent<Light2D>().enabled = false ;
        StartCoroutine(Rotating());
    }
}
