using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [HideInInspector]
    public float xGoal;

    [HideInInspector]
    public bool hide;

    private float showY;
    private float hideY;

    // Update is called once per frame
    void Start()
    {
        hide = true;
        showY = transform.position.y;
        hideY = showY + 2f;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(xGoal, hide ? hideY : showY, transform.position.z), 30f * Time.deltaTime);
    }
}
