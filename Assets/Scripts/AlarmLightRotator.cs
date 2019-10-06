using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLightRotator : MonoBehaviour
{

    public float rotSpeed = 0.1f;

    private void Start()
    {
        StartCoroutine(Rotating());

    }
    public IEnumerator Rotating()
    {
        while (true)
        {
            gameObject.transform.RotateAround(Vector3.forward, rotSpeed*Time.deltaTime);
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
