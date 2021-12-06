using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [HideInInspector]
    public float xGoal;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(xGoal, transform.position.y, transform.position.z), 30f * Time.deltaTime);
    }
}
