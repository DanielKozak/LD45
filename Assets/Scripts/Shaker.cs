using UnityEngine;
using System.Collections;

public class Shaker : MonoBehaviour
{
    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public float Duration = 10f;
    public float Speed = 6.0f;
    public float Magnitude = 0.5f;
    public bool Test = false;

    public void OnEnable()
    {
        Debug.Log("Shake");
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {

        float elapsed = 0.0f;
        float randomStartX = Random.Range(-2000f, 2000f);
        float randomStartY = Random.Range(-2000f, 2000f);

        Vector3 originalCamPos = transform.position;

        while (elapsed < Duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / Duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map noise to [-1, 1]
            float x = Mathf.Sin(randomStartX + percentComplete * Speed);
            float y = Mathf.Cos(randomStartY + percentComplete * Speed);
            x *= Magnitude * damper;
            y *= Magnitude * damper;

            transform.position = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z);

            yield return null;
        }

        enabled = false;
        transform.position = originalCamPos;
    }
}
