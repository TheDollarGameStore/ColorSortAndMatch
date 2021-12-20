using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 goalPos;
    private float shakeAmount;
    void Start()
    {
        goalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        shakeAmount = Mathf.Lerp(shakeAmount, 0f, 10f * Time.deltaTime);
        transform.position += new Vector3(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount));
        transform.position = Vector3.Lerp(transform.position, goalPos, 10f * Time.deltaTime);
    }

    public void Shake()
    {
        shakeAmount = 0f;
    }
}
