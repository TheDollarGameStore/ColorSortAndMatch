using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public Constants.SphereColor color;

    public GameObject popEffect;

    [HideInInspector]
    public bool inClaw;

    [HideInInspector]
    public Vector3 goalPos;

    private void Update()
    {
        if (!inClaw)
        {
            transform.position = Vector3.Lerp(transform.position, goalPos, 20f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, GameManager.instance.claw.targetTube.transform.position + (Vector3.up * 8.4f), 20f * Time.deltaTime);
        }
        
    }

    private void Pop()
    {
        Instantiate(popEffect, transform.position, Quaternion.identity);
        GameManager.instance.canMove = true;
        Camera.main.GetComponent<CameraBehaviour>().Shake();
        Destroy(gameObject);
    }
}
