using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyIn : MonoBehaviour
{
    public float delay;
    public Vector2 offset;
    public float lerpSpeed;

    private Vector2 goalPos;

    private RectTransform rt;

    private bool flying;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();

        goalPos = rt.anchoredPosition;

        rt.anchoredPosition += offset;

        Invoke("StartFlying", delay);
    }

    void StartFlying()
    {
        flying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (flying)
        {
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, goalPos, lerpSpeed * Time.deltaTime);
        }
    }
}
